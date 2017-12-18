using System;
using System.Net;
using System.Linq;
using System.Web.Http;
using System.Net.Http;

namespace NiteNiteUtilities.Controllers
{
    public class TwitchTopicFollowController : ApiController
    {
        #region Methods

        public HttpResponseMessage Get()
        {
            // we need to pick from the query string to find our
            // twitch-provided parameters
            var query = Request.GetQueryNameValuePairs();

            // first, we're interested in the mode because it dictates what the
            // subsequent parameters we need are
            var mode = query
                .FirstOrDefault(m => m.Key.Equals("hub.mode", StringComparison.OrdinalIgnoreCase))
                .Value;

            // it's possible something fails and we get a crap request
            if (string.IsNullOrWhiteSpace(mode))
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                };
            }

            Console.WriteLine($"GET TwitchTopicFollow, mode = {mode}");

            if (mode.Equals("subscribe", StringComparison.OrdinalIgnoreCase))
            {
                // now that we know we've been granted the subscription, we
                // need to respond with the proper challenge
                var challenge = query.First(m => m.Key.Equals("hub.challenge", StringComparison.OrdinalIgnoreCase)).Value;

                Console.WriteLine($"GET TwitchTopicFollow, challenge = {challenge}");

                return new HttpResponseMessage
                {
                    Content = new StringContent(challenge),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
        }

        public IHttpActionResult Post()
        {
            Console.WriteLine("Received event!");

            return Ok();
        }

        #endregion
    }
}
