using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.ControlLib.BreadCrumbProgressControl
{
    public partial class BreadCrumbStep : Label
    {       
        public bool Completed { get; set; }
        public bool Current { get; set; }

        public int CurrentBorderThickness { get; set; }
        public int NoncurrentBorderThickness { get; set; }

        protected const int defaultCurrentBorderThickness = 20;
        protected const int defaultNoncurrentBorderThickness = 10;

        public BreadCrumbStep()
        {
            InitializeComponent();
            CurrentBorderThickness = defaultCurrentBorderThickness;
            NoncurrentBorderThickness = defaultNoncurrentBorderThickness;
        }

        public BreadCrumbStep(string text)
        {
            InitializeComponent();

            CurrentBorderThickness = defaultCurrentBorderThickness;
            NoncurrentBorderThickness = defaultNoncurrentBorderThickness;

            AutoSize = false;
            TextAlign = ContentAlignment.MiddleCenter;
            Text = text;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // draw a black border
            using (Pen solidBlackPen = new Pen(Color.Black, Current ? CurrentBorderThickness : NoncurrentBorderThickness))
            {
                e.Graphics.DrawRectangle(solidBlackPen, ClientRectangle);
            }            
        }
    }
}
