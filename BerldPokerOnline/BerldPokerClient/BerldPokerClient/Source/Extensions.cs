using System.Drawing;
using System.Windows.Forms;

namespace BerldPokerClient
{
    public static class Extensions
    {
        public static void FitFont(this Control control)
        {
            while (control.Width < TextRenderer.MeasureText(control.Text, new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style)).Width)
            {
                control.Font = new Font(control.Font.FontFamily, control.Font.Size - 0.5f, control.Font.Style);
            }
        }
    }
}
