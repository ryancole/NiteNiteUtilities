using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using NiteNiteUtilities.Repository;

namespace NiteNiteUtilities.Utility
{
    public class OwinService
    {
        private IDisposable m_app;

        #region Methods

        public async Task Start()
        {
            var port = ConfigurationManager.AppSettings["port"];

            // start up the web application prior to requesting the twitch data
            // so that our web hook callbacks are active
            m_app = WebApp.Start<StartOwin>($"http://+:{port}");

            // fetch our initial data from twitch
            await FetchInitialTwitchData();
        }

        public void Stop()
        {
            m_app.Dispose();
        }

        private async Task FetchInitialTwitchData()
        {
            var username = ConfigurationManager.AppSettings["TwitchUsername"];

            Console.WriteLine($"Fetching twitch user id for {username} ... ");

            // we need to fetch our user details so that we can request web
            // hook event data from twitch
            PersistantRuntimeData.Me = (await TwitchUserRepository.GetByName(username)).Users.Single();

            Console.WriteLine($"Requesting twitch follower notifications for {PersistantRuntimeData.Me.Id} ... ");

            // now that we have our own user data, we can request the web hook
            // event data
            var response = await TwitchWebhookRepository.Get(PersistantRuntimeData.Me.Id);
        }

        #endregion
    }
}
