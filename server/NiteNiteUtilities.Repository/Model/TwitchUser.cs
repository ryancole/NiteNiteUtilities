using System.Runtime.Serialization;

namespace NiteNiteUtilities.Repository.Model
{
    [DataContract]
    public class TwitchUser
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "profile_image_url")]
        public string ProfileImageUrl { get; set; }

        #endregion
    }
}
