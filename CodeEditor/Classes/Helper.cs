using CodeEditor.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeEditor.Classes
{
    public partial class Helper
    {
        public string[] languages = { "No", "JS" };
        public Dictionary<string, Color> keyWords = new Dictionary<string, Color>();
        public JSFormatting jsFormatting = new JSFormatting();

        public void highlightingKeywords(string currentSyntax, RichTextBox textBox)
        {
            switch (currentSyntax)
            {
                case "JS":
                    keyWords = jsFormatting.KeyWords;
                    break;
                default:
                    keyWords = new Dictionary<string, Color>();
                    break;
            }

            int selectionStart = textBox.SelectionStart;
            foreach (KeyValuePair<string, Color> keyValue in keyWords)
            {
                MatchCollection allKeyWords = Regex.Matches(textBox.Text, keyValue.Key);
                foreach (Match findKeyWord in allKeyWords)
                {
                    textBox.SelectionStart = findKeyWord.Index;
                    textBox.SelectionLength = findKeyWord.Length;
                    textBox.SelectionColor = keyValue.Value;
                }
            }

            textBox.SelectionStart = selectionStart;
            textBox.SelectionLength = 0;
            textBox.SelectionColor = textBox.ForeColor;
        }

        public void resetHighlightingKeywords(RichTextBox textBox)
        {
            int selectionStart = textBox.SelectionStart;

            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
            textBox.SelectionColor = textBox.ForeColor;

            textBox.SelectionStart = selectionStart;
        }
    }
}
