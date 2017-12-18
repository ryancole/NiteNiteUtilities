using System.Net;
using System.Configuration;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using NiteNiteUtilities.Repository.View;

namespace NiteNiteUtilities.Repository
{
    public static class TwitchUserRepository
    {
        #region Methods

        async public static Task<GetTwitchUserView> Get(string name)
        {
            var request = HttpWebRequest.Create($"https://api.twitch.tv/helix/users?login={name}");

            // the request needs to include the user's own application client
            // identifier, which comes from the config
            request.Headers["Client-ID"] = ConfigurationManager.AppSettings["TwitchClientId"];

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(GetTwitchUserView));

                    var payload = serializer.ReadObject(stream) as GetTwitchUserView;

                    return payload;
                }
            }
        }

        #endregion
    }
}
