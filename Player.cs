using System;

namespace CsAlgorithm
{
    public class Player
    {
        public int PosX{get; set;}
        public int PosY{get; set;}
        
        Board _board;

        public void Initialize(int posY, int posX, int destY, int destX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
        }
    }
}
