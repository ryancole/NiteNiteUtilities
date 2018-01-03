using System.Collections.Generic;
using NiteNiteUtilities.Repository.Model;

namespace NiteNiteUtilities.Utility
{
    public static class PersistantRuntimeData
    {
        static PersistantRuntimeData()
        {
            Guid = System.Guid.NewGuid().ToString();
            Followers = new Queue<FollowerQueueData>();
        }

        #region Properties

        public static string Guid { get; }

        public static TwitchUser Me { get; set; }

        public static Queue<FollowerQueueData> Followers { get; set; }

        #endregion
    }

    public class FollowerQueueData
    {
        #region Properties

        public string To { get; set; }

        public string From { get; set; }

        #endregion
    }
}
