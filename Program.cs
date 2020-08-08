using System;

namespace CsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize(25);
            board.Render();
        }
    }
}
