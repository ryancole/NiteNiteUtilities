﻿using Topshelf;
using NiteNiteUtilities.Utility;

namespace NiteNiteUtilities
{
    class Program
    {
        static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(host =>
            {
                host.Service<OwinService>();
            });

            return (int)exitCode;
        }
    }
}
