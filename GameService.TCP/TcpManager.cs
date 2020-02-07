﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameApp.Extensions;
using GameService.TCP.EventArgs;
using GameService.TCP.EventHandling;
using Serilog;

namespace GameService.TCP
{
    public class TcpManager : ITcpManager
    {
        private bool _isConnected;
        public bool IsConnected => _isConnected;
        private TcpListener _listener;
        private NetworkStream _gameStream;
        private StreamWriter _gameStreamWriter;
        private Task _serverTask;

        public event EventHandler<MovementReceivedEventArgs> OnMovementReceivedEvent;
        
        event EventHandler<MovementReceivedEventArgs> IEventDisposer.OnMovementReceivedEvent
        {
            add => OnMovementReceivedEvent += value;
            remove => OnMovementReceivedEvent -= value;
        }

        public TcpManager()
        {
        }

        public void StartServer(int port)
        {
            _serverTask = new Task(() => StartListening(port), TaskCreationOptions.LongRunning);
            _serverTask.Start();
        }

        public void StopServer()
        {
            //TODO: close connection
            //_gameStream?.Close();
            // _listener?.Stop();
        }

        private async void StartListening(int port)
        {
            try
            {
                _listener = TcpListener.Create(port);
                _listener.Start();
                Log.Information($"Started TCP listening on port {port}");
                while (true)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    Log.Information($"Accepted client {client.Client.LocalEndPoint}");
                    new Task(() => ListenToClient(client), TaskCreationOptions.LongRunning).Start();
                }
            }
            catch (SocketException e)
            {
                Log.Error("Error in connecting", e);
            }
        }

        private async void ListenToClient(TcpClient client)
        {
            if (client == null) return;

            try
            {
                var stream = client.GetStream();
                int length;
                do
                {
                    byte[] buffer = new byte[128];
                    length = await stream.ReadAsync(buffer, 0, buffer.Length);

                    var message = ParseMessage(buffer);
                    Log.Information("Received message " + message);
                    if (message == "game")
                    {
                        _gameStream = stream;
                        _gameStreamWriter = new StreamWriter(_gameStream);
                    }

                    await Task.Run(() => ProcessMessage(message));

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
                    await _gameStreamWriter.WriteAsync(message);
                    await _gameStreamWriter.FlushAsync();
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
            return Encoding.ASCII.GetString(buffer).Replace(" ", "");
        }

        private void ProcessMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (char.IsDigit(message[0]))
            {
                string[] content = message.Split(' ');
                int clicks = Int32.Parse(content[0]) == 1 ? 1 : -1;
                string code = content[1];
                OnMovementReceivedEvent?.Invoke(this, new MovementReceivedEventArgs(code, clicks));
            }
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