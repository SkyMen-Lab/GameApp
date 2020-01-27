using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace GameApp.Extensions
{
    public static class TcpListenerExtension
    {
        public static IServiceCollection AddTcpListener(this IServiceCollection services, IPEndPoint endPoint)
        {
            //TODO: configure
            return services;
        }
    }
}