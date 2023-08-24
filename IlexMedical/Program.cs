using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace IlexMedical
{
    public class Sudoku
    {
        public Sudoku(string row)
        {
            int rowLength = 9;
            int columnLength = 9;

            // a valid sudoku is 9*9 chars
            if (row.Length != rowLength * columnLength)
            {
                throw new Exception("sudexo must contain 81 chars");
            }
            else
            {
                // create a 2 dimensions array that will contain the values
                this.SudokuValue = new char[rowLength, columnLength];
                for (int i = 0; i < rowLength; i++)
                {
                    for (int j = 0; j < columnLength; j++)
                    {
                        this.SudokuValue[i, j] = row[i * 9 + j];
                    }
                }
            }
        }

        /// <summary>
        /// validate a sudoku
        /// check there is unique chars in the every column, every row and every square
        /// </summary>
        /// <returns>true on valid. otherwise false</returns>
        public bool ValidateSudoku()
        {
            for (int i = 0; i < SudokuValue.GetLength(0); i++)
            {
                // create temp lists that will contain unique values for the current row and current column
                List<char> row = new List<char>();
                List<char> column = new List<char>();

                for (int j = 0; j < SudokuValue.GetLength(1); j++)
                {
                    // validate the row: if the char already exists in the current row, the sudoku is invalid
                    char xValue = SudokuValue[i, j]; // e.g. 0,0 0,1, 0,2 | 1,0 1,1 1,2 1,3
                    if (xValue != '0' && row.Contains(xValue))
                    {
                        Console.WriteLine("Already exists value: {0} in the row: {1}", xValue, i);
                        return false;
                    }
                    else
                    {
                        // add to a list that contains unique values for the current row
                        row.Add(xValue);
                    }

                    // validate the column: if the char already exists in the current column, the sudoku is invalid
                    char yValue = SudokuValue[j, i]; // e.g. 0,0 1,0 2,0 |  1,0 1,1 2,1, 3,1
                    if (yValue != '0' && column.Contains(yValue))
                    {
                        Console.WriteLine("Already exists value: {0} in the column: {1}", yValue, j);
                        return false;
                    }
                    else
                    {
                        // add to a list that contains unique values for the current column
                        column.Add(yValue);
                    }

                    // validate the square every 3 chars in the column and the row. e.g. for i,j values: 0,0 0,3 0,6 3,0 3,3 3,6
                    if ((i + 3) % 3 == 0 && (j + 3) % 3 == 0)
                    {
                        if (ValidateSquare(i, j) == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// validate a square
        /// </summary>
        /// <param name="i">first index in the square row</param>
        /// <param name="j">first index in the square column</param>
        /// <returns></returns>
        private bool ValidateSquare(int i, int j) {
            // create temp list that will contain unique values for the current square
            List<char> square = new List<char>();

            // iterate the square i.e. 3*3 indexes
            // e.g. for square in i,j = 0,0 we will check values in:
            // 0,0 0,1 0,2
            // 1,0, 1,1 1,2
            // 2,0, 2,1 2,2
            for (int m = i; m < i + 3; m++)
            {
                for (int n = j; n < j + 3; n++)
                {
                    // if the char already exists in the current square, the sudoku is invalid
                    char currentValue = SudokuValue[m, n];
                    if (currentValue != '0' && square.Contains(currentValue))
                    {
                        Console.WriteLine("Already exists value: {0} in the square beginning on top-left: ({1},{2})", currentValue, i, j);
                        return false;
                    }
                    else
                    {
                        // add to a list that contains unique values for the current square
                        square.Add(currentValue);
                    }
                }
            }
            return true;
        }

        private char[,] SudokuValue { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku;
            sudoku = new Sudoku("008317000004205109000040070327160904901450000045700800030001060872604000416070080");
            Console.WriteLine("Result: {0}", sudoku.ValidateSudoku());

            // duplicate value 8 in the first row
            sudoku = new Sudoku("008387000004205109000040070327160904901450000045700800030001060872604000416070080");
            Console.WriteLine("Result: {0}", sudoku.ValidateSudoku());
        }
    }
}
