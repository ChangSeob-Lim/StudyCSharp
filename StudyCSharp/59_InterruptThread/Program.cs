using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _59_InterruptThread
{
    class SideTask
    {
        int count = 0;

        public SideTask(int count)
        {
            this.count = count;
        }

        public void KeepAlive()
        {
            try
            {
                Console.WriteLine("Running Thread isn't gonna be interrupted");
                //Thread.SpinWait(1_000_000_000);

                while (count > 0)
                {
                    Console.WriteLine($"{count--} left");
                    Console.WriteLine("Entering into WaitJoinSleep...");
                    Thread.Sleep(10);
                }
                Console.WriteLine("Count: 0");
            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
                Thread.ResetAbort();
            }
            catch(ThreadInterruptedException ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Clearing resource...");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SideTask task = new SideTask(100);
            Thread th = new Thread(new ThreadStart(task.KeepAlive));
            th.IsBackground = false;

            Console.WriteLine("Starting thread...");
            th.Start();

            Thread.Sleep(100);

            // 인터럽트
            //Console.WriteLine("Interrupting thread...");
            //th.Interrupt();
            
             Console.WriteLine("Waiting");
            th.Suspend();
            Thread.Sleep(1000);
            th.Resume();

            Console.WriteLine("Waiting until thread stop...");
            th.Join();

            Console.WriteLine("Done!!");
        }
    }
}
