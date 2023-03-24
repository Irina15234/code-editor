using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeEditor.styles
{
    public class SystemColors
    {
        public string mainBG = "#2b2b2b";
        public string panelBG = "#3c3f41";
        public string panelHoverBG = "#636567";

        public string baseFont = "#a9b7c6";
    }

    public class CustomMenuColorTable : ProfessionalColorTable
    {
        private readonly SystemColors colors =  new SystemColors();

        public override Color MenuBorder
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color MenuItemBorder
        {
            get { return ColorTranslator.FromHtml(colors.panelBG); }
        }

        public override Color MenuItemSelected
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return ColorTranslator.FromHtml(colors.panelBG); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color ToolStripGradientBegin
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color ToolStripGradientEnd
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }

        public override Color ToolStripGradientMiddle
        {
            get { return ColorTranslator.FromHtml(colors.panelHoverBG); }
        }
    }

}
