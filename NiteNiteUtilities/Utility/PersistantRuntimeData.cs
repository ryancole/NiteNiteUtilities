using System.Collections.Generic;
using NiteNiteUtilities.View;
using NiteNiteUtilities.Repository.Model;

namespace NiteNiteUtilities.Utility
{
    public static class PersistantRuntimeData
    {
        static PersistantRuntimeData()
        {
            Guid = System.Guid.NewGuid().ToString();
            Followers = new Queue<TwitchWebhookUserFollowsView>();
        }

        #region Properties

        public static string Guid { get; }

        public static TwitchUser Me { get; set; }

        public static Queue<TwitchWebhookUserFollowsView> Followers { get; set; }

        #endregion
    }
}
