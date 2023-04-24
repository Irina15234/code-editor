using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor.Classes
{
    public class JSFormatting
    {
        public readonly string[] KeyWordsList = {
            "const",
            "var",
            "function",
            "void",
            "false",
            "true",
            "switch",
            "case",
            "default",
            "for",
            "if",
            "else",
            "break",
            "continue",
            "delete",
            "try",
            "catch",
            "finally",
            "new",
            "instanceof",
            "typeof",
            "null",
            "let",
            "console",
            "this",
            "while",
            "do",
            "in",
            "throw",
            "with",
            "enum",
            "import",
            "export",
            "debugger",
            "protected",
            "public",
            "private",
            "implements",
            "package",
            "goto",
            "super",
            "underfined",
        };

        public readonly string[] FunctionsKeyWordsList = {
            "foreEach",
            "log",
            "map",
            "filter",
            "reduce",
        };

        public Dictionary<string, Color> KeyWords = new Dictionary<string, Color>();

        public JSFormatting()
        {
            SystemColors systemColors = new SystemColors();

            foreach (var keyWord in FunctionsKeyWordsList)
            {
                KeyWords.Add('.'+keyWord, systemColors.getRgbColor(systemColors.yellow));
            }

            foreach (var keyWord in KeyWordsList)
            {
                KeyWords.Add(keyWord, systemColors.getRgbColor(systemColors.orange));
            }
        }

        public void formatting()
        {

        }
    }
}
