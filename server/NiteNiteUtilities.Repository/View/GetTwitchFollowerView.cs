using System.Collections.Generic;
using System.Runtime.Serialization;
using NiteNiteUtilities.Repository.Model;

namespace NiteNiteUtilities.Repository.View
{
    [DataContract]
    public class GetTwitchFollowerView
    {
        #region Properties

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "data")]
        public ICollection<TwitchFollower> Followers { get; set; }

        #endregion
    }
}
