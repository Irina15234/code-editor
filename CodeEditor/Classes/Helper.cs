using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CodeEditor.Classes
{
    public partial class Helper
    {
        public readonly string[] languages = { "No", "JS" };
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

        public void codeTextBoxHandler(RichTextBox textBox, ListBox wordsListBox, Panel keyWordsPanel, string prevText)
        {
            if (keyWords.Count > 0)
            {
                string newWord = getNewWord(prevText, textBox);
                if (newWord.Length > 0)
                {
                    defineKeyWordsPanel(keyWordsPanel, wordsListBox, newWord);
                } else
                {
                    defineKeyWordsPanel(keyWordsPanel, wordsListBox, "");
                }
            }
        }

        public void resetKeyWordsPanel(ListBox wordsListBox, Panel keyWordsPanel)
        {
            defineKeyWordsPanel(keyWordsPanel, wordsListBox, "");
        }

        private int getChangedRowIndex(string[] prevTextLines, RichTextBox textBox)
        {
            for (int i = 0; i < Math.Min(prevTextLines.Length, textBox.Lines.Count()); i++)
            {
                if (prevTextLines[i] != textBox.Lines[i])
                {
                    return i;
                }
            }

            return Math.Max(textBox.Lines.Length - 1, 0);
        }

        private int getChangedCharIndex(string[] prevTextLines, RichTextBox textBox, int rowIndex)
        {
            string prevRow = prevTextLines[rowIndex];
            string row = textBox.Lines[rowIndex];

            if (Math.Abs(prevRow.Length - row.Length) > 1)
            {
                return -1;
            }

            for (int i = 0; i < Math.Min(prevRow.Length, row.Length); i++)
            {
                if (prevRow[i] != row[i])
                {
                    return i;
                }
            }

            return Math.Max(prevRow.Length, row.Length) - 1;
        }

        private string getNewWord(string prevText, RichTextBox textBox)
        {
            if (textBox.Lines.Length == 0)
            {
                return "";
            }

            string[] prevTextLines = prevText.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int rowIndex = getChangedRowIndex(prevTextLines, textBox);
            int charIndex = getChangedCharIndex(prevTextLines, textBox, rowIndex);

            if (charIndex < 0)
            {
                return "";
            }
            if (charIndex == 0)
            {
                return textBox.Text[charIndex].ToString();
            }

            string newWord = "";
            int startIndex = prevTextLines[rowIndex].Length > textBox.Lines[rowIndex].Length ? charIndex - 1 : charIndex;
            for (int i = startIndex; i > -1; i--)
            {
                if (textBox.Lines[rowIndex][i] != ' ')
                {
                    newWord = textBox.Lines[rowIndex][i] + newWord;
                }
            }

            return newWord;
        }
    }
}
