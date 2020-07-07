using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaServer.Networking
{
    public class NetworkServer
    {
        private NetPeerConfiguration m_config;
        private NetServer m_server;

        public event EventHandler<(object, int)> OnConsoleMessage;

        public NetworkServer()
        {
            m_config = new NetPeerConfiguration("Laedia")
            { Port = 8926 };
            m_server = new NetServer(m_config);
            m_server.Start();

            OnConsoleMessage?.Invoke(this, ("Starting server", (int)ConsoleColor.Green));
        }


        public void Update(float dt)
        {
            if (m_server == null)
                return;

            HandleMessages();
        }

        private void HandleMessages()
        {
            NetIncomingMessage message;
            while ((message = m_server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        // handle custom messages
                        var data = message.Data;
                        OnConsoleMessage?.Invoke(this, ($"Custom message received of type {message.MessageType}", (int)ConsoleColor.White));
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        switch (message.SenderConnection.Status)
                        {
                            case NetConnectionStatus.None:
                                break;
                            case NetConnectionStatus.InitiatedConnect:
                                OnConsoleMessage?.Invoke(this, ($"Initiated connection for {message.SenderEndPoint}", (int)ConsoleColor.DarkGreen));
                                break;
                            case NetConnectionStatus.ReceivedInitiation:
                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                break;
                            case NetConnectionStatus.RespondedConnect:
                                break;
                            case NetConnectionStatus.Connected:
                                OnConsoleMessage?.Invoke(this, ($"Connected {message.SenderEndPoint} - {message.SenderConnection}", (int)ConsoleColor.Green));
                                break;
                            case NetConnectionStatus.Disconnecting:
                                OnConsoleMessage?.Invoke(this, ($"Disconnecting {message.SenderEndPoint} - {message.SenderConnection}", (int)ConsoleColor.DarkYellow));
                                break;
                            case NetConnectionStatus.Disconnected:
                                OnConsoleMessage?.Invoke(this, ($"Disconnected {message.SenderEndPoint} - {message.SenderConnection}", (int)ConsoleColor.DarkYellow));
                                break;
                                /* .. */

                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        // handle debug messages
                        // (only received when compiled in DEBUG mode)
                        OnConsoleMessage?.Invoke(this, (message.ReadString(), (int)ConsoleColor.White));
                        break;

                    /* .. */
                    default:
                        OnConsoleMessage?.Invoke(this, ("unhandled message with type: "
                            + message.MessageType, (int)ConsoleColor.White));
                        break;
                }
            }
        }
    }
}
