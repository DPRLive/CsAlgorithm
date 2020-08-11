using System;
using System.Collections.Generic;
using System.Text;

namespace CsAlgorithm
{
    public class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] _tile;
        public int _size;

        public enum TileType
        {
            Empty,
            Wall,
        }

        // binary tree 미로생성 알고리즘 
        public void GenerateByBinaryTree() 
        {
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

            // 랜덤으로 우측 혹은 아래로 길뚫기
            Random rand = new Random();
            for(int y = 0; y < _size; y++)
            {
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
                    // 랜덤으로 우측뚫기
                    if(rand.Next(0,2) == 0)
                    {
                        _tile[y,x+1] = TileType.Empty;
                    }
                    //우측 아니면 아래 뚫기
                    else
                    {
                        _tile[y+1,x] = TileType.Empty;

                    }
                }
            }
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

        public void Initialize(int size)
        {
            // 벽을 다 막아주려면 사이즈가 홀수여야함
            if (size % 2 == 0)
                return;

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