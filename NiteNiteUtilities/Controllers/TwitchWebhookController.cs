using System.Web.Http;
using System.Threading.Tasks;
using NiteNiteUtilities.Repository;

namespace NiteNiteUtilities.Controllers
{
    public class TwitchWebhookController : ApiController
    {
        #region Methods

        public async Task<IHttpActionResult> Get(int id)
        {
            var response = await TwitchWebhookRepository.Get(id);

            switch (response)
            {
                case System.Net.HttpStatusCode.Accepted:
                    return Ok();

                default:
                    return InternalServerError();
            }
        }

        #endregion
    }
}
