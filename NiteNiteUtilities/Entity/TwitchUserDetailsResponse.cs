using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NiteNiteUtilities.Entity
{
    [DataContract]
    public class TwitchUserDetailsResponse
    {
        #region Properties

        [DataMember(Name = "data")]
        public ICollection<TwitchUserDetails> Users { get; set; }

        #endregion
    }
}
