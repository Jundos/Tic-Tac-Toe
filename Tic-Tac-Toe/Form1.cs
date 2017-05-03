using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        public Game game = new Game();
        public Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            game.update(g);
            g = panel1.CreateGraphics();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (game.playerControl == true)
            {
                // координаты относительно панели "panel1"
                int clickPositionX = MousePosition.X - this.Left - panel1.Left - 8, // 8 толщина рамки слева
                    clickPositionY = MousePosition.Y - this.Top - panel1.Top - 31; // 31 толщина рамки вверху
                game.PlayerMove(clickPositionX, clickPositionY);
            }
            if (game.endGame) game.NewGame();
            game.update(g);
            
        }
            // возникает после клика мышки
        private void panel1_MouseCaptureChanged(object sender, EventArgs e)
        {
            game.update(g);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.NewGame();
            game.update(g);
        }
    }
}
