using System.Net;
using System.Configuration;
using System.Threading.Tasks;

namespace NiteNiteUtilities.Repository
{
    public static class TwitchWebhookRepository
    {
        #region Methods

        async public static Task<HttpStatusCode> Get(int id)
        {
            var port = ConfigurationManager.AppSettings["Port"];
            var fqdn = ConfigurationManager.AppSettings["FullyQualifiedDomainName"];

            var query = new[]
            {
                "hub.mode=subscribe",
                "hub.lease_seconds=864000",
                $"hub.topic=https://api.twitch.tv/helix/users/follows?to_id={id}",
                $"hub.callback=http://{fqdn}:{port}/Api/TwitchTopicFollow"
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

        #endregion
    }
}
