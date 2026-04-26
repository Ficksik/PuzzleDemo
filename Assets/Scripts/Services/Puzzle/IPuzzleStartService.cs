using Cysharp.Threading.Tasks;

namespace PuzzleDemo.Services.Puzzle
{
    public interface IPuzzleStartService
    {
        UniTask StartAsync(PuzzleStartRequest request);
    }
}
