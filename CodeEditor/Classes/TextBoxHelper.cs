using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CodeEditor.Classes
{
    public partial class Helper
    {
        public void codeTextBoxHandler(RichTextBox textBox, ListBox wordsListBox, Panel keyWordsPanel, string prevText)
        {
            if (keyWords.Count > 0)
            {
                ChangedText changedText = getNewWord(prevText, textBox);
                if (changedText.newWord.Length > 0)
                {
                    defineKeyWordsPanel(keyWordsPanel, wordsListBox, changedText.newWord);
                    wordsListBox.MouseClick += (object sender, MouseEventArgs e) => {
                        wordsListBox_Click(sender, changedText, textBox);
                        resetKeyWordsPanel(wordsListBox, keyWordsPanel);
                    };
                } else
                {
                    defineKeyWordsPanel(keyWordsPanel, wordsListBox, "");
                }
            }
        }

        private static void wordsListBox_Click(object sender, ChangedText changedText, RichTextBox textBox)
        {
            string currentWord = (sender as ListBox).SelectedItem.ToString();

            string newRow = textBox.Lines[changedText.rowIndex].ToString();
            int startReolaceIndex = changedText.charIndex + 1 - changedText.newWord.Length;

            var lines = textBox.Lines;
            lines[changedText.rowIndex] = newRow.Remove(startReolaceIndex, changedText.newWord.Length).Insert(startReolaceIndex, currentWord);
            textBox.Lines = lines;
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
            if (prevTextLines.Length - 1 < rowIndex)
            {
                return Math.Abs(prevTextLines.Length - textBox.Lines.Length) > 1 ? -1 : 0;
            }
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

        private ChangedText getNewWord(string prevText, RichTextBox textBox)
        {
            if (textBox.Lines.Length == 0)
            {
                return new ChangedText();
            }

            string[] prevTextLines = prevText.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int rowIndex = getChangedRowIndex(prevTextLines, textBox);
            int charIndex = getChangedCharIndex(prevTextLines, textBox, rowIndex);

            if (charIndex < 0)
            {
                return new ChangedText();
            }
            if (charIndex == 0)
            {
                return new ChangedText(textBox.Text[charIndex].ToString(), rowIndex, charIndex);
            }

            string newWord = "";
            int startIndex = prevTextLines[rowIndex].Length > textBox.Lines[rowIndex].Length ? charIndex - 1 : charIndex;
            for (int i = startIndex; i > -1; i--)
            {
                if (!Regex.IsMatch(textBox.Lines[rowIndex][i].ToString(), "^[a-zA-Z ]"))
                {
                    return new ChangedText();
                }
                if (textBox.Lines[rowIndex][i] != ' ')
                {
                    newWord = textBox.Lines[rowIndex][i] + newWord;
                }
            }

            return new ChangedText(newWord, rowIndex, charIndex);
        }
    }
}
