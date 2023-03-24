using CodeEditor.styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CodeEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
