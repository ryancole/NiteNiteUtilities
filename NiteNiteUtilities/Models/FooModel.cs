using NiteNiteUtilities.Entity;

namespace NiteNiteUtilities.Models
{
    public class FooModel
    {
        #region Properties

        public TwitchUserDetails User { get; set; }

        public TwitchFollowerDetailsResponse Followers { get; set; }

        #endregion
    }
}
