using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;


namespace SynchronizedBalls
{
    /// <summary> 
    /// </summary>
    public class BallMover
    {
        private delegate void UpdatePictureBoxCallback(Point p);

        private PictureBox pb;
        private Random rand = new Random();
        private int speed;
        private RW rw;

        public BallMover(PictureBox pb, RW rw)
        {
            this.pb = pb;
            this.rw = rw;
                       
        }

        
        public BallMover(PictureBox pb)
        {
           this.pb = pb;

        }
        
        public void RunReader()
        {

            try
            {
                speed = rand.Next(5, 10);

                while (true)
                {
                    
                    while (pb.Location.X < SynchronisationTestForm.CS_MINX)
                    {
                        MoveBall();
                        Thread.Sleep(speed);
                    }
                    rw.EnterReader();
                    while (pb.Location.X < SynchronisationTestForm.CS_MAXX)
                    {
                        MoveBall();
                        Thread.Sleep(speed);
                    }
                    rw.ExitReader();
                    
                  
                    while (pb.Location.X < SynchronisationTestForm.MAXX)
                    {
                        MoveBall();
                        Thread.Sleep(speed);
                    }
                    ResetBall();
                }
            }
            catch (ThreadInterruptedException)
            {
                ResetBall();
                return;
            }

        }

        public void RunWriter()
        {

            try
            {
                speed = rand.Next(5, 10);

                while (true)
                {
                    
                    while (pb.Location.X < SynchronisationTestForm.CS_MINX)
                    {
                        MoveBall();
                        Thread.Sleep(speed);
                    }

                    rw.EnterWriter();
                    while (pb.Location.X < SynchronisationTestForm.CS_MAXX)
                    {
                        MoveBall();
                        Thread.Sleep(speed);
                    }
                    rw.ExitWriter();

                    while (pb.Location.X < SynchronisationTestForm.MAXX)
                    {
                        MoveBall();
                        Thread.Sleep(speed);
                    }
                    ResetBall();
                }
            }
            catch (ThreadInterruptedException)
            {
                ResetBall();
                return;
            }

        }

        public void InterruptBall()
        {
            if (pb.Location.X < SynchronisationTestForm.CS_MAXX && pb.Location.X > SynchronisationTestForm.CS_MINX)
            {
                
                ResetBall();
            }
            else
            {
                ResetBall();
            }

        }

        /// <summary>
        /// This method moves the ball and returns the new location
        /// </summary>
        private void MoveBall()
        {
            Point p = pb.Location;
            p.X++;
            pb.Invoke(new UpdatePictureBoxCallback(MovePictureBox), p);
        }

        /// <summary>
        ///  This method sets the ball back to the left hand side of the white area
        /// </summary>
        private void ResetBall()
        {
            Point p = pb.Location;
            p.X = SynchronisationTestForm.MINX;
            pb.Invoke(new UpdatePictureBoxCallback(MovePictureBox), p);
        }

        private void MovePictureBox(Point p)
        {
            pb.Location = p;
        }

    }
}