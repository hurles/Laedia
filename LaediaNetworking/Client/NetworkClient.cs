using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaClient
{
    public class NetworkClient
    {
        private NetPeerConfiguration m_config;
        private NetClient m_client;

        private NetConnection m_connection;

        public event EventHandler<(object, int)> OnConsoleMessage;

        public NetworkClient()
        {

        }
        public void ConnectToServer(string hostname, int port)
        {
            m_config = new NetPeerConfiguration("Laedia");
            m_client = new NetClient(m_config);
            m_client.Start();

            OnConsoleMessage?.Invoke(this, ("Starting connection..", (int)ConsoleColor.Green));

            m_connection = m_client.Connect(host: hostname, port: port);

        }

        public void DisconnectFromServer()
        {
            OnConsoleMessage?.Invoke(this, ("Disconnecting..", (int)ConsoleColor.Green));
            m_client?.Disconnect("Disconnecting"); 

            OnConsoleMessage?.Invoke(this, ($"{m_connection?.Status}", (int)ConsoleColor.White));
        }

        public void SendMessage(string message)
        {
            if (m_client != null)
            {
                var msg = m_client.CreateMessage();
                msg.Write(message);
                var result = m_client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public void Update(float dt)
        {
            if (m_client == null)
                return; 
            HandleMessages();
        }

        private void HandleMessages()
        {
            NetIncomingMessage message;
            while ((message = m_client.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        // handle custom messages
                        var data = message.Data;
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        switch (message.SenderConnection.Status)
                        {
                                                   
                        }

                        OnConsoleMessage?.Invoke(this, ($"Status changed to: {message.SenderConnection.Status}", (int)ConsoleColor.White));

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
