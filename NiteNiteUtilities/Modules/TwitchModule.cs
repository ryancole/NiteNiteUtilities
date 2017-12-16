using System;
using Nancy;

namespace NiteNiteUtilities.Modules
{
    public class TwitchModule : NancyModule
    {
        public TwitchModule()
        {
            Get["/callback"] = Callback;
        }

        #region Methods

        private dynamic Callback(dynamic parameters)
        {
            Console.WriteLine("Received twitch event subscription request response ...");

            switch (Request.Query["hub.mode"])
            {
                case "subscribe":
                    return Request.Query["hub.challenge"];

                default:
                    return HttpStatusCode.OK;
            }
        }

        #endregion
    }
}
