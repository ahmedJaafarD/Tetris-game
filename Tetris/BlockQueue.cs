using System;


namespace Tetris
{
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new ZBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock()

        };


        public BlockQueue()
        {
            NextBlock = getRandBlock();
        }


        private readonly Random rand = new Random();


        public Block NextBlock { get; private set; }

        public Block getRandBlock()
        {
            return blocks[rand.Next(blocks.Length)];
        }


        public Block GetAndUpdate()
        {
            Block block = NextBlock;

            do
            {
                NextBlock = getRandBlock();
            }
            while (block.ID == NextBlock.ID);

            return block;
        }
    }
}
