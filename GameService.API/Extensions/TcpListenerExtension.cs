using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace GameApp.Extensions
{
    public static class TcpListenerExtension
    {
        public static IServiceCollection AddTcpListener(this IServiceCollection services, IPEndPoint endPoint)
        {
            services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IConfigureOptions<KestrelServerOptions>, TcpOptionsSetup>());

            services.Configure<TcpConnectionOptions>(options => { options.EndPoint = endPoint; });
            
            services.TryAddSingleton<IMessageHandler, MessageHandler>();
            return services;
        }
    }
   
    public class TcpOptionsSetup : IConfigureOptions<KestrelServerOptions>
    {
        private readonly TcpConnectionOptions _options;

        public TcpOptionsSetup(IOptions<TcpConnectionOptions> options)
        {
            _options = options.Value;
        }
        public void Configure(KestrelServerOptions options)
        {
            options.Listen(_options.EndPoint, builder =>
            {
                builder.UseConnectionHandler<TcpConnectionHandler>();
            });
        }
    }

    public class TcpConnectionOptions
    {
        public IPEndPoint EndPoint { get; set; }
    }

    public class TcpConnectionHandler : ConnectionHandler
    {
        private readonly IMessageHandler _messageHandler;
        private PipeWriter _writer;
        private PipeReader _reader;
        public TcpConnectionHandler(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }
        
        public override async Task OnConnectedAsync(ConnectionContext connection)
        {
            _reader = connection.Transport.Input;
            _writer = connection.Transport.Output;
            
            while (true)
            {
                var result = await _reader.ReadAsync();
                var buffer = result.Buffer;

                var message = await _messageHandler.ParseMessageAsync(ref buffer);
                if (message != null)
                    await _messageHandler.ProcessMessage(message);
                
                _reader.AdvanceTo(buffer.Start, buffer.End);
            }
        }

        private async Task OnActionSend(MessageToSend<Action> messageToSend)
        {
            
        }
    }
    
}