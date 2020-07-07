using LaediaServer.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaServer.Server
{
    class GameServer
    {
        public enum ProgramStatus
        {
            PS_Running,
            PS_Quitting
        }

        public ProgramStatus Status { get; set; } = ProgramStatus.PS_Running;

        private NetworkServer m_networkServer;


        internal void Start()
        {
            m_networkServer = new NetworkServer();
            m_networkServer.OnConsoleMessage += (x, message) => WriteLine(message.Item1, (ConsoleColor)message.Item2);

        }

        public GameServer()
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
            }

        }

        public void Update(float dt)
        {
            m_networkServer.Update(dt);

            OnUpdate(dt);
        }

        public void OnUpdate(float dt)
        {

        }

        public void ChangeStatus(ProgramStatus status)
        {
            Status = status;
        }

        public static void WriteLine(object text, ConsoleColor color = ConsoleColor.White)
        {    
            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] - {text}");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
