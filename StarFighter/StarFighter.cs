using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StarFighter
{
    public partial class StarFighter : Form
    {
        private const int NUMBER_OF_STARS = 30;
        private Dictionary<PictureBox, int> starList = new Dictionary<PictureBox, int>();

        public StarFighter()
        {
            InitializeComponent();

            timer1.Enabled = true;
            Cursor.Hide();

            timer1.Interval = 50;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            ship.Top = playground.Bottom - (playground.Bottom / 5);

            MakeStars();
        }

        private void MakeStars()
        {
            Random r = new Random();
            for (int i = 1; i <= NUMBER_OF_STARS; i++)
            {
                PictureBox pic = new PictureBox();

                pic.Width = 5;
                pic.Height = 5;
                pic.BackColor = Color.White;
                pic.Location = new Point(r.Next(playground.Width), r.Next(playground.Height));

                starList.Add(pic, r.Next(15, 50));

                playground.Controls.Add(pic);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ship.Left = Cursor.Position.X - (ship.Width / 2);

            MoveStars();
            MoveLasers();
        }

        private void ShootLaser()
        {
            PictureBox laser = new PictureBox();
            laser.Width = 3;
            laser.Height = 15;
            laser.BackColor = Color.Red;
            laser.Location = new Point(ship.Location.X + (ship.Width / 2), ship.Location.Y);

            playground.Controls.Add(laser);
        }

        private void MoveLasers()
        {
            foreach (var cur in playground.Controls)
            {
                if (cur.GetType() == typeof(PictureBox))
                {
                    PictureBox aLaser = (PictureBox)cur;

                    if (aLaser.Width == 3)
                    {
                        int index = playground.Controls.IndexOf((PictureBox)cur);
                        int newY = playground.Controls[index].Location.Y - 35;

                        if (newY < this.Top)
                        {
                            playground.Controls.RemoveAt(index);
                        }
                        else
                        {
                            playground.Controls[index].Location = new Point(playground.Controls[index].Location.X, newY);
                        }
                    }
                }
            }
        }

        private void MoveStars()
        {
            foreach (var cur in playground.Controls)
            {
                if (cur.GetType() == typeof(PictureBox))
                {
                    PictureBox aStar = (PictureBox)cur;

                    if (aStar.Width == 5)
                    {
                        int index = playground.Controls.IndexOf((PictureBox)cur);

                        int newY = playground.Controls[index].Location.Y + starList[aStar];

                        if (newY > this.Bottom)
                        {
                            newY = this.Top;
                        }

                        playground.Controls[index].Location = new Point(playground.Controls[index].Location.X, newY);
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Space)
            {
                ShootLaser();
            }
        }
    }
}