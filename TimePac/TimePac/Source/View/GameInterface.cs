using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using TimePac.Properties;

namespace TimePac
{
    public partial class GameInterface : UserControl
    {
        private bool[] _tileStructure = new bool[196];
        private int _fieldSize = -1;
        private Image _sizedWall = null;
        private Image _sizedRunner = null;
        private Image _sizedStreetStraight = null;
        private Image _sizedStreetCurve = null;
        private Image _runner = Resources.NasCar;
        private Direction _direction = Direction.Right;

        private double _speed;
        private double _runnerX;
        private double _runnerY;

        private Font _timerFont;

        private Stopwatch _watch = new Stopwatch();

        public GameInterface(FormMain parent)
        {
            if(File.Exists("CarToUse.png"))
            {
                try
                {
                    _runner = Image.FromFile("CarToUse.png");
                }
                catch
                { }
            }

            Parent = parent;
            parent.KeyDown += new KeyEventHandler(OnGameInterfaceKeyDown);

            InitializeComponent();
            SetTileStructure();

            OnGameInterfaceResize(null, null);
        }

        private void SetTileStructure()
        {
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    if (Resources.Level.GetPixel(x, y).Name == "ffffffff")
                    {
                        _tileStructure[x * 14 + y] = false;
                    }
                    else
                    {
                        _tileStructure[x * 14 + y] = true;
                    }
                }
            }
        }

        private void OnGameInterfaceResize(object sender, EventArgs e)
        {
            if (Width > Height)
            {
                _panelGame.Location = new Point((int)((Width - Height) / 2.0), 0);

                _panelGame.Width = Height;
                _panelGame.Height = Height;
            }
            else
            {
                _panelGame.Location = new Point(0, (int)((Height - Width) / 2.0));

                _panelGame.Width = Width;
                _panelGame.Height = Width;
            }

            _fieldSize = (int)Math.Round(_panelGame.Width / 14.0, 0);
            _timerFont = new Font(new FontFamily("Arial"), (int)(_fieldSize * 0.5), FontStyle.Regular);
            _sizedRunner = new Bitmap(_runner, new Size(_fieldSize, _fieldSize));
            _sizedStreetStraight = new Bitmap(Resources.StreetStraight, new Size(_fieldSize, _fieldSize));
            _sizedStreetCurve = new Bitmap(Resources.StreetCurve, new Size(_fieldSize, _fieldSize));

            _speed = _fieldSize / 5;
            _sizedWall = new Bitmap(Resources.WallSprite, new Size(_fieldSize, _fieldSize));

            Reset(true);
        }

        private void OnPanelGamePaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    if (_tileStructure[x * 14 + y])
                    {
                        g.DrawImageUnscaled(_sizedWall, x * _fieldSize, y * _fieldSize);
                    }
                    else
                    {
                        if ((!_tileStructure[(x + 1) * 14 + y] || !_tileStructure[(x - 1) * 14 + y]) && _tileStructure[x * 14 + y + 1] && _tileStructure[x * 14 + y - 1])
                        {
                            g.DrawImageUnscaled(_sizedStreetStraight, x * _fieldSize, y * _fieldSize);
                        }
                        else if ((!_tileStructure[x * 14 + y + 1] || !_tileStructure[x * 14 + y - 1]) && _tileStructure[(x + 1) * 14 + y] && _tileStructure[(x - 1) * 14 + y])
                        {
                            _sizedStreetStraight.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            g.DrawImageUnscaled(_sizedStreetStraight, x * _fieldSize, y * _fieldSize);
                            _sizedStreetStraight.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        else
                        {
                            if (!_tileStructure[(x - 1) * 14 + y] && !_tileStructure[x * 14 + y + 1])
                            {
                                g.DrawImageUnscaled(_sizedStreetCurve, x * _fieldSize, y * _fieldSize);
                            }
                            else if (!_tileStructure[(x - 1) * 14 + y] && !_tileStructure[x * 14 + y - 1])
                            {
                                _sizedStreetCurve.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                g.DrawImageUnscaled(_sizedStreetCurve, x * _fieldSize, y * _fieldSize);
                                _sizedStreetCurve.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            }
                            else if (!_tileStructure[(x + 1) * 14 + y] && !_tileStructure[x * 14 + y + 1])
                            {
                                _sizedStreetCurve.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                g.DrawImageUnscaled(_sizedStreetCurve, x * _fieldSize, y * _fieldSize);
                                _sizedStreetCurve.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            }
                            else if (!_tileStructure[(x + 1) * 14 + y] && !_tileStructure[x * 14 + y - 1])
                            {
                                _sizedStreetCurve.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                g.DrawImageUnscaled(_sizedStreetCurve, x * _fieldSize, y * _fieldSize);
                                _sizedStreetCurve.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            }
                        }
                    }
                }
            }

            g.DrawImageUnscaled(_sizedRunner, (int)_runnerX, (int)_runnerY);
            g.DrawString(((int)_watch.Elapsed.TotalMilliseconds).ToString(), _timerFont, Brushes.Black, _fieldSize * 2, (int)(_fieldSize * 2.1));
        }

        private void OnGameInterfaceKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    _timer.Enabled = true;
                    _direction = Direction.Left;
                    _sizedRunner = new Bitmap(_runner, new Size(_fieldSize, _fieldSize));
                    _sizedRunner.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;

                case Keys.W:
                    _timer.Enabled = true;
                    _direction = Direction.Up;
                    _sizedRunner = new Bitmap(_runner, new Size(_fieldSize, _fieldSize));
                    _sizedRunner.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;

                case Keys.D:
                    _timer.Enabled = true;
                    _direction = Direction.Right;
                    _sizedRunner = new Bitmap(_runner, new Size(_fieldSize, _fieldSize));
                    break;

                case Keys.S:
                    _timer.Enabled = true;
                    _direction = Direction.Down;
                    _sizedRunner = new Bitmap(_runner, new Size(_fieldSize, _fieldSize));
                    _sizedRunner.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;

                case Keys.R:

                    Reset(true);
                    break;

                case Keys.Escape:
                    Parent.KeyDown -= new KeyEventHandler(OnGameInterfaceKeyDown);
                    ((FormMain)Parent).SwitchInterface(new MenuInterface(((FormMain)Parent)));
                    break;
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (!_watch.IsRunning)
            {
                _watch.Restart();
            }

            int x = (int)_runnerX / _fieldSize;
            int y = (int)_runnerY / _fieldSize;

            switch (_direction)
            {
                case Direction.Left:

                    x = (int)(_runnerX - _speed) / _fieldSize;

                    if (!_tileStructure[x * 14 + y] && (_runnerY % _fieldSize == 0 || !_tileStructure[x * 14 + y + 1]))
                    {
                        _runnerX -= _speed;
                    }
                    else
                    {
                        _runnerX = (x + 1) * _fieldSize;
                    }

                    break;

                case Direction.Up:

                    y = (int)(_runnerY - _speed) / _fieldSize;

                    if (!_tileStructure[x * 14 + y] && (_runnerX % _fieldSize == 0 || !_tileStructure[(x + 1) * 14 + y]))
                    {
                        _runnerY -= _speed;
                    }
                    else
                    {
                        _runnerY = (y + 1) * _fieldSize;
                    }

                    break;

                case Direction.Right:

                    if (_runnerX == _fieldSize * 12 && _runnerY == _fieldSize * 12)
                    {
                        Reset(false);

                        FormMain parent = ((FormMain)Parent);
                        Score highestSoFar = parent.HighScores.FirstOrDefault(c => c.Name == parent.UserName);

                        if (highestSoFar == null)
                        {
                            Score score = new Score();
                            score.Name = parent.UserName;
                            score.Time = _watch.Elapsed.TotalMilliseconds;

                            parent.HighScores.Add(score);

                            parent.HighScores = parent.HighScores.OrderBy(c => c.Time).ToList();

                            int placement = parent.HighScores.IndexOf(parent.HighScores.First(c => c.Name == parent.UserName));

                            MessageBox.Show("Sie sind auf #" + (placement + 1));
                        }
                        else if (highestSoFar.Time > _watch.Elapsed.TotalMilliseconds)
                        {
                            double oldScore = parent.HighScores.First(c => c.Name == parent.UserName).Time;

                            parent.HighScores.First(c => c.Name == parent.UserName).Time = _watch.Elapsed.TotalMilliseconds;
                            parent.HighScores = parent.HighScores.OrderBy(c => c.Time).ToList();

                            int placement = parent.HighScores.IndexOf(parent.HighScores.First(c => c.Name == parent.UserName));

                            MessageBox.Show("Sie habe ihren alten Rekord von " + ((int)oldScore).ToString() + " mit dem jetzigem (" + ((int)_watch.Elapsed.TotalMilliseconds).ToString() + ") übertroffen! Sie sind jetzt #" + (placement + 1), "Neuer Rekord!");

                        }

                        return;
                    }

                    x = (int)(_runnerX + _speed) / _fieldSize;

                    if (!_tileStructure[(x + 1) * 14 + y] && (_runnerY % _fieldSize == 0 || !_tileStructure[(x + 1) * 14 + y + 1]))
                    {
                        _runnerX += _speed;
                    }
                    else
                    {
                        _runnerX = x * _fieldSize;
                    }

                    break;
                case Direction.Down:

                    y = (int)(_runnerY + _speed) / _fieldSize;

                    if (!_tileStructure[x * 14 + y + 1] && (_runnerX % _fieldSize == 0 || !_tileStructure[(x + 1) * 14 + y + 1]))
                    {
                        _runnerY += _speed;
                    }
                    else
                    {
                        _runnerY = y * _fieldSize;
                    }

                    break;
            }

            _panelGame.Invalidate();
        }

        private void Reset(bool resetTimer)
        {
            _timer.Enabled = false;
            _runnerX = _fieldSize;
            _runnerY = _fieldSize;

            _watch.Stop();

            if (resetTimer)
            {
                _watch.Reset();
            }

            _panelGame.Invalidate();
        }
    }
}
