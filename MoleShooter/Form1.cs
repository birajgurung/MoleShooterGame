//#define My_Debug
using MoleShooter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoleShooter
{
    public partial class MoleShooter : Form
    {

        int _gameFrame = 0;


#if My_Debug
        int _cursX = 0;
        int _cursY = 0;
#endif

        CMole _mole;
        CSplat _splat;
        CSign _sign;
        CScoreFrame _scoreFrame;

        Random rnd = new Random();

        public MoleShooter()
        {
            InitializeComponent();


            //Create Scope site
            Bitmap b = new Bitmap(Resources.Sight);
            this.Cursor = CustomCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _scoreFrame = new CScoreFrame() { Left = 10, Top = 20 };
            _sign = new CSign() { Left = 570, Top = 20 };
            _mole = new CMole() { Left = 10, Top = 450 };
            _splat = new CSplat();


        }
        //private void GameLoop(object sender, EventArgs e)
        //{

        //}

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {

            if (_gameFrame >= 5)
            {
                UpdateMole();
                _gameFrame = 0;
            }
            _gameFrame++;

            this.Refresh();
        }

        private void UpdateMole()
        {
            _mole.Update(
                rnd.Next(Resources.Mole.Width, this.Width - Resources.Mole.Width),
                rnd.Next(this.Height / 2, this.Height - Resources.Mole.Height * 2)
                );
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics dc = e.Graphics;

            _sign.DrawImage(dc);
            _scoreFrame.DrawImage(dc);


#if My_Debug
            //using this code to help find x and y coordinates so it's easier to place the images.

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;

            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics, "X=" + _cursX.ToString() + ":" + "Y=" + _cursY.ToString(), _font,
                new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);
#endif
            _mole.DrawImage(dc);

            base.OnPaint(e);
        }

        private void MoleShooter_MouseMove(object sender, MouseEventArgs e)
        {
            //each time we move our mouse, this method is called. X and Y represents the coordinates.
#if My_Debug
            _cursX = e.X;
            _cursY = e.Y;
#endif
            this.Refresh();

        }

        private void MoleShooter_MouseClick(object sender, MouseEventArgs e)
        {
            //Start Hot Spot
            if (e.X > 681 && e.X < 758 && e.Y > 39 && e.Y < 60)
            {
                timerGameLoop.Start();
            }

            // Stop Hot Spot
            else if (e.X > 681 && e.X < 758 && e.Y > 83 && e.Y < 101)
            {
                timerGameLoop.Stop();
            }

            //Reset Hot Spot

            else if (e.X > 681 && e.X < 758 && e.Y > 129 && e.Y < 150)
            {
                timerGameLoop.Stop();
            }
            //Quit Hot Spot
            else if (e.X > 681 && e.X < 758 && e.Y > 180 && e.Y < 200)
            {
                timerGameLoop.Stop();
            }
            else
            {

            }

        }

        private void FireGun()
        {
            //Fire off the shotgun
            SoundPlayer simpleSound = new SoundPlayer(Resources.Shotgun1);
            simpleSound.Play();
        }
    }
}
