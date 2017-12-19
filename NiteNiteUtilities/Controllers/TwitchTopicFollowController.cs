using System;
using System.Net;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using NiteNiteUtilities.View;
using NiteNiteUtilities.Utility;

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

            if (mode.Equals("subscribe", StringComparison.OrdinalIgnoreCase))
            {
                // we may already be subscribed and receiving events from this
                // subscription, so lets reject any other subscriptions that
                // may be pending
                if (PersistantRuntimeData.AlreadySubcribed)
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Conflict
                    };
                }

                // now that we know we've been granted the subscription, we
                // need to respond with the proper challenge
                var challenge = query.First(m => m.Key.Equals("hub.challenge", StringComparison.OrdinalIgnoreCase)).Value;

                // we need to make note that we are now going to be receiving
                // events from this subscription and so we can reject any other
                // subscription attempts that may be coming in
                PersistantRuntimeData.AlreadySubcribed = true;

                Console.WriteLine($"Confirming twitch follower event request using {challenge} ...");

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

        public IHttpActionResult Post(TwitchWebhookUserFollowsView payload)
        {
            Console.WriteLine($"Received follower event for {payload.Data.From} ...");

            // we're just going to store incoming follower events in a queue so
            // that we can pop off of it as needed and we won't lose followers
            // across scene changes or unloading of the browser source
            PersistantRuntimeData.Followers.Enqueue(payload);

            return Ok();
        }

        #endregion
    }
}
