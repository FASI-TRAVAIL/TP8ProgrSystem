using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DiningPhilosophers
{
    class Sticks
    {
        bool[] stick = new bool[5]; // initially false, i.e. not used
        private object obj = new object();

        // Try to pick up the sticks with the designated numbers
        public void Get(int left, int right)
        {
            //lock (this)
            Monitor.Enter(obj);
            {
                while (stick[left] || stick[right]) Monitor.Wait(obj);
                stick[left] = true; stick[right] = true;
            }
            Monitor.Exit(obj);
            
        }

        // Lay down the forks with the designated numbers
        public void Put(int left, int right)
        {
            //lock (this)
            Monitor.Enter(obj);
            {
                stick[left] = false; stick[right] = false;
                Monitor.PulseAll(obj);
            }
            Monitor.Exit(obj);

        }
    }
}
