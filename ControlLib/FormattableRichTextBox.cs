using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.ControlLib
{
    public partial class FormattabeRichTextBox : RichTextBox
    {
        protected List<Selection> selections = new List<Selection>();
        private Timer blinkTimer;
        //private Timer throbTimer;

        public string EmptyText { get; set; }
        public Color EmptyTextColor = SystemColors.ControlLight;
       
        public bool ThrobbingBlink { get; set; }
        
        public int BlinkInterval
        {
            get { return blinkTimer.Interval; }
            set { blinkTimer.Interval = value;  }
        }

        public FormattabeRichTextBox()
        {
            InitializeComponent();
           
            //throbTimer = new Timer(components);
            //throbTimer.Interval = 1;
            //throbTimer.Tick += throbTimer_Tick;
           
            blinkTimer = new Timer(components);
            blinkTimer.Tick += blinkTimer_Tick;            
        }

        //void throbTimer_Tick(object sender, EventArgs e)
        //{
        //    HandleThrobTimer();
        //}
        
        //private void HandleThrobTimer()
        //{
        //    throbTimer.Stop();

        //    foreach (Selection selection in selections)
        //    {
        //        if (selection.Blink)
        //        {
        //            int tick = 1;
        //            Color color;
        //            if (selection.Fading)
        //            {
        //                int r = SelectionColor.R;
        //                if (r < BackColor.R)
        //                {
        //                    r+=tick;
        //                }
        //                int g = SelectionColor.G;
        //                if (g < BackColor.G)
        //                {
        //                    g += tick;
        //                }
        //                int b = SelectionColor.B;
        //                if (b < BackColor.B)
        //                {
        //                    b += tick;
        //                }
        //                color = Color.FromArgb(SelectionColor.A, r, g, b);
        //                if (color.ToArgb() == BackColor.ToArgb())
        //                {
        //                    selection.Fading = false;
        //                }                                            
        //            }
        //            else
        //            {
        //                int r = SelectionColor.R;
        //                if (r > selection.ForeColor.R)
        //                {
        //                    r-=tick;
        //                }
        //                int g = SelectionColor.G;
        //                if (g > selection.ForeColor.G)
        //                {
        //                    g -= tick;
        //                }
        //                int b = SelectionColor.B;
        //                if (b > selection.ForeColor.B)
        //                {
        //                    b -= tick;
        //                }
        //                color = Color.FromArgb(SelectionColor.A, r, g, b);
        //                if (color.ToArgb() == selection.ForeColor.ToArgb())
        //                {
        //                    selection.Fading = true;
        //                }         
        //            }
        //            SetTextColor(selection.Begin, selection.Length, color);
        //        }
        //    }

        //    throbTimer.Start();
        //}        

        void blinkTimer_Tick(object sender, EventArgs e)
        {
            HandleBlinkTimer();
        }

        // set text to BackColor on next timer tick to hide text
        private static bool blinkOff = true;
        private void HandleBlinkTimer()
        {
            blinkTimer.Stop();

            foreach (Selection selection in selections)
            {
                if (selection.Blink)
                {
                    SetTextColor(selection.Begin, selection.Length, blinkOff ? BackColor : selection.ForeColor);
                }
            }          

            blinkOff = !blinkOff;
            blinkTimer.Start();
        }

        public new void AppendText(string text)
        {
            AppendText(text, ForeColor, false);
        }

        public void AppendText(string text, Color foreColor, bool blink)
        {            
            int start = TextLength;
            base.AppendText(text);
            int end = TextLength;

            Selection selection = new Selection(start, end - start, foreColor, blink);
            selections.Add(selection);

            SetTextColor(selection.Begin, selection.Length, selection.ForeColor);

            // if this new text is blinking, start the timer
            if (blink)
            {
                //if (ThrobbingBlink)
                //{
                //    throbTimer.Start();
                //}
                //else
                //{
                    blinkOff = true;
                    blinkTimer.Start();                
                //}                
            }            
        }

        public void SetTextColor(int begin, int length, Color color)
        {
            Select(begin, length);
            SelectionColor = color;            
            SelectionLength = 0;
        }

        public new void Clear()
        {
            selections.Clear();
            base.Clear();
        }

        public void RemoveLastLine()
        {
            if (selections.Count > 0)
            {
                Selection selection = selections[selections.Count-1];
                Text = Text.Remove(selection.Begin, selection.Length);                
                selections.Remove(selection);
            }
        }

        protected void UpdateTextColor()
        {
            foreach (Selection selection in selections)
            {
                SetTextColor(selection.Begin, selection.Length, selection.ForeColor);
            }
        }
      
        protected void ClearBlinking()
        {
            foreach (Selection selection in selections)
            {
                selection.Blink = false;              
            }

            UpdateTextColor();

            blinkTimer.Stop();
            //if (ThrobbingBlink)
            //{
            //    throbTimer.Stop();
            //}            
        }        

        protected class Selection
        {
            public int Begin { get; private set; }
            public int Length { get; private set; }
            public Color ForeColor { get; set; }
            public bool Blink { get; set; }
            //public bool Fading { get; set; }

            public Selection(int begin, int length, Color foreColor, bool blink)                
            {
                Begin = begin;
                Length = length;
                ForeColor = foreColor;
                Blink = blink;
                //Fading = true;
            }
        }
    }
}
