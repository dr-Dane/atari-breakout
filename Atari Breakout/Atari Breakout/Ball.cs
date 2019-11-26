using System;
using System.Drawing;

namespace Atari_Breakout
{
    public class Ball
    {
        private int _previousX;
        private int _previousY;
        private GraphicTool _graphicTool;

        public int X { get; private set; } //coordinates of center of the ball
        public int Y { get; private set; }
        public int dX { get; set; } // value that is added to X in order for ball to move
        public int dY { get; set; }
        public int R { get; }
        public GraphicTool GraphicTool
        {
            set => _graphicTool = value;
        }

        public Ball(Random random, int x, int y, int r)
        {
            X = x;
            _previousX = x;
            Y = y;
            R = r;
            _previousY = y;
            dX = random.Next(-8, 9);
            dY = -random.Next(6, 9);
        }

        public void Move()
        {
            _previousX = X;
            _previousY = Y;
            X += dX;
            Y += dY;
        }

        public void Show()
        {
            if (_graphicTool == null)
            {
                throw new NullReferenceException("_graphicTools is not set yet");
            }
            _graphicTool.Graphics.FillEllipse(_graphicTool.Clear, _previousX - R, _previousY - R, 2 * R, 2 * R); // deleting previous position
            _graphicTool.Graphics.FillEllipse(_graphicTool.Brush, X - R, Y - R, 2 * R, 2 * R);
        }

        private float Distance(Point a, Point b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public bool BlockHit(Block block)
        {
            // CORNERS
            // top left
            if (Distance(new Point(X, Y), new Point(block.X, block.Y)) <= R)
            {
                dX = -Math.Abs(dX);
                dY = -Math.Abs(dY);
                return true;
            }
            // bottom left
            if (Distance(new Point(X, Y), new Point(block.X, block.Y + block.Height)) <= R)
            {
                dX = -Math.Abs(dX);
                dY = Math.Abs(dY);
                return true;
            }
            // bottom right
            if (Distance(new Point(X, Y), new Point(block.X + block.Width, block.Y + block.Height)) <= R)
            {
                dX = Math.Abs(dX);
                dY = Math.Abs(dY);
                return true;
            }
            //top right
            if (Distance(new Point(X, Y), new Point(block.X + block.Width, block.Y)) <= R)
            {
                dX = Math.Abs(dX);
                dY = -Math.Abs(dY);
                return true;
            }

            //SIDES
            // vertical
            if (Y >= block.Y && Y <= block.Y + block.Height)
            {
                //left
                if (X + R >= block.X && X + R <= block.X + 10)
                {
                    dX = -Math.Abs(dX);
                    return true;
                }
                //right
                if (X - R <= block.X + block.Width && X - R >= block.X + block.Width - 10)
                {
                    dX = Math.Abs(dX);
                    return true;
                }
            }
            //horizontal
            if (X >= block.X && X <= block.X + block.Width)
            {
                //top
                if (Y + R >= block.Y && Y + R <= block.Y + 10)
                {
                    dY = -Math.Abs(dY);
                    return true;
                }
                //bottom
                if (Y - R <= block.Y + block.Height && Y - R >= block.Y + block.Height - 10)
                {
                    dY = Math.Abs(dY);
                    return true;
                }
            }
            return false;
        }

        public void PlayerInteraction(Player player)
        {
            //CORNERS
            //left
            if (Distance(new Point(X, Y), new Point(player.X - player.Width / 2, player.Y)) <= R)
            {
                dY = -Math.Abs(dY);
                dX = -8;
            }
            //right
            else if (Distance(new Point(X, Y), new Point(player.X + player.Width / 2, player.Y)) <= R)
            {
                dY = -Math.Abs(dY);
                dX = 8;
            }

            //TOP SIDE
            else if (X >= player.X - player.Width / 2 && X <= player.X + player.Width / 2)
            {
                if (Y + R >= player.Y && Y <= player.Y + player.Height)
                {
                    dY = -Math.Abs(dY);
                    dX = (X - player.X) / (player.Width / 14);
                }
            }
        }
    }
}