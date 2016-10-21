using System.Windows.Forms;

namespace DecisionDealer.View
{
    public class BufferedPanel : Panel
    {
        public BufferedPanel() : base()
        {
            DoubleBuffered = true;
        }
    }
}
