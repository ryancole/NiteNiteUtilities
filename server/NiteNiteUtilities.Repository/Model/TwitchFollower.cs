using System.Runtime.Serialization;

namespace NiteNiteUtilities.Repository.Model
{
    [DataContract]
    public class TwitchFollower
    {
        #region Properties

        [DataMember(Name = "to_id")]
        public string To { get; set; }

        [DataMember(Name = "from_id")]
        public string From { get; set; }

        #endregion
    }
}
