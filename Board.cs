using System;
using System.Collections.Generic;
using System.Text;

namespace CsAlgorithm
{
    public class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] _tile {get; private set;}
        public int _size {get; private set;}

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }

        //SideWinder 미로생성 알고리즘
        public void GenerateBySideWinderTree()
        {
            Random rand = new Random();
            //길 막기 작업
            for(int y = 0; y < _size; y++)
            {
                for(int x = 0; x < _size; x++)
                {
                    if(x % 2 == 0  || y % 2 == 0)
                        _tile[y,x] = TileType.Wall;
                    else 
                        _tile[y,x] = TileType.Empty;
                }
            }

            for(int y = 0; y < _size; y++)
            {
                //연속해서 뚫는거 세기위해 카운트 생성
                int Count = 1;

                for(int x = 0; x < _size; x++)
                {
                    if(x % 2 == 0  || y % 2 == 0)
                        continue;

                    if(y == _size - 2 && x == _size - 2)
                        continue;

                    // y 맨끝까지 갔으면 강제로 오른쪽 뚫기
                    if(y == _size - 2)
                    {
                        _tile[y,x+1] = TileType.Empty;
                        continue;
                    }
                    
                    // x 맨끝까지 갔으면 강제로 왼쪽 뚫기
                    if(x == _size - 2)
                    {
                        _tile[y + 1,x] = TileType.Empty;
                        continue;
                    }

                    if(rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        Count++;
                    }
                    else
                    {
                        // 연속해서 오른쪽 뚫은것중 아래 하나 선택해서 뚫기 
                        int randomIndex = rand.Next(0,Count);
                        _tile[y + 1,x - randomIndex * 2] = TileType.Empty;
                        Count = 1;
                    }

                    if(y == _size - 2 && x == _size - 2)
                        continue;
                    
                
                }
            }
        }

        public void Initialize(int size, Player player)
        {
            // 벽을 다 막아주려면 사이즈가 홀수여야함
            if (size % 2 == 0)
                return;

            _player = player;

            _tile = new TileType[size,size];
            _size = size;
            GenerateBySideWinderTree();
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
             for(int y = 0; y < _size; y++)
            {
                for(int x = 0; x < _size; x++)
                {
                    //플레이어 좌표를 가져오고, 그 좌표랑 현재 좌표가 같으면 플레이어 색상적용
                    if (y == _player.PosY && x == _player.PosX )
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else    
                        Console.ForegroundColor = GetTileColor(_tile[y,x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch(type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}