using System;
using System.Collections.Generic;
using System.Data;

namespace _02_QueensPuzzle
{
    public class Program
    {
        private static HashSet<int> attackRows = new HashSet<int>();
        private static HashSet<int> attackCols = new HashSet<int>();
        private static HashSet<int> attackLeftDiag = new HashSet<int>();
        private static HashSet<int> attackRightDiag = new HashSet<int>();

        static void Main(string[] args)
        {
            var board = new bool[8,8];

            PutQueen(board, 0);
        }

        private static void PutQueen(bool[,] board, int row)
        {
            if (row == board.GetLength(0) )
            {
                PrintBoard(board);
                return;
            }

            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (!IsAttacked(row, col))
                {
                    board[row, col] = true;
                    attackRows.Add(row);
                    attackCols.Add(col);
                    attackLeftDiag.Add(row - col);
                    attackRightDiag.Add(row + col);

                    PutQueen(board, row+ 1);

                    board[row, col] = false;
                    attackRows.Remove(row);
                    attackCols.Remove(col);
                    attackLeftDiag.Remove(row - col);
                    attackRightDiag.Remove(row + col);
                }
            }
        }

        private static bool IsAttacked( in int row, in int col)
        {
            return attackRows.Contains(row) || 
                   attackCols.Contains(col) || 
                   attackLeftDiag.Contains(row - col) ||
                   attackRightDiag.Contains(row + col);
        }

        private static void PrintBoard(bool[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
