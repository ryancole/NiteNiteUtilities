using System.Web.Http;
using System.Threading.Tasks;
using NiteNiteUtilities.Repository;
using NiteNiteUtilities.Repository.View;

namespace NiteNiteUtilities.Controllers
{
    public class TwitchFollowerController : ApiController
    {
        #region Methods

        public async Task<GetTwitchFollowerView> Get(string id)
        {
            var follower = await TwitchFollowerRepository.Get(id);

            return follower;
        }

        #endregion
    }
}
