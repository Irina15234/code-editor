using CodeEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public void updateLangToolStripMenu(ref string currentSyntax, ToolStripMenuItem langToolStripMenuItem, Action<string> callback)
        {
            langToolStripMenuItem.DropDownItems.Clear();
            this.createLangToolStripMenu(ref currentSyntax, langToolStripMenuItem, callback);
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

        public void defineKeyWordsPanel(Panel keyWordsPanel, ListBox wordsListBox, string text)
        {
            //keyWordsPanel.Location = new System.Drawing.Point(12, 12);
            if (text.Length == 0)
            {
                keyWordsPanel.Visible = false;
                return;
            }

            wordsListBox.Items.Clear();

            foreach (var keyWord in keyWords)
            {
                Regex regex = new Regex("^" + text);
                bool isFinishedText = keyWord.Key.Length == text.Length;

                if (isFinishedText && regex.IsMatch(keyWord.Key))
                {
                    keyWordsPanel.Visible = false;
                    return;
                }

                if (regex.IsMatch(keyWord.Key))
                {
                    wordsListBox.Items.Add(keyWord.Key);
                }
            }

            if (wordsListBox.Items.Count > 0)
            {
                keyWordsPanel.Visible = true;
            } else
            {
                keyWordsPanel.Visible = false;
            }
            
        }
    }
}
