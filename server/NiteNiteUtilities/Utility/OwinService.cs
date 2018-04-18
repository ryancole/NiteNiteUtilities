using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Topshelf;
using NiteNiteUtilities.Repository;

namespace NiteNiteUtilities.Utility
{
    public class OwinService : ServiceControl
    {
        private IDisposable m_app;

        #region Methods

        public bool Start(HostControl hostControl)
        {
            StartWebServer();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            m_app.Dispose();

            return true;
        }

        private async void StartWebServer()
        {
            var port = ConfigurationManager.AppSettings["port"];

            // start up the web application prior to requesting the twitch data
            // so that our web hook callbacks are active
            m_app = WebApp.Start<StartOwin>($"http://+:{port}");

            // fetch our initial data from twitch
            await FetchInitialTwitchData();
        }

        private async Task FetchInitialTwitchData()
        {
            Console.WriteLine("Fetching public IP address ...");

            PersistantRuntimeData.Ip = await IpifyRepository.Get();

            var username = ConfigurationManager.AppSettings["TwitchUsername"];

            Console.WriteLine($"Fetching twitch user id for {username} ... ");

            // we need to fetch our user details so that we can request web
            // hook event data from twitch
            PersistantRuntimeData.Me = (await TwitchUserRepository.GetByName(username)).Users.Single();

            Console.WriteLine($"Populating follower queue ...");

            // now that we know our user id, we can fetch our the most recent
            // follower so that it shows up on initial load
            var followers = await TwitchFollowerRepository.Get(PersistantRuntimeData.Me.Id);

            if (followers.Followers.Count > 0)
            {
                var follower = followers
                    .Followers
                    .First();

                // lets put them on the queue
                PersistantRuntimeData.Followers.Enqueue(new FollowerQueueData
                {
                    To = follower.To,
                    From = follower.From
                });
            }

            Console.WriteLine($"Requesting twitch follower notifications ... ");

            // now that we have our own user data, we can request the web hook
            // event data
            var response = await TwitchWebhookRepository.Get(
                PersistantRuntimeData.Ip,
                PersistantRuntimeData.Me.Id,
                PersistantRuntimeData.Guid);
        }

        #endregion
    }
}
