using Cysharp.Threading.Tasks;
using System.Threading;

namespace PuzzleDemo.Core.UI
{
    public interface IUIService
    {
        UniTask<TResult> ShowAsync<TDialog, TContext, TResult>(TContext context, CancellationToken ct = default)
            where TDialog : BaseDialog<TContext, TResult>;

        void Hide<TDialog>() where TDialog : IDialog;
    }
}
