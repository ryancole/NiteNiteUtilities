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

        public void Start()
        {
            var port = ConfigurationManager.AppSettings["port"];

            m_app = WebApp.Start<StartOwin>($"http://+:{port}");
        }

        public void Stop()
        {
            m_app.Dispose();
        }

        private async Task FetchInitialTwitchData()
        {
            var username = ConfigurationManager.AppSettings["TwitchUsername"];

            // fetch details of ours twitch user account
            var me = (await TwitchUserRepository.Get(username)).Users.Single();

            // set global persisted data
            PersistantRuntimeData.Instance.Me = me;
        }

        #endregion
    }
}
