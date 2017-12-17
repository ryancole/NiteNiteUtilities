using System.Web.Http;
using System.Threading.Tasks;
using NiteNiteUtilities.Repository;
using NiteNiteUtilities.Repository.View;

namespace NiteNiteUtilities.Controllers
{
    public class TwitchUserController : ApiController
    {
        #region Methods

        public async Task<GetTwitchUserView> Get()
        {
            var user = await TwitchUserRepository.Get();

            return user;
        }

        #endregion
    }
}
