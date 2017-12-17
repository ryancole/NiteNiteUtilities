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

    public class TwitchWebhookUserFollowsViewData
    {
        #region Properties

        public string To { get; set; }

        public string From { get; set; }

        #endregion
    }
}
