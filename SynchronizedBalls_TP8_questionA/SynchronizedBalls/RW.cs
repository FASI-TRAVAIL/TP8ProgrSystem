using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SynchronizedBalls
{
    public class RW
    {
        private int ReadersInCS;
        private int WritersInCS;
        private readonly object RWLock = new object();

        public RW()
        {
            
        }

        public void EnterReader()
        {

            lock (RWLock)
            {

                while (WritersInCS == 1)
                {
                    Monitor.Wait(RWLock);
                }
                    
                ReadersInCS++;

            }

        }

        public void ExitReader()
        {

            lock (RWLock)
            {

                ReadersInCS--;

                if (ReadersInCS == 0)

                    Monitor.Pulse(RWLock);

            }

        }

        public void EnterWriter()
        {
            lock (RWLock)
            {

                while (WritersInCS == 1  || ReadersInCS > 0)
                {
                    Monitor.Wait(RWLock);
                }

                
                WritersInCS++;
   

            }

        }

        public void ExitWriter()
        {

            lock (RWLock)
            {

                WritersInCS--;

                Monitor.PulseAll(RWLock);

            }
 
        }



    }





}
