using System.Runtime.Serialization;

namespace NiteNiteUtilities.Entity
{
    [DataContract]
    public class TwitchUserDetails
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }

        #endregion
    }
}
