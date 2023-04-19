using System.Windows.Forms;

namespace VarianceSimulator
{
    public class BufferedPanel : Panel
    {
        public BufferedPanel() : base()
        {
            DoubleBuffered = true;
        }
    }
}
