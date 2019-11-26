using System.Drawing;

namespace Atari_Breakout
{
    public class GraphicTool
    {
        public Graphics Graphics { get; set; }
        public SolidBrush Brush { get; set; }
        public SolidBrush Clear { get; }

        public GraphicTool(Graphics graphics, Color backgroundColor, Color brushColor)
        {
            Graphics = graphics;
            Brush = new SolidBrush(brushColor);
            Clear = new SolidBrush(backgroundColor);
        }
    }
}