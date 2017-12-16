using System.Web.Http;

namespace NiteNiteUtilities.Controllers
{
    public class HomeController : ApiController
    {
        #region Methods

        public string Get()
        {
            return "Home!";
        }

        #endregion
    }
}
