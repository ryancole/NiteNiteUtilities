using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NiteNiteUtilities.Repository
{
    public static class IpifyRepository
    {
        #region Methods

        public static async Task<string> Get()
        {
            var request = WebRequest.Create($"https://api.ipify.org/");

            using (var response = await request.GetResponseAsync())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var ip = await reader.ReadToEndAsync();

                    return ip;
                }
            }
        }

        #endregion
    }
}
