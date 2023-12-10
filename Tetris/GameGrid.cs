using System.Windows;

namespace Tetris
{
    public class GameGrid
    {
        private readonly int[,] grid;

        public int rows { get; }
        public int cols { get; }

        public int this[int r,int c]
        {
            get => grid[r,c];
            set => grid[r,c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            this.rows = rows;
            this.cols = columns;
            grid = new int[rows,columns];
        }

        public bool isInside(int r,int c)
        {
            return r >= 0 && r < rows && c >= 0 && c < cols;
        }

        public bool isEmpty(int r, int c)
        {
            //return grid[r,c] == 0 && isInside(r,c);
            return isInside(r, c) && grid[r, c] == 0;

        }

        public bool isRowFull(int r)
        {
            for (int i = 0; i < cols; i++)
            {
                if (grid[r,i] == 0 ) return false;

            }

            return true;
        }

        public bool isRowEmpty(int r)
        {
            for(int i = 0; i < cols; i++)
            {
                if (grid[r,i] != 0 ) return false;
            }

            return true;
        }


        private void ClearRow(int r)
        {
            for ( int c=0 ; c<cols; c++)
            {
                grid[r, c] = 0;
            }

        }

        private void MoveRowDown(int r, int numRows)
        {
            for(int c=0 ; c < cols; c++)
            {
                grid[r+numRows,c] = grid[r,c];
                grid[r, c] = 0;
            }
        }


        public int ClearFullRow()
        {
            int cleared = 0;

            for (int r= rows -1; r>=0 ; r--)
            {
                if(isRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);

                }
            }
            return cleared;
        }

    }
}
