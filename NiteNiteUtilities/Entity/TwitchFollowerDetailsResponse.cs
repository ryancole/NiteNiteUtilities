using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NiteNiteUtilities.Entity
{
    [DataContract]
    public class TwitchFollowerDetailsResponse
    {
        #region Properties

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "data")]
        public ICollection<TwitchFollowerDetails> Followers { get; set; }

        #endregion
    }
}
