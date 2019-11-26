namespace Atari_Breakout
{
    public class Block
    {
        private readonly GraphicTool _graphicTool;

        public int X { get; } // upper left corner
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Block(int x, int y, int width, int height, GraphicTool graphicTool)
        {
            _graphicTool = graphicTool;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Show()
        {
            _graphicTool.Graphics.FillRectangle(_graphicTool.Brush, X, Y, Width, Height);
        }

        public void Delete()
        {
            _graphicTool.Graphics.FillRectangle(_graphicTool.Clear, X, Y, Width, Height);
        }


    }
}