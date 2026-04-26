using Cysharp.Threading.Tasks;

namespace PuzzleDemo.Services.Ads
{
    public class FakeAdsService : IAdsService
    {
        public async UniTask<bool> ShowAsync()
        {
            await UniTask.Delay(1500);
            return true;
        }
    }
}
