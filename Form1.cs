using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using WMPLib;

namespace _4_In_A_Row_Project
{

    public partial class Form1 : Form
    {
        //class variables, types:

        private bool isMouseDown;
        private bool isMouseUp;
        Random rnd = new Random();
        private int totalSeconds = 0;
        bool isPlayerVsPlayer = true;
        bool isPlayerVsBot = false;
        bool isBotVsBot = false;
        WindowsMediaPlayer backgroundM = new WindowsMediaPlayer();
        WindowsMediaPlayer winSound = new WindowsMediaPlayer();
        WindowsMediaPlayer dropSound = new WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
            backgroundM.URL = "elevator-music-bossa-nova-background-music-version-60s-10900.mp3";
            winSound.URL = "success-1-6297.mp3";
            dropSound.URL = "Water-Drop-Bloop-Sound-CloseDistance-8-www.FesliyanStudios.com.mp3";
            this.WindowState = FormWindowState.Maximized;
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        // Events:
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            isMouseUp = false;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            isMouseUp = true;
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // Update the timer:
            totalSeconds++;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            string timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Updating the timer label:
            label1.Text = timerText;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (isBotVsBot || isPlayerVsBot || isPlayerVsPlayer)
            {
                this.Invalidate();
                timer1.Enabled = true;
                totalSeconds = -1;
                label1.Visible = true;
                label2.Visible = true;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {

            isPlayerVsPlayer = true;
            isPlayerVsBot = false;
            isBotVsBot = false;
            totalSeconds = -1;
            timer1.Enabled = true;
            this.Invalidate();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            isPlayerVsPlayer = false;
            isPlayerVsBot = true;
            isBotVsBot = false;
            totalSeconds = -1;
            timer1.Enabled = true;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            isPlayerVsPlayer = false;
            isPlayerVsBot = false;
            isBotVsBot = true;
            totalSeconds = -1;
            timer1.Enabled = true;
            // Application.DoEvents();

        }

        //Static methods:

        //The method gets, a Board, Dot, row, and column.
        //The method returns if there are four in a row to the right.
        static bool ChkRowsRight(Board board, Dot dot, int row, int column)
        {
            Color color = dot.GetColor();
            if (column + 4 <= board.GetColumns())
            {
                if (board.GetIsTaken(column, row) && board.GetIsTaken(column + 1, row) && board.GetIsTaken(column + 2, row) && board.GetIsTaken(column + 3, row))
                {
                    if (board.GetDotColor(column, row) == color && board.GetDotColor(column + 1, row) == color && board.GetDotColor(column + 2, row) == color && board.GetDotColor(column + 3, row) == color)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //The method gets a Board, Dot, row and a column.
        //The method return if there are four in a column upwards.
        static bool ChkColumnsUp(Board board, Dot dot, int row, int column)
        {
            Color color = dot.GetColor();
            if (row >= 3) 
            {
                if (board.GetIsTaken(column, row) && board.GetIsTaken(column, row - 1) && board.GetIsTaken(column, row - 2) && board.GetIsTaken(column, row - 3))
                {
                    if (board.GetDotColor(column, row) == color && board.GetDotColor(column, row - 1) == color && board.GetDotColor(column, row - 2) == color && board.GetDotColor(column, row - 3) == color)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        //The method get a Board, Dot , row and a column.
        //The method returns if there are four in diagonal (up right).
        static bool ChkDiagUpRight(Board board, Dot dot, int row, int column)
        {
            Color color = dot.GetColor();
            if (column + 4 <= board.GetColumns() && row >= 3) 
            {
                if (board.GetIsTaken(column, row) && board.GetIsTaken(column + 1, row - 1) && board.GetIsTaken(column + 2, row - 2) && board.GetIsTaken(column + 3, row - 3))
                {
                    if (board.GetDotColor(column, row) == color && board.GetDotColor(column + 1, row - 1) == color && board.GetDotColor(column + 2, row - 2) == color && board.GetDotColor(column + 3, row - 3) == color)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //The method gets a Board, Dot, column and a row.
        //The method returns if there are four in diagonal (right down).
        static bool ChkDiagDownRight(Board board, Dot dot, int row, int column)
        {
            Color color = dot.GetColor();
            if (column + 4 <= board.GetColumns() && row + 4 <= board.GetRows())
            {
                if (board.GetIsTaken(column, row) && board.GetIsTaken(column + 1, row + 1) && board.GetIsTaken(column + 2, row + 2) && board.GetIsTaken(column + 3, row + 3))
                {
                    if (board.GetDotColor(column, row) == color && board.GetDotColor(column + 1, row + 1) == color && board.GetDotColor(column + 2, row + 2) == color && board.GetDotColor(column + 3, row + 3) == color)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //The method gets a board and a Dot.
        //The method calls each win checker and sees if one at least is true.
        static bool ChkWin(Board board, Dot dot)
        {
            for (int k = 0; k < board.GetColumns() * board.GetRows(); k++)
            {
                for (int i = 0; i < board.GetColumns(); i++)
                {


                    for (int j = 0; j < board.GetRows(); j++)
                    {
                        if (ChkRowsRight(board, dot, j, i) || ChkColumnsUp(board, dot, j, i) || ChkDiagUpRight(board, dot, j, i) || ChkDiagDownRight(board, dot, j, i))
                        {
                         
                            return true;
                        }


                    }

                }
            }
            return false;
        }


        //The method gets a Board, Dot and another Dot; the game state.
        //The method returns the not really ideal column to win.
        static int pseuduBestMove(Board board, Dot bot, Dot player)
        {
            Random rnd = new Random();
            Color botColor = bot.GetColor();
            Color playerColor = player.GetColor();
            int columnNum = board.GetColumns();
            int lowestRow = board.GetRows() - 1;
            bool skip = false;

            for (int i = 0; i < columnNum; i++)
            {
                skip = false;
                lowestRow = board.GetLowestSpotInCol(i) - 1;
                if (board.GetLowestSpotInCol(i) == 0)
                {
                   skip = true;
                }
                if (!skip)
                {
                    board.SetIsTakenBoard(true, i, lowestRow);

                    //chks if bot can win:
                    board.SetDotColor(lowestRow, i, botColor);
                    if (ChkWin(board, bot) )
                    {
                        board.SetIsTakenBoard(false, i, lowestRow);
                        board.SetDotColor(lowestRow, i, Color.White);
                        return i;

                    }
             
                    //chks if player can win:
                    board.SetIsTakenBoard(true, i, lowestRow);
                    board.SetDotColor(lowestRow, i, playerColor);

                    if (ChkWin(board, player)) 
                    {
                        board.SetIsTakenBoard(false, i, lowestRow);
                        board.SetDotColor(lowestRow, i, Color.White);
                        return i;

                    }
                    board.SetIsTakenBoard(false, i, lowestRow);
                    board.SetDotColor(lowestRow, i, Color.White);
                }
                
            }


            //if can't win or prevent opponent from winning, then selects a "random" column, with a tendency to the middle column:
            int col = rnd.Next(1, columnNum);
            if (col < 5 && board.GetIsTaken(board.GetColumns()/2,0)==false)
            {
                return board.GetColumns() / 2;
            }
            else if (board.GetLowestSpotInCol(col) > 0)
            {
                return col;
            }
            else
            {
                for(int i = 0; i < columnNum; i++)
                {
                    if(!board.GetIsTaken(i, 0))
                        return i;
                }
            }
          return -999; //(impossible)
        }


        //The method gets a Dot,  Board, and Stats.
        //The method draw the dot on screen according to user input, and also "drops" the dot when player enters required input.
        void Move(Dot dot, Board board, Stats stats)
        {
            bool wasPressed = false;
            bool run = true;

            while (run)
            {
                Point cursorPosition = Cursor.Position;
                int x = cursorPosition.X - dot.GetSize() / 2;
                int y = cursorPosition.Y - dot.GetSize() / 2 - dot.GetSize() / 4;

                if (isMouseDown && (x < board.GetX() - board.GetSpace() || x > board.GetX() + board.GetWidth() || y<board.GetY() -board.GetSpace()))
                {
                    dot.Undraw();
                    dot.SetX(x);
                    dot.SetY(y);
                    dot.Draw();
                    stats.Draw();
                    wasPressed = true;
                }
               else if (isMouseDown)
                {
                        dot.Undraw();
                        dot.SetX(board.GetColumns() / 2 * board.GetSpace() + board.GetX());
                        dot.SetY(0);
                        wasPressed = true;
                        dot.Draw();
                }
                else if (isMouseUp && wasPressed && dot.GetX() == x )
                {
                    dot.Undraw();

                    if (y < board.GetY() - board.GetSpace() / 2 && y > 0 && x > board.GetX() && x < board.GetWidth() + board.GetX())
                    {
                        for (int col = 0; col < board.GetColumns(); col++)
                        {
                            if (x > board.GetSpace() * col + board.GetX() - board.GetSpace() / 3 && x < (col + 1) * board.GetSpace() + board.GetX() - board.GetSpace() / 2 && !board.GetIsTaken(col, 0))
                            {
                                dot.Animate(col, board);
                                run = false;
                                break;
                            }
                        }
                    } 
                }
                else
                {
                    dot.Undraw();
                    dot.SetX(board.GetColumns() / 2 * board.GetSpace() + board.GetX());
                    dot.SetY(0);
                    dot.Draw();
                    board.Draw();
                    stats.Draw();

                    while (!isMouseDown )
                    {
                        Application.DoEvents();
                    }
                }

                Application.DoEvents();
            }
        }




        //Main Code
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
                Graphics g = e.Graphics;
                Board board = new Board(6, 7, 98, Color.Orange, 370, 190, g);
                Dot red = new Dot(Color.Red, g, board, dropSound);
                Dot blue = new Dot(Color.Blue, g, board, dropSound);
                Stats stats = new Stats(board, g);
               
                int spaceTaken = 0;
                int spaceLeft = board.GetRows() * board.GetColumns();
                bool gameOver = false;

            winSound.controls.stop();
            dropSound.controls.stop();
            backgroundM.settings.volume = 40;
            backgroundM.controls.play();
            button4.Invalidate();

            stats.Draw();
            board.Draw();
          
            //Game Loop:
            while (!gameOver && (spaceTaken<spaceLeft) && (isPlayerVsBot || isPlayerVsPlayer || isBotVsBot))
               {
              
                   if (isPlayerVsPlayer || isPlayerVsBot)
                    {
                        Move(red, board, stats);
                    }
                    else if (isBotVsBot)
                    {
                        int m = pseuduBestMove(board, red, blue);
                    red.Animate(m, board);
                    }
                   

                if (ChkWin(board, red))
                    {
                        gameOver = true;
                        break;
                    }
              
                stats.Undraw();
                    stats.SetTurn(blue.GetColor());
                    stats.Draw();

                    if (isPlayerVsPlayer)
                    {
                  
                        Move(blue, board, stats);
                    }

                    else if (isBotVsBot || isPlayerVsBot)
                    {
                     
                        int n = pseuduBestMove(board, blue, red);
                        blue.Animate(n, board);
                    }
                   

                    if (ChkWin(board, blue))
                    {
                     gameOver = true;
                    }
                    else
                    {
                       stats.Undraw();
                        stats.SetTurn(red.GetColor());
                    }

                 spaceTaken += 2;
                if (spaceTaken == spaceLeft)
                {

                    gameOver = true;
                }
                stats.Draw();
       
                //end of game loop
            }

            //chks if blue/red won, or if draw, and updates screen:
            stats.Undraw();
            if(ChkWin(board, red))
            {
                stats.SetWon(true);
                stats.SetTurn(red.GetColor());
            }
            else if(ChkWin(board, blue))
            {
                stats.SetWon(true);
                stats.SetTurn(blue.GetColor());
            }
            else
            {
                stats.SetDraw(true) ;
            }
            board.Undraw();
            stats.Draw();
            timer1.Stop();
            backgroundM.controls.stop();
            winSound.controls.play();

            //chk if player want to play again:
            for (int j = 0; j < 10; j++)
            {
                Application.DoEvents();
                Thread.Sleep(1000);
            }
            //if not - stop the the program:
            Application.Exit();
         

        }
    
    }


}
