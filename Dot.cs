using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Windows.Forms.AxHost;
using System.Windows.Forms.VisualStyles;
using System.Security.Cryptography;
using System.Drawing.Text;
using System.Windows.Forms;
using WMPLib;

namespace _4_In_A_Row_Project
{
    class Dot
    {
        //Dot's properties:
        private Color color;
        private int x;
        private int y;
        private int size;
        private Graphics g;
        private WindowsMediaPlayer dropSound;

        //The Constructor method:
        public Dot(Color color1, Graphics gr, Board board, WindowsMediaPlayer dropSound)
        {
            g = gr;
            color = color1;
            size = board.GetSpace();
            this.dropSound= dropSound;

        }

        //Getters and Setters:
        public Color GetColor()
        {
            return this.color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public int GetX()
        {
            return this.x;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return this.y;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetSize()
        {
            return size;
        }


        //Other type methods:
        public void Draw()
        {
            Outline(color);
        }

        public void Undraw()
        {
          
            Outline(Color.MidnightBlue);
        }


        private void Outline(Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillEllipse(brush, this.x, this.y, size, size);
            brush.Dispose();
        }

        //(Drops the Dot from above the top row):
        public void Animate(int column, Board board)
        {
           dropSound.controls.stop();
            int rowNum = 0;
            int row = -1;
            bool wasDroped = false;
            bool tooHeigh = false;
            if (board.GetRows() <= 7)
            {
                dropSound.controls.play();
            }
            else
            {
                tooHeigh = true;
            }
 
            SetX(board.GetX() + column * board.GetSpace());
                SetY(board.GetY());
                
                    for (int i = this.y; rowNum != board.GetRows() && !board.GetIsTaken(column, rowNum); i += board.GetSpace())
                    {
                    
                     Application.DoEvents();
                     Thread.Sleep(65);
                     this.Outline(Color.White);
                     this.SetY(i);
                     this.Draw();
                     row++;
                     rowNum++;
                
                    if(tooHeigh && row+3<board.GetRows() && board.GetIsTaken(column,row+3) )
                      {
                       dropSound.controls.play();
                        wasDroped= true;
                      }
                    
             
                    }

            if (!wasDroped && tooHeigh)
            {
               dropSound.controls.play();
            }
          if (rowNum==1)
            {
               board.SetDotColor(0, column, this.color);
              board.SetIsTakenBoard(true, column, 0);
                
            }
            else if(rowNum>0) 
            {

                board.SetDotColor(row, column, this.color);
                board.SetIsTakenBoard(true, column, row);
            }
           
            board.Draw();
            Thread.Sleep(200);
        }
    }
}


