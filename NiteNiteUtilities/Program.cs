using System;
using System.Net;
using System.Threading.Tasks;
using Nancy.Hosting.Self;
using NiteNiteUtilities.Entity;
using NiteNiteUtilities.Utility;
using NiteNiteUtilities.Repository;

namespace NiteNiteUtilities
{
    public class Program
    {
        async public static Task Main(string[] args)
        {
            Console.WriteLine("Fetching twitch user information ...");

            // fetch the user information from twitch
            TwitchUserData.User = await TwitchApiRepository.FetchUserInformation();

            Console.WriteLine("Fetching twitch follower information ...");

            // fetch follower information
            TwitchUserData.Followers = await TwitchApiRepository.FetchFollowerInformation(TwitchUserData.User);

            // we have to build our desired local host uri based on the port
            // that the user requested
            var uri = new UriBuilder
            {
                Host = "47.188.241.69",
                Port = Configuration.Port
            };

            // nancy self hosting configuration settings
            var hostConfigs = new HostConfiguration
            {
                RewriteLocalhost = true,
                UrlReservations = new UrlReservations
                {
                    CreateAutomatically = true
                }
            };

            // actually begin listening for incoming http requests and let
            // nancy handle them
            try
            {
                using (var host = new NancyHost(hostConfigs, uri.Uri))
                {
                    host.Start();

                    Console.WriteLine("Handling incoming requests ...");
                    Console.WriteLine("Requesting twitch event subscriptions ...");

                    var response = await TwitchApiRepository.RequestSubscription(TwitchUserData.User);

                    if (response != HttpStatusCode.Accepted)
                    {
                        Console.WriteLine("Request for twitch event subscriptions rejected!");
                        return;
                    }

                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static class TwitchUserData
    {
        #region Properties

        public static TwitchUserDetails User { get; set; }

        public static TwitchFollowerDetailsResponse Followers { get; set; }

        #endregion
    }
}
