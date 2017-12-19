using System.Collections.Generic;
using NiteNiteUtilities.View;
using NiteNiteUtilities.Repository.Model;

namespace NiteNiteUtilities.Utility
{
    public static class PersistantRuntimeData
    {
        static PersistantRuntimeData()
        {
            Followers = new Queue<TwitchWebhookUserFollowsView>();
            AlreadySubcribed = false;
        }

        #region Properties

        public static bool AlreadySubcribed { get; set; }

        public static TwitchUser Me { get; set; }

        public static Queue<TwitchWebhookUserFollowsView> Followers { get; set; }

        #endregion
    }
}
