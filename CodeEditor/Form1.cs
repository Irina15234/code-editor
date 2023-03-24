using CodeEditor.styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace CodeEditor
{
    public partial class CodeEditorForm : Form
    {
        public CodeEditorForm()
        {
            InitializeComponent();
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            mainMenu.Renderer = new MenuRenderer();
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
    }
}
