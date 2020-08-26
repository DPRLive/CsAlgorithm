using System;

namespace CsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board._size - 2, board._size - 2, board);
            board.Render();
        }
    }
}
