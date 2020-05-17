using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Libraries.UnitLib;
using Libraries.UtilityLib;
using Libraries.UtilityLib.Reflection;


namespace Libraries.UnitLib
{
    public partial class UnitConverterControl : UserControl
    {
        protected const decimal defaultSourceAmount = 1.0m;

        public UnitConverterControl()
        {
            InitializeComponent();

            var units = Reflection.GetInheritedClasses<Unit>();
            if (units != null)
            {
                unitTypeComboBox.DataSource = units;
                unitTypeComboBox.DisplayMember = "Name";
            }
        }

        private void unitTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type type = unitTypeComboBox.SelectedValue as Type;
            if (type != null)
            {
                Unit[] sourceUnits = Reflection.GetInheritedClassInstances<Unit>(type);
                if (sourceUnits != null)
                {
                    sourceUnitComboBox.DataSource = sourceUnits;
                    sourceUnitComboBox.DisplayMember = "Name";
                }

                Unit[] destUnits = Reflection.GetInheritedClassInstances<Unit>(type);
                if (destUnits != null)
                {
                    destUnitComboBox.DataSource = destUnits;
                    destUnitComboBox.DisplayMember = "Name";
                }                                                                    
            }

            sourceMaskedTextBox.Text = defaultSourceAmount.ToString();
        }

        protected void UpdateResultMaskedTextBox()
        {
            Unit sourceUnit = sourceUnitComboBox.SelectedValue as Unit;
            if (sourceUnit != null)
            {
                Unit destUnit = destUnitComboBox.SelectedValue as Unit;
                if (destUnit != null)
                {
                    decimal sourceAmount = 0.0m;
                    if (decimal.TryParse(sourceMaskedTextBox.Text, out sourceAmount))
                    {
                        decimal converted = sourceUnit.Convert(sourceAmount, destUnit);
                        resultMaskedTextBox.Text = converted.ToString();
                    }                    
                }
            }            
        }

        private void destUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResultMaskedTextBox();
        }              

        private void sourceUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResultMaskedTextBox();
        }

        private void sourceMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateResultMaskedTextBox();
        }
    }
}
