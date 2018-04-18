using System.Collections.Generic;
using System.Runtime.Serialization;
using NiteNiteUtilities.Repository.Model;

namespace NiteNiteUtilities.Repository.View
{
    [DataContract]
    public class GetTwitchUserView
    {
        #region Properties

        [DataMember(Name = "data")]
        public ICollection<TwitchUser> Users { get; set; }

        #endregion
    }
}
