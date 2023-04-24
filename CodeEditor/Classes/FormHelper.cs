using CodeEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeEditor.Classes
{
    public partial class Helper
    {
        public SystemColors systemColors = new SystemColors();

        public Helper() { }

        public void createLangToolStripMenu(ref string currentSyntax, ToolStripMenuItem langToolStripMenuItem, Action<string> callback)
        {
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
                dropDownItem.Click += (object sender, EventArgs e) => langToolStripMenuItem_Click(sender, callback, langToolStripMenuItem);
                langToolStripMenuItem.DropDownItems.Add(dropDownItem);
            }
        }

        private static void langToolStripMenuItem_Click(object sender, Action<string> callback, ToolStripMenuItem langToolStripMenuItem)
        {
            for (var i = 0; i < langToolStripMenuItem.DropDownItems.Count; i++)
            {
                langToolStripMenuItem.DropDownItems[i].Image = null;
            }

            (sender as ToolStripMenuItem).Image = Resources.point;
            callback(sender.ToString());
        }
    }
}
