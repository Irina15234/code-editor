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
using System.IO;

namespace CodeEditor
{
    public partial class CodeEditorForm : Form
    {
        public SystemColors systemColors = new SystemColors();
        public Helper helper = new Helper();

        private string currentSyntax = "No";

        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        public CodeEditorForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            mainMenu.Renderer = new MenuRenderer();

            helper.createLangToolStripMenu(ref this.currentSyntax, this.langToolStripMenuItem, (newSyntax) => toolStripMenuClickCallback(newSyntax));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new CustomMenuColorTable()) { }
        }

        private void toolStripMenuClickCallback(string newSyntax)
        {
            this.currentSyntax = newSyntax;
            helper.resetHighlightingKeywords(codeTextBox);
            helper.highlightingKeywords(newSyntax, codeTextBox);
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

                string extension = Path.GetExtension(filename).Replace(".", string.Empty).ToUpper();
                this.currentSyntax = helper.languages.Contains(extension) ? extension : "No";
                helper.highlightingKeywords(this.currentSyntax, codeTextBox);
                helper.updateLangToolStripMenu(ref this.currentSyntax, this.langToolStripMenuItem, (newSyntax) => toolStripMenuClickCallback(newSyntax));
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

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
