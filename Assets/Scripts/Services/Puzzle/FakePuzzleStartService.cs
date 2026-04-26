using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PuzzleDemo.Services.Puzzle
{
    public class FakePuzzleStartService : IPuzzleStartService
    {
        public UniTask StartAsync(PuzzleStartRequest request)
        {
            Debug.Log($"[PuzzleStart] Grid: {request.GridSize}x{request.GridSize}, Payment: {request.PaymentType}");
            return UniTask.CompletedTask;
        }
    }
}
