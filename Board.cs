using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _4_In_A_Row_Project
{
    class Board
    {
        //Booard's properties:
        private int length;
        private int width;
        private int rows;
        private int columns;
        private int space;
        private Color color;
        private int x;
        private int y;
        private Slot [,] slots;
        private  Graphics g;
     
        //The Constructor method:
        public Board( int rows, int columns, int space, Color color, int x, int y, Graphics g)
        {
            this.g = g;
            Color backColor = Color.White;
            this.length = space * rows;
            this.width = space*(columns )   ;
            this.rows = rows;
            this.columns = columns;
            this.space = space;
            this.color = color;
            this.x = x;
            this.y = y;
            int otherX = x;
            int otherY = y;
            SolidBrush b = new SolidBrush(Color.White);
            SolidBrush b2 = new SolidBrush(Color.Black);
            this.slots = new Slot[rows, columns];
            
            
            for (int i = 0; i < columns; i++)
            {
               
                otherY = y;
                for (int j = 0; j <rows; j++)
                {
                    slots[j, i] = new Slot(otherX , otherY, backColor);
                
                    otherY += space;
                   
                }
                otherX += space;
            }
           
        }
         
        //Getters and Setters:
        public bool GetIsTaken(int colomnNum, int rowNum) { return slots[rowNum, colomnNum].GetIsTaken(); }
        public void SetIsTakenBoard( bool isTaken, int colomn, int row) { slots[row, colomn].SetIsTaken(isTaken); }

        public int GetLength()
        {
            return this.length;
        }
        public Color GetDotColor(int column, int row) { return slots[row, column].GetDotColor(); }
        
        public int GetWidth()
        {
            return this.width;
        }
        public void SetDotColor(int row, int column, Color color) { slots[row, column].SetDotColor(color); }
       
        public int GetRows()
        {
            return this.rows;
        }

        public void SetRows(int rows)
        {
            this.rows = rows;
        }

        public int GetColumns()
        {
            return this.columns;
        }
        //(returns the lowest spot available in a certain column):
        public int GetLowestSpotInCol(int col)
        {
            for(int i =0; i<this.rows; i++)
            {
                if (GetIsTaken(col, i))
                {
                    return i ;

                }
                
                    
            }
            return rows;
        }
        public void SetColumns(int columns)
        {
            this.columns = columns;
        }

        public int GetSpace()
        {
            return this.space;
        }

        public void SetSpace(int space)
        {
            this.space = space;
        }

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


        //Other methods:
        private void Outline(Pen pen, int x, int y,int rows, int columns, int space)
        {
            //(Draws horizontal lines):
            for (int i = 1; i <= rows; i++)
            {
                g.DrawLine(pen, x, y + i * space, x + columns * space, y + i * space);
                if (i == 1)
                {
                    for (int j = 1; j <= columns; j++)
                    {
                        int arrowSize = space - space / 3;
                        int arrowX = x + (j - 1) * space + space / 2;
                        int arrowY = y - arrowSize / 2;



                        // (Draws arrow's head):
                        g.DrawLine(pen, arrowX - arrowSize / 2, arrowY, arrowX, arrowY + arrowSize / 2);
                        g.DrawLine(pen, arrowX + arrowSize / 2, arrowY, arrowX, arrowY + arrowSize / 2);
                    }
                }

            }
            //(Draws vertical lines):
            for (int i = 0; i <= columns; i++)
            {
                g.DrawLine(pen, x + i * space, y, x + i * space, y + rows * space);
            }
           
                //(Draws circules):
                int otherX = x;
                int otherY = y;
                SolidBrush b; 
                for (int i = 0; i < columns; i++)
                {
                    otherY = y;
                    for (int j = 0; j < rows; j++)
                    {

                    if (slots[j, i].GetIsTaken())
                    {
                      
                        b = new SolidBrush(slots[j, i].GetDotColor());
                        g.FillEllipse(b, otherX, otherY, space, space);
                       
                    }
                    else
                    {
                        b = new SolidBrush(Color.White);
                        g.FillEllipse(b, otherX, otherY, space, space);
                    }
                    otherY += space;
                    }
                    otherX += space;
                }

            

        }

        public void Draw()
        {
            Pen pen = new Pen(this.color,7);
            this.Outline( pen, this.x, this.y, this.rows, this.columns, this.space);
        }

        public void Undraw()
        {
            Pen pen = new Pen(Color.MidnightBlue, 7);
            this.Outline( pen, this.x, this.y, this.rows, this.columns, this.space);
        }

    }
}

