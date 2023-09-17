using System;

namespace tictactoe_interface
{
    public enum CellValue
    {
        CROSS = 'X',
        CIRCLE = 'O',
        EMPTY = 'F'
    }

    public class Game
    {
        public CellValue[,] Grid { get; set; }
        public const int GRIDSIZE = 3;
        public CellValue CurentPlayer;

        public Game()
        {
            CurentPlayer = CellValue.CIRCLE;
            StartGame();
        }

        public void StartGame()
        {
            Grid = new CellValue[GRIDSIZE, GRIDSIZE];
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    Grid[i, j] = CellValue.EMPTY;
                }
            }
        }

        public CellValue GetCell(int row, int col)
        {
            return Grid[row, col];
        }
    
        public bool IsFinished
        {
            get => !CheckWinner().Equals(CellValue.EMPTY) || CheckDraw();

        }


        public CellValue CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (CheckRow(i))
                {
                    return Grid[i, 0];
                }

                if (CheckColumn(i))
                {
                    return Grid[0, i];
                }
            }

            if (CheckDiagonal() || CheckAntiDiagonal())
            {
                return Grid[1, 1];
            }
            return CellValue.EMPTY;
        }
        public bool CheckDraw()
        {
            for (int row = 0; row < GRIDSIZE; row++)
            {
                for (int col = 0; col < GRIDSIZE; col++)
                {
                    if (Grid[row, col] == CellValue.EMPTY)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsMoveValid(int row, int col)
        {

            if (row >= 0 && row < GRIDSIZE && col >= 0 && col < GRIDSIZE)
            {

                return Grid[row, col].Equals(CellValue.EMPTY);
            }
            return false;
        }
        private bool CheckRow(int row)
        {
            return Grid[row, 0] != CellValue.EMPTY && Grid[row, 0] == Grid[row, 1] && Grid[row, 1] == Grid[row, 2];
        }

        private bool CheckColumn(int col)
        {
            return Grid[0, col] != CellValue.EMPTY && Grid[0, col] == Grid[1, col] && Grid[1, col] == Grid[2, col];
        }

        private bool CheckDiagonal()
        {
            return Grid[0, 0] != CellValue.EMPTY && Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2];
        }

        private bool CheckAntiDiagonal()
        {
            return Grid[0, 2] != CellValue.EMPTY && Grid[0, 2] == Grid[1, 1] && Grid[1, 1] == Grid[2, 0];
        }

        public void MakeMove(int row, int col)
        {
            Grid[row, col] = CurentPlayer;
            CurentPlayer = (CurentPlayer == CellValue.CIRCLE) ? CellValue.CROSS : CellValue.CIRCLE;
        }
        public void RemoveCoup(int row, int col)
        {
            Grid[row,col] = CellValue.EMPTY;
        }
    }

}
