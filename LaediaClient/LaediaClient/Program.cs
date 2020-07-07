using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaClient
{

    public class Program
    {
        private static GameClient m_gameClient = new GameClient();
        private static Stopwatch m_gameTickTimer = new Stopwatch();
        public static long FrameCounter { get => m_frameCounter; set => m_frameCounter = value; }
        private static long m_frameCounter = 0;

        static void Main(string[] args)
        {

            Console.WriteLine("Client Started");

            m_gameClient.Start();

                while (m_gameClient.Status != GameClient.ProgramStatus.PS_Quitting)
                {

                    if (m_gameClient.Status == GameClient.ProgramStatus.PS_Running)
                    {
                        if (m_gameTickTimer.ElapsedMilliseconds >= 50)
                        {
                        m_gameClient.Update((float)m_gameTickTimer.ElapsedMilliseconds / 1000.0f);
                            m_frameCounter++;
                            m_gameTickTimer.Restart();
                        }
                    }

                    if (Console.KeyAvailable)
                    {
                        var line = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                            m_gameClient.OnCommandentered(line);
                    }
                }        
        }
    }
}
