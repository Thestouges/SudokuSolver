using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        int[][] inputGrid;
        bool error;
        TextBox[][] textboxes;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inputGrid = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                inputGrid[i] = new int[9];
            }
            textboxes = new TextBox[9][];
            textboxes[0] = new TextBox[9] {a1,a2,a3,a4,a5,a6,a7,a8,a9};
            textboxes[1] = new TextBox[9] {b1,b2,b3,b4,b5,b6,b7,b8,b9};
            textboxes[2] = new TextBox[9] {c1,c2,c3,c4,c5,c6,c7,c8,c9};
            textboxes[3] = new TextBox[9] {d1,d2,d3,d4,d5,d6,d7,d8,d9};
            textboxes[4] = new TextBox[9] {e1,e2,e3,e4,e5,e6,e7,e8,e9};
            textboxes[5] = new TextBox[9] {f1,f2,f3,f4,f5,f6,f7,f8,f9};
            textboxes[6] = new TextBox[9] {g1,g2,g3,g4,g5,g6,g7,g8,g9};
            textboxes[7] = new TextBox[9] {h1,h2,h3,h4,h5,h6,h7,h8,h9};
            textboxes[8] = new TextBox[9] {i1,i2,i3,i4,i5,i6,i7,i8,i9};
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int temp;
            error = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (textboxes[i][j].Text != "")
                    {
                        if (Int32.TryParse(textboxes[i][j].Text, out temp))
                        {
                            if (temp >= 1 && temp <= 9)
                            {
                                inputGrid[i][j] = Convert.ToInt32(textboxes[i][j].Text);
                                textboxes[i][j].BackColor = Color.Green;
                                textboxes[i][j].ForeColor = Color.White;
                            }
                            else
                                error = false;
                        }
                    }
                    else
                    {
                        inputGrid[i][j] = 0;
                    }
                }
            }

            if (error)
            {
                if (backtrack(inputGrid))
                {
                    DisplayGrid();
                }
            }

        }

        private bool backtrack(int[][] grid)
        {
            int row, col;

            if (!FindEmptyLocation(grid, out row, out col))
            {
                return true;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (isSafe(grid, row, col, i))
                {
                    grid[row][col] = i;

                    if (backtrack(grid))
                        return true;

                    grid[row][col] = 0;
                }
            }
            return false;
        }
        
        private bool FindEmptyLocation(int[][] grid,out int row, out int col){
            for (row = 0; row < 9; row++)
                for (col = 0; col < 9; col++)
                    if (grid[row][col] == 0)
                        return true;
            col = 0;
            return false;

        }

        private bool isSafe(int[][] grid, int row, int col, int num){
            return !CheckRow(grid, row, num) && 
                !CheckCol(grid, col, num) && 
                !CheckBox(grid, row - row % 3, col - col % 3, num);
        }

        private bool CheckRow(int[][] grid, int row, int num)
        {
            for (int col = 0; col < 9; col++)
                if (grid[row][col] == num)
                    return true;
            return false;
        }

        private bool CheckCol(int[][] grid, int col, int num)
        {
            for (int row = 0; row < 9; row++)
                if (grid[row][col] == num)
                    return true;
            return false;
        }

        private bool CheckBox(int[][] grid, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (grid[row + boxStartRow][col + boxStartCol] == num)
                        return true;
            return false;
        }

        private void DisplayGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textboxes[i][j].Text = inputGrid[i][j].ToString();
                }
            }
        }
    }
}
