using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Atari_Breakout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Ball _ball;
        private Player _player;
        private readonly Color _playerColor = Color.Green;
        private readonly Color _ballColor = Color.Blue;
        private readonly Color _backgroundColor = Color.White;
        private Graphics _graphics;
        private List<Block> _blocks;
        private Random _random = new Random();
        private bool _gameOver = true;

        void FormBlocks()
        {
            _blocks = new List<Block>();

            int spaceBetweenRows = (int)(_ball.R * 1.5f);
            int maxHeight = pictureBox1.Height / 11;
            int minHeight = spaceBetweenRows * 3;
            int currentX = spaceBetweenRows;
            int currentY = maxHeight;
            int currentRowsHeight = _random.Next(minHeight, maxHeight);
            int currentRows = 1;

            while (currentRows <= 5)
            {
                Block currentBlock;
                Color color = Color.FromArgb(_random.Next(0, 150), _random.Next(0, 150), _random.Next(0, 150));
                GraphicTool graphicTool = new GraphicTool(_graphics, _backgroundColor, color);
                int maxWidth = pictureBox1.Width / 7;
                int minWidth = 80;
                int width = _random.Next(minWidth, maxWidth);
                if (currentX + width >= pictureBox1.Width - spaceBetweenRows ||
                    currentX >= pictureBox1.Width - spaceBetweenRows - maxWidth - minWidth / 2)
                {
                    width = pictureBox1.Width - spaceBetweenRows - currentX;
                    currentBlock = new Block(currentX, currentY, width, currentRowsHeight, graphicTool);
                    currentRows++;
                    currentY += currentRowsHeight + spaceBetweenRows;
                    currentRowsHeight = _random.Next(minHeight, maxHeight);
                    currentX = spaceBetweenRows;
                }
                else
                {
                    currentBlock = new Block(currentX, currentY, width, currentRowsHeight, graphicTool);
                    currentX += width + spaceBetweenRows;
                }

                _blocks.Add(currentBlock);
            }

            ShowBlocks();
        }

        void ShowBlocks()
        {
            foreach (var block in _blocks)
            {
                block.Show();
            }
        }

        void NewGame()
        {
            _gameOver = false;
            _graphics = pictureBox1.CreateGraphics();
            _graphics.Clear(_backgroundColor);
            _ball = new Ball(new Random(), pictureBox1.Width / 2, pictureBox1.Height - 100, pictureBox1.Height / 70);
            _ball.GraphicTool = new GraphicTool(_graphics, _backgroundColor, _ballColor);
            _ball.Show();
            FormBlocks();
            _player = new Player(pictureBox1.Width / 2, pictureBox1.Height - pictureBox1.Height / 20, pictureBox1.Width / 8, pictureBox1.Height / 40);
            _player.GraphicTool = new GraphicTool(_graphics, _backgroundColor, _playerColor);
            _player.Show();
            timer1.Interval = 10000 / pictureBox1.Height;
            timer1.Start();
        }

        void BallMovementLogic()
        {
            //hitting walls
            // left 
            if (_ball.X - _ball.R <= 0)
            {
                _ball.dX = Math.Abs(_ball.dX);
            }
            // right
            if (_ball.X + _ball.R >= pictureBox1.Width)
            {
                _ball.dX = -Math.Abs(_ball.dX);
                return;
            }
            //top wall
            if (_ball.Y - _ball.R <= 0)
            {
                _ball.dY *= -1;
                return;
            }
            // GAME OVER
            if (_ball.Y - _ball.R >= pictureBox1.Height)
            {
                timer1.Stop();
                _gameOver = true;
                MessageBox.Show("YOU LOST!");
                return;
            }

            //hitting blocks
            foreach (var block in _blocks)
            {
                if (_ball.BlockHit(block))
                {
                    block.Delete();
                    _blocks.Remove(block);
                    if (_blocks.Count == 0)
                    {
                        timer1.Stop();
                        _gameOver = true;
                        _ball.Show();
                        MessageBox.Show("YOU WON!");
                    }
                    return;
                }
            }

            //hitting player
            _ball.PlayerInteraction(_player);

        }

        private void labelStart_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SetBounds(20, 60, ClientRectangle.Width - 40, ClientRectangle.Height - 80);
            labelStart.SetBounds(20, 10, 90, 60);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ball.Move();
            _ball.Show();
            BallMovementLogic();

            _player.Show();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_gameOver)
                return;
            _player.Delete(pictureBox1.Width);
            _player.X = e.X;
            if (_player.X < _player.Width / 2)
            {
                _player.X = _player.Width / 2;
            }

            if (_player.X > pictureBox1.Width - _player.Width / 2)
            {
                _player.X = pictureBox1.Width - _player.Width / 2;
            }
            _player.Show();
        }
    }
}
