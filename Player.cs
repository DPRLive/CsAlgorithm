  
using System;

namespace CsAlgorithm
{
    public class Player
    {
        public int PosX{get; private set;}
        public int PosY{get; private set;}
        Random _random = new Random();
        
        Board _board;

        public void Initialize(int posY, int posX, int destY, int destX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                // 0.1초마다 실행될 로직 
                int randValue = _random.Next(0, 5);
                switch (randValue)
                {
                    case 0: //상
                        if(PosY - 1 >= 0 && _board._tile[PosY - 1, PosX] == Board.TileType.Empty) 
                        PosY = PosY - 1;
                        break;
                    case 1: // 하
                        if(PosY + 1 < _board._size &&_board._tile[PosY + 1, PosX] == Board.TileType.Empty) 
                        PosY = PosY + 1;   
                        break;         
                    case 2: // 좌
                        if(PosX - 1 >= 0 && _board._tile[PosY, PosX - 1] == Board.TileType.Empty) 
                        PosX = PosX - 1;   
                        break;         
                    case 3: // 우
                        if(PosX + 1 < _board._size && _board._tile[PosY, PosX + 1] == Board.TileType.Empty) 
                        PosX = PosX + 1;            
                        break;

                }
            }
        }
    }
}