namespace Tetris
{
    public class GameState
    {
        private Block currentBlock;


        public Block CurrentBlock
        {

            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public Block HeldBlock { get; private set; }
        public bool CanHold { get; private set; }


        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
        }

        private bool BlockFits()
        {
            foreach(Position p in CurrentBlock.TilePositions())
            {
                if(!GameGrid.isEmpty(p.rows, p.cols))
                {
                    return false;
                }
            }

            return true;
        }

        public void RotateBlockCW()
        {
            currentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }


        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits() )
            {
                CurrentBlock.RotateCW();
            }
        }


        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }


        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }



        private bool IsGameOver()
        {
            return !(GameGrid.isRowEmpty(0) && GameGrid.isRowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.rows, p.cols] = CurrentBlock.ID;
            }

            Score += GameGrid.ClearFullRow();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
        }



        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if(!BlockFits())
            {
                currentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }


        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }


        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }

        public int BlockDropDistance()
        {
            int drop = GameGrid.rows;

            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        private int TileDropDistance(Position p)
        {
            int drop = 0;

            while (GameGrid.isEmpty(p.rows + drop + 1, p.cols))
            {
                drop++;
            }

            return drop;
        }
    }
}
