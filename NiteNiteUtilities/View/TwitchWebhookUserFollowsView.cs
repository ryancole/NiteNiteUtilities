using System.Runtime.Serialization;

namespace NiteNiteUtilities.View
{
    public class TwitchWebhookUserFollowsView
    {
        #region Properties

        public string Id { get; set; }

        public string Type { get; set; }

        public string Topic { get; set; }

        public string Timestamp { get; set; }

        public TwitchWebhookUserFollowsViewData Data { get; set; }

        #endregion
    }

    [DataContract]
    public class TwitchWebhookUserFollowsViewData
    {
        #region Properties

        [DataMember(Name = "to_id")]
        public string To { get; set; }

        [DataMember(Name = "from_id")]
        public string From { get; set; }

        #endregion
    }
}
