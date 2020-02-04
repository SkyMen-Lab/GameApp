using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameApp.Extensions;
using Serilog;

namespace GameService.TCP
{
    public class TcpManager : ITcpManager
    {
        private bool _isConnected;
        public bool IsConnected => _isConnected;
        private TcpListener _listener;
        private NetworkStream _gameStream;

        public TcpManager()
        {
            Thread startListeningThread = new Thread(StartListening);
            startListeningThread.Start();
        }

        private void StartListening()
        {
            try
            {
                _listener = TcpListener.Create(3434);
                _listener.Start();
                Log.Information("Started TCP listening");
                while (true)
                {
                    var client = _listener.AcceptTcpClient();
                    Log.Information($"Accepted client {client}");
                    ThreadPool.QueueUserWorkItem(ListenToClient, client);
                }
            }
            catch (SocketException e)
            {
                Log.Error("Error in connecting", e);
            }
        }

        private async void ListenToClient(object param)
        {
            var client = (TcpClient) param;

            if (client == null) return;

            try
            {
                var stream = client.GetStream();
                int length;
                Memory<byte> memoryBuffer = Memory<byte>.Empty;
                do
                {
                    length = await stream.ReadAsync(memoryBuffer);
                    byte[] buffer = memoryBuffer.ToArray();

                    if (ParseMessage(buffer) == "game")
                        _gameStream = stream;

                } while (length > 0);

            }
            catch (SocketException e)
            {
                Log.Error("Error accepting the client", e);
            }
        }
        
        

        public async Task SendMessageAsync(string message)
        {
            if (_gameStream != null)
            {
                try
                {
                    var streamWriter = new StreamWriter(_gameStream);
                    await streamWriter.WriteAsync(message);
                    await streamWriter.FlushAsync();
                }
                catch (SocketException e)
                {
                    Log.Error("Error during sending a message", e);
                }
            }
        }

        private string ParseMessage(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) return "";
            return Encoding.Unicode.GetString(buffer);
        }

        public Task ConnectAsync(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }
    }
}