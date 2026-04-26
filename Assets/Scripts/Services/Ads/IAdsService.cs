using Cysharp.Threading.Tasks;

namespace PuzzleDemo.Services.Ads
{
    public interface IAdsService
    {
        UniTask<bool> ShowAsync();
    }
}
