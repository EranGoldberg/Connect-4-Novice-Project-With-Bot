using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_In_A_Row_Project
{
    internal class Stats
    {
        //Stats's properties:
        private bool draw;
        private Color turn;
        private int x;
        private int y;
        private int length;
        private int width;
        private Font font;
        bool won;
        private Graphics g;

        //The Constructor method:
        public Stats(Board board, Graphics g)
        {

            this.x = 0;
            this.g = g;
            this.y = 10;
            this.turn = Color.Red;
            this.width = board.GetX() / 2 + board.GetX()/4;
            this.length= width;
            this.font = new Font("Georgia", 13);
            won = false;
            draw = false;
        

        }

       //Getters and Setters:
        public void SetDraw(bool draw) { this.draw = draw; }
        
        public Color GetTurn() { return this.turn; } 
        public void SetTurn(Color color) { this.turn = color; }
        public void SetWon(bool won) { this.won = won; }

        
        //Other methods:
        private void OutLine(Graphics g, Color textColor, Color rectColor)
        {
            Brush brush1 = new SolidBrush(textColor);
            Pen p = new Pen(rectColor, 2);
            g.DrawRectangle(p, x, y, width, length);
            string strC = "";
            if (turn == Color.Red)
            {
                strC = "Red";
            }
            else
            {
                strC = "Blue";
            }
            if (won)
            {
                string winMsg = strC + " Wins!";
                g.DrawString(winMsg, this.font, brush1, this.x + width / 4, this.y + length / 12);
            }
            else if (draw)
            {
                string winMsg = "Draw!";
                g.DrawString(winMsg, this.font, brush1, this.x + width / 4, this.y + length / 12);
            }
            else
            {
                string turnS = "It is " + strC + "'s turn";

                g.DrawString(turnS, this.font, brush1, this.x + width / 4, this.y + length / 12);
            }

        }
        public void Draw()
        {
            OutLine(g, Color.LightYellow, Color.LightYellow);
        }
        public void Undraw()
        {
            OutLine(g, Color.MidnightBlue, Color.MidnightBlue);
        }
    }
}
