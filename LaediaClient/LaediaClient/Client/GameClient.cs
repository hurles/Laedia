using LaediaClient.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaClient
{
    class GameClient
    {
        public enum ProgramStatus
        {
            PS_Running,
            PS_Quitting
        }

        public ProgramStatus Status { get; set; } = ProgramStatus.PS_Running;

        private NetworkClient m_networkClient;

        public GameClient()
        {
            
        }

        public void Start()
        {

        }

        public void Update(float dt)
        {
            m_networkClient?.Update(dt);

            OnUpdate(dt);
        }

        public void OnUpdate(float dt)
        {

        }

        internal void OnCommandentered(string line)
        {
            var enteredCommand = CommandSystem.GetCommandData(line);

            if (enteredCommand == null)
                return;

            switch (enteredCommand.CommandType)
            {
                case CommandTypes.CT_Quit:
                    ChangeStatus(ProgramStatus.PS_Quitting);
                    break;
                case CommandTypes.CT_Connect:
                    m_networkClient = new NetworkClient();
                    m_networkClient.OnConsoleMessage += (x, message) => WriteLine(message.Item1, (ConsoleColor)message.Item2);
                    m_networkClient.ConnectToServer("127.0.0.1", 8926);
                    break;
                case CommandTypes.CT_Disconnect:
                    m_networkClient?.DisconnectFromServer();
                    break;
                case CommandTypes.CT_SendString:
                    m_networkClient.SendMessage("Hello server");
                    break;
            }
            
        }

        public void ChangeStatus(ProgramStatus status)
        {
            Status = status;
        }

        public static void WriteLine(object text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] - {text.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
