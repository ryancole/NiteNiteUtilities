using Topshelf;
using NiteNiteUtilities.Utility;

namespace NiteNiteUtilities
{
    class Program
    {
        static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(host =>
            {
                host.Service<OwinService>(service =>
                {
                    service.ConstructUsing(() => new OwinService());
                    service.WhenStarted(async s => await s.Start());
                    service.WhenStopped(s => s.Stop());
                });
            });

            return (int)exitCode;
        }
    }
}
