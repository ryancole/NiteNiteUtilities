using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Security.Cryptography;
using NiteNiteUtilities.Utility;

namespace NiteNiteUtilities.Filters
{
    public class TwitchSecretAuthorizationAttribute : AuthorizeAttribute
    {
        #region Methods

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IEnumerable<string> headers;

            // if there is no secret header, we can reject right away
            if (actionContext.Request.Headers.TryGetValues("x-hub-signature", out headers) == false)
            {
                return false;
            }

            var header = headers.Single();

            // we will need to compare the provided hash with the hash that we
            // generate, using our secret
            var providedHash = header.Split('=').Last();

            // we need to convert our secret text to a byte array prior to
            // being used the hmac key
            var secretArray = Encoding.ASCII.GetBytes(PersistantRuntimeData.Guid);

            using (var hmac = new HMACSHA256(secretArray))
            {
                // we need to use the request body as the hash payload
                var body = actionContext.Request.Content.ReadAsStringAsync().Result;

                // now we can generate the resulting hash
                var result = hmac.ComputeHash(Encoding.ASCII.GetBytes(body));

                var stringed = BitConverter.ToString(result).Replace("-", "").ToLower();

                // we are authorized if both of the hashes match
                return providedHash.Equals(stringed, StringComparison.OrdinalIgnoreCase);
            }
        }

        #endregion
    }
}
