using Cysharp.Threading.Tasks;
using System.Threading;

namespace PuzzleDemo.Core.UI
{
    public interface IDialog
    {
        UniTask ShowAsync(CancellationToken ct = default);
        UniTask HideAsync(CancellationToken ct = default);
    }
}
