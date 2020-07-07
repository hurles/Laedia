using LaediaServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaServer
{
    public class Program
    {
        private static GameServer m_gameServer;
        private static Stopwatch m_gameTickTimer = new Stopwatch();
        public static long FrameCounter { get => m_frameCounter; set => m_frameCounter = value; }
        private static long m_frameCounter = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Server Started");
            m_gameTickTimer.Start();

            m_gameServer = new GameServer();

            m_gameServer.Start();

            while (m_gameServer.Status != GameServer.ProgramStatus.PS_Quitting)
            {

                if (m_gameServer.Status == GameServer.ProgramStatus.PS_Running)
                {
                    if (m_gameTickTimer.ElapsedMilliseconds >= 50)
                    {
                        m_gameServer.Update((float)m_gameTickTimer.ElapsedMilliseconds / 1000.0f);
                        m_frameCounter++;
                        m_gameTickTimer.Restart();
                    }
                }

                if (Console.KeyAvailable)
                {
                    var line = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                        m_gameServer.OnCommandentered(line);
                }
            }
        }
    }
}
