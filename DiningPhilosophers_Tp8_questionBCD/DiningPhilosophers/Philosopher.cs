using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiningPhilosophers
{
    class Philosopher
    {
        private const int STEPS = 20;

        private int rightStick;
        private int leftStick;

        private Sticks stick = new Sticks();

        private Form form;

        public Philosopher(int right, int left,Sticks stick, Form form)
        {
            this.percentageTakenLeft = 0;
            this.percentageTakenRight = 0;
            this.rightStick = right;
            this.leftStick = left;
            this.stick = stick;
            this.form = form;
        }

        public enum state { Thinking, Hungry, Eating, LeftStickTaken, RightStickTaken };
        private state philstate;
        public state State
        {
            get
            {
                return philstate;
            }
            set
            {
                philstate = value;
                form.Invalidate();
            }
        }

        private double percentageTakenLeft;
        public double PercentageTakenLeft
        {
            get
            {
                return percentageTakenLeft;
            }
            set
            {
                percentageTakenLeft = value;
                form.Invalidate();
            }
        }

        private double percentageTakenRight;
        public double PercentageTakenRight
        {
            get
            {
                return percentageTakenRight;
            }
            set
            {
                percentageTakenRight = value;
                form.Invalidate();
            }
        }

        public void Run()
        {
            while (true)
            {
                
                // Think for a few seconds
                State = state.Thinking;
                Thread.Sleep(2000);

                // Stop thinking, get hungry
                State = state.Hungry;

                // Take both sticks
                //rightStick.WaitOne();
                stick.Get(leftStick, rightStick);
                State = state.Eating;
                TakeBothSticks();

                // Eat for a few seconds
                Thread.Sleep(2000);

                // Return both sticks                           
                stick.Put(leftStick, rightStick);
                ReturnBothSticks();

            }
        }

        void TakeLeftStick()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenLeft += 1.0 / STEPS;
            }
        }

        void TakeRightStick()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenRight += 1.0 / STEPS;
            }
        }

        void ReturnLeftStick()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenLeft -= 1.0 / STEPS;
            }
        }

        void ReturnRightStick()
        {
            for (int i = 0; i < STEPS; i++)
            {
                Thread.Sleep(100);
                PercentageTakenRight -= 1.0 / STEPS;
            }
        }

        void TakeBothSticks()
        {
            
            for (int i = 0; i < STEPS; i++)
            {

                Thread.Sleep(100);
                //stick.Get(leftStick, rightStick);
                PercentageTakenLeft += 1.0 / STEPS;
                PercentageTakenRight += 1.0 / STEPS;
            }
            
        }

        void ReturnBothSticks()
        {
            
            for (int i = 0; i < STEPS; i++)
            {               
                Thread.Sleep(100);
                //stick.Put(leftStick, rightStick);
                PercentageTakenLeft -= 1.0 / STEPS;
                PercentageTakenRight -= 1.0 / STEPS;
            }
        }
    }
}
