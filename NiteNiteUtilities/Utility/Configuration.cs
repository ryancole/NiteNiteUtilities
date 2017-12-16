using System.Configuration;

namespace NiteNiteUtilities.Utility
{
    public static class Configuration
    {
        #region Properties

        public static int Port
        {
            get
            {
                var port = 8080;

                int.TryParse(ConfigurationManager.AppSettings["port"], out port);

                return port;
            }
        }

        #endregion
    }
}
