using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public class Game
    {
        Random rnd = new Random();
        Font font = new Font(FontFamily.GenericMonospace.Name, 30);
        private int[,] pole;
        private int numberCombination = 0;
        public bool playerControl = true, endGame = false;

        public Game()
        {
            pole = new int[3,3] {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0} };
        }
        public void NewGame()
        {
            pole = new int[3, 3] {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0} };

            numberCombination = 0;
            playerControl = true;
            endGame = false;
        }
        private void ComputerMove()
        {
            while(true)
            {
                int i = rnd.Next(3), j = rnd.Next(3);
                if (pole[i, j] == 0){ pole[i, j] = 2; break; } 
            }
        }
        public void PlayerMove(int clickPositionX, int clickPositionY)
        {
            int i = 0, j = 0;
            if (clickPositionX > 0 && clickPositionX < 100) j = 0;
            if (clickPositionX > 100 && clickPositionX < 200) j = 1;
            if (clickPositionX > 200 && clickPositionX < 300) j = 2;

            if (clickPositionY > 0 && clickPositionY < 100) i = 0;
            if (clickPositionY > 100 && clickPositionY < 200) i = 1;
            if (clickPositionY > 200 && clickPositionY < 300) i = 2;
            
            if (pole[i,j] == 0)
            {
                pole[i, j] = 1;
                playerControl = false;
            }
            else { playerControl = true; }
        }
        public void update(Graphics g)
        {
            drawPole(g);
            
            if (winCombinations() == true)
            {
                drawWinLine(g);
                g.DrawString("WIN", font, Brushes.Gold, 100, 120);
                playerControl = false; endGame = true;
            }
            else
            {
                if (standoff()) { g.DrawString("Standoff", font, Brushes.Black, 40, 130); endGame = true; }
                else
                    if (!playerControl) { ComputerMove(); playerControl = true; }
            }
        }
        private bool winCombinations()
        {
            bool win = true;
            for (int p = 1; p <= 2; p++)
            {
                this.numberCombination = 0;
                // проверка по горизонтали
                for (int i = 0; i < 3; i++)
                {
                    win = true;
                    for (int j = 0; j < 3; j++)
                    {
                        if (pole[i, j] != p) win = false;
                    }
                    this.numberCombination++;
                    if (win){ return win; }
                }
                // проверка по вертикали
                for (int i = 0; i < 3; i++)
                {
                    win = true;
                    for (int j = 0; j < 3; j++)
                    {
                        if (pole[j, i] != p) win = false;
                    }
                    this.numberCombination++;
                    if (win) { return win; }
                }
                // проверка по диагонали
                this.numberCombination++;
                win = true;
                for (int i = 0; i < 3; i++)
                {
                    if (pole[i, i] != p) win = false;  // "\"
                }
                if (win) { return win; }
                this.numberCombination++;
                win = true;
                for (int i = 0; i < 3; i++)
                {
                    if (pole[i, 2 - i] != p) win = false; // "/"
                }
                if (win) { return win; }
            }
            return win;
        }
        private bool standoff() // ничья
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pole[i, j] == 0) return false;
                }
            }
            return true;
        }
        private void drawPole(Graphics g)
        {
            Pen p = new Pen(Color.Blue, 3);
            g.FillRectangle(Brushes.LightCyan, 0, 0, 300, 300);
            for (int i = 0; i < 301; i += 100)
            {
                g.DrawLine(p, i, 0, i, 300);
                g.DrawLine(p, 0, i, 300, i);
            }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (pole[i, j] == 1)
                        drawX(g, i, j);
                    if (pole[i, j] == 2)
                        drawO(g, i, j);
                }
        }
        private void drawX(Graphics g, int i, int j)
        {
            switch (j)
            {
                case 1: j = 100; break;
                case 2: j = 200; break;
                default:
                    break;
            }
            switch (i)
            {
                case 1: i = 100; break;
                case 2: i = 200; break;
                default:
                    break;
            }
            Pen p = new Pen(Color.Red, 9);
            g.DrawLine(p, j + 15, i + 15, j + 85, i + 85); // "\"
            g.DrawLine(p, j + 15, i + 85, j + 85, i + 15); // "/"
        }
        private void drawO(Graphics g, int i, int j)
        {
            switch (j)
            {
                case 1: j = 100; break;
                case 2: j = 200; break;
                default:
                    break;
            }
            switch (i)
            {
                case 1: i = 100; break;
                case 2: i = 200; break;
                default:
                    break;
            }
            Pen p = new Pen(Color.Green, 9);
            g.DrawEllipse(p, j+15, i+15, 70, 70);
        }
        private void drawWinLine(Graphics g)
        {
            Pen p = new Pen(Color.Black, 8);
            switch (numberCombination)
            {
                case 1: g.DrawLine(p, 15, 50, 285, 50); break;
                case 2: g.DrawLine(p, 15, 150, 285, 150); break;
                case 3: g.DrawLine(p, 15, 250, 285, 250); break;
                case 4: g.DrawLine(p, 50, 15, 50, 285); break;
                case 5: g.DrawLine(p, 150, 15, 150, 285); break;
                case 6: g.DrawLine(p, 250, 15, 250, 285); break;
                case 7: g.DrawLine(p, 30, 30, 270, 270); break;
                case 8: g.DrawLine(p, 270, 30, 30, 270); break;
                default:
                    break;
            }
        }
    }
}
