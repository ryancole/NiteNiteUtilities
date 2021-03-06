﻿using System;
using System.Net;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using NiteNiteUtilities.View;
using NiteNiteUtilities.Filters;
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
                // now that we know we've been granted the subscription, we
                // need to respond with the proper challenge
                var challenge = query.First(m => m.Key.Equals("hub.challenge", StringComparison.OrdinalIgnoreCase)).Value;

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

        [TwitchSecretAuthorization]
        public IHttpActionResult Post(TwitchWebhookUserFollowsView payload)
        {
            Console.WriteLine($"Received follower event for {payload.Data.From} ...");

            // we're just going to store incoming follower events in a queue so
            // that we can pop off of it as needed and we won't lose followers
            // across scene changes or unloading of the browser source
            PersistantRuntimeData.Followers.Enqueue(new FollowerQueueData
            {
                To = payload.Data.To,
                From = payload.Data.From
            });

            return Ok();
        }

        #endregion
    }
}
