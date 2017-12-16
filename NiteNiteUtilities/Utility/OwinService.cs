using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

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

        #endregion
    }
}
