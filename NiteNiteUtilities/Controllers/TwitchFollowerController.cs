using System.Net;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using NiteNiteUtilities.Utility;
using NiteNiteUtilities.Repository;

namespace NiteNiteUtilities.Controllers
{
    public class TwitchFollowerController : ApiController
    {
        #region Methods

        async public Task<IHttpActionResult> Get()
        {
            try
            {
                // all we want to do is pop the next follower off the queue so that
                // the browser source can be unloaded and we won't lose track of
                // any followers in the process
                var follower = PersistantRuntimeData.Followers.Dequeue();

                // now we need to out to twitch again and fetch the display name of
                // the person who followed
                var details = await TwitchUserRepository.GetById(follower.Data.From);

                // we only want to respond with the user details
                return Json(details.Users.Single());
            }
            catch
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        #endregion
    }
}
