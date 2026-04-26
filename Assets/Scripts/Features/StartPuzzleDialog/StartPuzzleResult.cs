using PuzzleDemo.Services.Puzzle;

namespace PuzzleDemo.Features.StartPuzzleDialog
{
    public class StartPuzzleResult
    {
        public bool Started { get; set; }
        public int GridSize { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
