using CodeEditor.Properties;
using CodeEditor.Classes;
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
using SystemColors = CodeEditor.Classes.SystemColors;
using System.Text.RegularExpressions;

namespace CodeEditor
{
    public partial class CodeEditorForm : Form
    {
        public SystemColors systemColors = new SystemColors();
        public Helper helper = new Helper();

        private string currentSyntax = "No";

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        public CodeEditorForm()
        {
            InitializeComponent();
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            mainMenu.Renderer = new MenuRenderer();

            helper.createLangToolStripMenu(ref this.currentSyntax, this.langToolStripMenuItem, (newSyntax) => toolStripMenuClickCallback(newSyntax));
        }

        private class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new CustomMenuColorTable()) { }
        }

        private void toolStripMenuClickCallback(string newSyntax)
        {
            this.currentSyntax = newSyntax;
        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            helper.highlightingKeywords(currentSyntax, codeTextBox);
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
