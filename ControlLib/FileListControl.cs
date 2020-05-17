using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Libraries.UtilityLib;
using System.IO;
using Libraries.UtilityLib.Logger;

namespace Libraries.ControlLib
{
    public partial class FileListControl : UserControl
    {
        public string FileExtension { get; set; }

        protected string selectedDirectory;
        public string SelectedDirectory
        {
            get { return selectedDirectory; }
            set
            {
                selectedDirectory = value;
                UpdateUI();                
            }
        }

        public ListView.ColumnHeaderCollection Columns { get { return listView.Columns; } }
        protected ListView ListView { get { return listView; } }
        protected ContextMenuStrip ListContextMenuStrip { get { return listContextMenuStrip; } }
        
        public FileListControl()
        {
            InitializeComponent();          
        }

        protected void UpdateUI()
        {
            try
            {
                listView.Items.Clear();

                foreach (var file in Directory.GetFiles(selectedDirectory, string.Format("*.{0}", FileExtension)))
                {
                    string name = Path.GetFileName(file).Replace("."+FileExtension, string.Empty);
                    var item = new ListViewItem(name);
                    item.Tag = Path.Combine(selectedDirectory, file);
                    listView.Items.Add(item);
                }

                listView.Enabled = true;

                directoryTextBox.Text = selectedDirectory;
            }
            catch (Exception e)
            {
                listView.Enabled = false;
                LocalStaticLogger.WriteLine(e.ToString());
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (!string.IsNullOrWhiteSpace(selectedDirectory))
                {
                    dialog.SelectedPath = selectedDirectory;
                }
                else
                {
                    dialog.SelectedPath = Constants.CurrentDirectory.ToFileSystemPath();
                }

                if (dialog.ShowDialog(ParentForm) == DialogResult.OK)
                {
                    SelectedDirectory = dialog.SelectedPath;
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            ActivateSelectedListItems();
        }

        protected void ActivateSelectedListItems()
        {
            foreach (ListViewItem item in ListView.SelectedItems)
            {
                ActivateListItem(item);
            }
        }

        protected virtual void ActivateListItem(ListViewItem item)
        {
        }        
    }
}
