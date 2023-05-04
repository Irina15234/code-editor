namespace CodeEditor.Classes
{
    public class ChangedText
    {
        public readonly string newWord;
        public readonly int rowIndex;
        public readonly int charIndex;

        public ChangedText()
        {
            this.newWord = "";
            this.rowIndex = 0;
            this.charIndex = 0;
        }

        public ChangedText(string newWord, int rowIndex, int charIndex)
        {
            this.newWord = newWord;
            this.rowIndex = rowIndex;
            this.charIndex = charIndex;
        }
    }
}
