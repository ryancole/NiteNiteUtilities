using System.Web.Http;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Owin;

namespace NiteNiteUtilities.Utility
{
    public class StartOwin
    {
        #region Methods

        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration();

            // removing the xml formatter will cause the json formatter to be
            // the default, which lets us serialize responses in json
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            builder.UseWebApi(config);

            // we need to configure 
            builder.UseFileServer(new FileServerOptions
            {
                FileSystem = new PhysicalFileSystem("public"),
                EnableDirectoryBrowsing = false
            });
        }

        #endregion
    }
}
