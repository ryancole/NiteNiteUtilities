using Nancy;
using NiteNiteUtilities.Models;

namespace NiteNiteUtilities.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = Foo;
        }

        #region Methods

        private dynamic Foo(dynamic parameters)
        {
            var model = new FooModel
            {
                User = TwitchUserData.User,
                Followers = TwitchUserData.Followers
            };

            return View[model];
        }

        #endregion
    }
}
