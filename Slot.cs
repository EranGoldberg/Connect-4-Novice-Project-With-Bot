using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_In_A_Row_Project
{
    internal class Slot
    {
        //Slots's properties:
        private int x;
        private int y;
        private bool isTaken;
        private Color dotColor;

        //The Consturctor method:
        public Slot(int x, int y, Color dotColor)
        {
            this.x = x;
            this.y = y;
            this.isTaken = false;
            this.dotColor = dotColor;
        }
        //Getters and Setters:
        public int GetX() { return this.x; }
        public int GetY() { return this.y; }
        public Color GetDotColor() { return dotColor; }
        public bool GetIsTaken() { return this.isTaken;}
        public void SetX(int x) { this.x = x;}
        public void SetY(int y) { this.y = y;}
        public void SetDotColor(Color dotColor) { this.dotColor = dotColor;}
        public void SetIsTaken(bool isTaken) { this.isTaken = isTaken;} 
    }
}
