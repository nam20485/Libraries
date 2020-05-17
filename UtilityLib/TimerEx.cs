using System;
using System.Timers;

namespace Libraries.UtilityLib
{
    public class TimerEx
    {
        public event ElapsedEventHandler Elapsed;

        protected Timer timer;
        protected DateTime started;
        public TimeSpan TimeRemaining { get; set; }

        public TimerEx()
        {
            TimeRemaining = TimeSpan.Zero;

            timer = new Timer();
            timer.Elapsed += HandleElapsed;
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        public bool Enabled
        { 
            get { return timer.Enabled; }
            set
            {
                if (value)
                {
                    if (TimeRemaining > TimeSpan.Zero)
                    {
                        timer.Interval = TimeRemaining.TotalMilliseconds;
                    }
                    timer.Enabled = true;
                    started = DateTime.Now;
                }
                else
                {
                    timer.Enabled = false;
                    TimeRemaining = CalculateTimeRemaining();
                }
            }
        }

        public double Interval
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                TimeRemaining = TimeSpan.Zero;               
            }
        }

        public bool AutoReset
        {
            get { return timer.AutoReset; }
            set { timer.AutoReset = value; }
        }

        protected void HandleElapsed(object sender, ElapsedEventArgs e)
        {
            if (Elapsed != null)
            {                
                TimeRemaining = TimeSpan.Zero;
                Elapsed(sender, e);
            }
        }       

        protected TimeSpan CalculateTimeRemaining()
        {
            TimeSpan transpired = DateTime.Now-started;
            var timeRemaining = TimeSpan.FromMilliseconds(timer.Interval-transpired.TotalMilliseconds);
            return timeRemaining;
        }
    }
}

