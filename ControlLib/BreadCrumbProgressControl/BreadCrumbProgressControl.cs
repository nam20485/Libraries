using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.ControlLib.BreadCrumbProgressControl
{
    public partial class BreadCrumbProgressControl : UserControl
    {
        protected List<BreadCrumbStep> steps = new List<BreadCrumbStep>();

        public Color CompletedStepBackColor { get; set; }
        public Color UncompletedStepBackColor { get; set; }
        public Color StepTextColor { get; set; }

        public bool ShowLinks { get; set; }
        public bool ArrowLinks { get; set; }
        public bool BlinkCurrentStep { get; set; }

        public int StepBoxWidth { get; set; }
        public int StepBoxHorizontalMargin { get; set; }
        public int StepBoxVerticalMargin { get; set; }

        protected Timer blinkCurrentTimer;

        public BreadCrumbProgressControl()
        {
            InitializeComponent();

            blinkCurrentTimer = new Timer(components);
            blinkCurrentTimer.Interval = 500;
            blinkCurrentTimer.Tick += blinkCurrentTimer_Tick;                   
        }

        private void blinkCurrentTimer_Tick(object sender, EventArgs e)
        {
            OnBlinkCurrentTimerTick();
        }

        private void OnBlinkCurrentTimerTick()
        {
            blinkCurrentTimer.Stop();

            if (BlinkCurrentStep)
            {
                foreach (var step in steps)
                {
                    if (step.Current)
                    {
                        if (step.ForeColor != StepTextColor)
                        {
                            step.ForeColor = StepTextColor;
                        }
                        else
                        {
                            if (step.Completed)
                            {
                                step.ForeColor = CompletedStepBackColor;
                            }
                            else
                            {
                                step.ForeColor = UncompletedStepBackColor;
                            }
                        }

                        blinkCurrentTimer.Start();
                        break;
                    }
                }
            }            
        }

        public void AddSteps(string[] steps)
        {
            foreach (var s in steps)
            {
                AddStep(s);
            }

            CenterSteps();
        }

        protected int AddStep(string text)
        {
            int right = steps.Count * (StepBoxWidth + StepBoxHorizontalMargin);            
            if (steps.Count > 0)
            {
                // create arrow
                PictureBox arrowPictureBox = new PictureBox();
                arrowPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                arrowPictureBox.Size = new Size(StepBoxHorizontalMargin, ClientSize.Height);
                arrowPictureBox.Location = new Point(right - StepBoxHorizontalMargin, 0);
                if (ArrowLinks)
                {                    
                    arrowPictureBox.Image = Libraries.ControlLib.Resource.arrowLink;
                }
                else
                {                   
                    arrowPictureBox.Image = Libraries.ControlLib.Resource.barLink;
                }                
                arrowPictureBox.Visible = ShowLinks;
                Controls.Add(arrowPictureBox);
            }            

            // create the step
            BreadCrumbStep newStep = new BreadCrumbStep(text);           
            newStep.Size = new Size(StepBoxWidth, ClientSize.Height - 2 * StepBoxVerticalMargin);
            newStep.Location = new Point(right, StepBoxVerticalMargin);
            Controls.Add(newStep);            

            steps.Add(newStep);  
            UpdateStepProperties(newStep);            
           
            return steps.IndexOf(newStep);
        }

        public void SetStepCompleted(int index, bool completed)
        {
            if (steps.Count > index)
            {
                steps[index].Completed = completed;
                UpdateStepProperties(steps[index]);
            }            
        }

        public void SetStepCompleted(string text, bool completed)
        {
            var s = GetStepByText(text);
            if (s != null)
            {
                SetStepCompleted(steps.IndexOf(s), completed);
            }
        }

        public void SetCurrentStep(int index)
        {
            BreadCrumbStep newCurrentStep = null;
            if (steps.Count > index)
            {
                newCurrentStep = steps[index];
                foreach (BreadCrumbStep step in steps)
                {
                    if (step == newCurrentStep)
                    {
                        step.Current = true;
                        if (BlinkCurrentStep)
                        {
                            blinkCurrentTimer.Start();
                        }                        
                    }
                    else
                    {
                        step.Current = false;
                    }

                    UpdateStepProperties(step);
                }               
            }
        }

        public void SetCurrentStep(string text)
        {
            var s = GetStepByText(text);
            if (s != null)
            {
                SetCurrentStep(steps.IndexOf(s));
            }            
        }

        private BreadCrumbStep GetStepByText(string text)
        {
            BreadCrumbStep ret = null;
            foreach (var step in steps)
            {
                if (step.Text.CompareTo(text) == 0)
                {
                    ret = step;
                    break;
                }
            }
            return ret;
        }

        private void UpdateStepProperties()
        {
            foreach (BreadCrumbStep step in steps)
            {
                UpdateStepProperties(step);
            }
        }

        private void UpdateStepProperties(BreadCrumbStep step)
        {
            blinkCurrentTimer.Stop();

            if (step.Completed)
            {
                step.BackColor = CompletedStepBackColor;
                step.ForeColor = StepTextColor;               
            }
            else
            {
                step.BackColor = UncompletedStepBackColor;               
            }

            if (step.Current)
            {
                step.Font = new Font(Font, FontStyle.Bold);
            }
            else
            {
                step.Font = new Font(Font, FontStyle.Regular);
                // the blink timer was leaving the forecolor as the background color that
                //  is used to "hide" the text for the off portion of the blink
                step.ForeColor = StepTextColor;
            }

            blinkCurrentTimer.Start();
        }

        public void CenterSteps()
        {
            int width = steps.Count * StepBoxWidth + (steps.Count - 1) * StepBoxHorizontalMargin;
            int shim = (ClientSize.Width - width)/2;
            foreach (Control c in Controls)
            {
                c.Left = c.Left + shim;
            }
        }
    }
}
