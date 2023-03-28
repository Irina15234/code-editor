using CodeEditor.Properties;
using CodeEditor.styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using SystemColors = CodeEditor.styles.SystemColors;

namespace CodeEditor
{
    public partial class CodeEditorForm : Form
    {
        public SystemColors systemColors = new SystemColors();
        private string currentSyntax = "No";
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        public CodeEditorForm()
        {
            InitializeComponent();
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            mainMenu.Renderer = new MenuRenderer();

            string[] languages = { "No", "JS" };

            foreach (var item in languages)
            {
                var dropDownItem = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Name = "dropDownItem" + item,
                    Text = item,
                    ForeColor = systemColors.getRgbColor(systemColors.baseFont),
                    BackColor = systemColors.getRgbColor(systemColors.panelBG),
                    Image = item == currentSyntax ? Resources.point : null,
                    ImageScaling = ToolStripItemImageScaling.None

                };
                dropDownItem.Click += langToolStripMenuItem_Click;
                langToolStripMenuItem.DropDownItems.Add(dropDownItem);
            }
        }

        private class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new CustomMenuColorTable()) { }
        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(this.codeTextBox.Text);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fullscreenButton_Click(object sender, EventArgs e)
        {
            bool isFullscreen = this.WindowState == FormWindowState.Maximized;
            if (isFullscreen)
            {
                this.WindowState = FormWindowState.Normal;
            } else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void langToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < langToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (langToolStripMenuItem.DropDownItems[i].Text == this.currentSyntax)
                {
                    Console.WriteLine(2);
                    langToolStripMenuItem.DropDownItems[i].Image = null;
                }
            }

           
            (sender as ToolStripMenuItem).Image = Resources.point;
            this.currentSyntax = sender.ToString();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                string fileText = System.IO.File.ReadAllText(filename);
                codeTextBox.Text = fileText;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lang = this.currentSyntax == "No" ? null : this.currentSyntax;
            saveFileDialog.DefaultExt = "*." + lang;
            saveFileDialog.Filter = "Files|*." + lang;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllText(filename, codeTextBox.Text);
        }
    }
}
