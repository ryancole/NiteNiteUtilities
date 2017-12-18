using System.Collections.Generic;
using NiteNiteUtilities.Repository.Model;

namespace NiteNiteUtilities.Utility
{
    public class PersistantRuntimeData
    {
        static PersistantRuntimeData()
        {
            Instance = new PersistantRuntimeData();
        }

        public PersistantRuntimeData()
        {
            FollowerCount = 0;
            RecentFollows = new List<TwitchFollower>();
        }

        #region Properties

        public int FollowerCount { get; set; }

        public TwitchUser Me { get; set; }

        public ICollection<TwitchFollower> RecentFollows { get; set; }

        public static PersistantRuntimeData Instance { get; }

        #endregion
    }
}
