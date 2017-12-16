using System.Net;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using NiteNiteUtilities.Entity;

namespace NiteNiteUtilities.Repository
{
    public static class TwitchApiRepository
    {
        #region Methods

        async public static Task<HttpStatusCode> RequestSubscription(TwitchUserDetails user)
        {
            var callback = ConfigurationManager.AppSettings["TwitchCallbackUrl"];

            var query = new[]
            {
                "hub.mode=subscribe",
                $"hub.topic=https://api.twitch.tv/helix/users/follows?to_id={user.Id}",
                $"hub.callback={callback}"
            };

            var request = WebRequest.Create($"https://api.twitch.tv/helix/webhooks/hub?{string.Join("&", query)}") as HttpWebRequest;

            // this needs to be a post request
            request.Method = "POST";

            // the request needs to include the user's own application client
            // identifier, which comes from the config
            request.Headers["Client-ID"] = ConfigurationManager.AppSettings["TwitchClientId"];

            using (var response = await request.GetResponseAsync())
            {
                return (response as HttpWebResponse).StatusCode;
            }
        }

        async public static Task<TwitchUserDetails> FetchUserInformation()
        {
            // we need the username that we want need to fetch information
            // about. this is set by the user and needs to be their username
            var username = ConfigurationManager.AppSettings["TwitchUsername"];

            var request = HttpWebRequest.Create($"https://api.twitch.tv/helix/users?login={username}");

            // the request needs to include the user's own application client
            // identifier, which comes from the config
            request.Headers["Client-ID"] = ConfigurationManager.AppSettings["TwitchClientId"];

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(TwitchUserDetailsResponse));

                    var payload = serializer.ReadObject(stream) as TwitchUserDetailsResponse;

                    return payload.Users.First();
                }
            }
        }

        async public static Task<TwitchFollowerDetailsResponse> FetchFollowerInformation(TwitchUserDetails user)
        {
            var request = HttpWebRequest.Create($"https://api.twitch.tv/helix/users/follows?to_id={user.Id}");

            // the request needs to include the user's own application client
            // identifier, which comes from the config
            request.Headers["Client-ID"] = ConfigurationManager.AppSettings["TwitchClientId"];

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(TwitchFollowerDetailsResponse));

                    var payload = serializer.ReadObject(stream) as TwitchFollowerDetailsResponse;

                    return payload;
                }
            }
        }

        #endregion
    }
}
