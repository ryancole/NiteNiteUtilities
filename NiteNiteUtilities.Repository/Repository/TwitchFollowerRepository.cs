using System.Net;
using System.Configuration;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using NiteNiteUtilities.Repository.View;

namespace NiteNiteUtilities.Repository
{
    public static class TwitchFollowerRepository
    {
        #region Methods

        async public static Task<GetTwitchFollowerView> Get(int id)
        {
            var request = HttpWebRequest.Create($"https://api.twitch.tv/helix/users/follows?to_id={id}");

            // the request needs to include the user's own application client
            // identifier, which comes from the config
            request.Headers["Client-ID"] = ConfigurationManager.AppSettings["TwitchClientId"];

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var serializer = new DataContractJsonSerializer(typeof(GetTwitchFollowerView));

                    var payload = serializer.ReadObject(stream) as GetTwitchFollowerView;

                    return payload;
                }
            }
        }

        #endregion
    }
}
