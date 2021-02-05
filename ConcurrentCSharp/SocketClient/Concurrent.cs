using System;
using System.Threading;
using Sequential;

namespace Concurrent
{
    public class ConcurrentClient : SimpleClient
    {
        public Thread workerThread;

        public ConcurrentClient(int id, Setting settings) : base(id, settings)
        {
            // todo [Assignment]: implement required code
            prepareClient();
        }
        public void run()
        {
            // todo [Assignment]: implement required code
            workerThread = new Thread(this.communicate);
            workerThread.Start();
        }
    }
    public class ConcurrentClientsSimulator : SequentialClientsSimulator
    {
        private ConcurrentClient[] clients;

        public ConcurrentClientsSimulator() : base()
        {
            Console.Out.WriteLine("\n[ClientSimulator] Concurrent simulator is going to start with {0}", settings.experimentNumberOfClients);
            clients = new ConcurrentClient[settings.experimentNumberOfClients];
        }

        public void ConcurrentSimulation()
        {
            try
            {
                for(int i = 0; i < clients.Length; i++)
                {
                    clients[i] = new ConcurrentClient(i, settings);
                    clients[i].run();
                }

                for(int i = 0; i < clients.Length; i++)
                {
                    clients[i].workerThread.Join();
                }

                Console.Out.WriteLine("\n[ClientSimulator] All clients finished with their communication ...");

                Thread.Sleep(settings.delayForTermination);

                SimpleClient endClient = new SimpleClient(-1, settings);
                endClient.prepareClient();

                endClient.communicate();

            }
            catch (Exception e)
            { Console.Out.WriteLine("[Concurrent Simulator] {0}", e.Message); }
        }
    }
}
