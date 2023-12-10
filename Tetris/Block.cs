using System.Collections.Generic;

namespace Tetris
{
    public abstract class Block
    {

        protected abstract Position[][] Tiles { get; }
        protected abstract Position startOffSet { get; }

        public abstract int ID { get; }

        private int rotationState;
        private Position offSet;

        public Block()
        {
            offSet = new Position(startOffSet.rows, startOffSet.cols);
        }

        public IEnumerable<Position> TilePositions()
        {
            foreach ( Position p in Tiles[rotationState])
            {
                yield return new Position(p.rows + offSet.rows, p.cols + offSet.cols);
            }
        }

        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()
        {
            if(rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void Move(int r,int c)
        {
            offSet.rows += r;
            offSet.cols += c;
        }


        public void Reset()
        {
            offSet.rows = startOffSet.rows;
            offSet.cols = startOffSet.cols;
            rotationState = 0;

        }
    }
}
