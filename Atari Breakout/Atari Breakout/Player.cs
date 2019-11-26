using System;

namespace Atari_Breakout
{
    public class Player
    {
        private GraphicTool _graphicTool;

        public int X { get; set; } // center
        public int Y { get; set; } // top
        public int Width { get; }
        public int Height { get; }
        public GraphicTool GraphicTool
        {
            set => _graphicTool = value;
        }

        public Player(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Show()
        {
            if (_graphicTool == null)
            {
                throw new NullReferenceException("_graphicTools is not set yet");
            }
            _graphicTool.Graphics.FillRectangle(_graphicTool.Brush, X - Width / 2, Y, Width, Height);
        }

        public void Delete(int pictureBoxWidth)
        {
            _graphicTool.Graphics.FillRectangle(_graphicTool.Clear, 0, Y, pictureBoxWidth, Height);
            // because of time difference between Mouse_Move method and timer1_Tick method(Show() is called
            // in both of them) it's best to just delete whole area where player can be, rather than
            // storing old coordinates and deleting them
        }

    }
}