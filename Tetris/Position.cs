namespace Tetris
{
    public class Position
    {
        public int rows { get; set; }
        public int cols { get; set; }

        public Position(int r, int c)
        {
            this.rows = r;
            this.cols = c;
        }
    }
}
