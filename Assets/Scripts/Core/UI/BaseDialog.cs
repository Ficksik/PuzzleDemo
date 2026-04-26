using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace PuzzleDemo.Core.UI
{
    public abstract class BaseDialog<TContext, TResult> : MonoBehaviour, IDialog
    {
        private UniTaskCompletionSource<TResult> _tcs;

        public UniTask<TResult> WaitForResult() => _tcs.Task;

        public void Initialize(TContext context)
        {
            _tcs = new UniTaskCompletionSource<TResult>();
            OnInitialize(context);
        }

        protected virtual void OnInitialize(TContext context) { }

        public virtual UniTask ShowAsync(CancellationToken ct = default)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public virtual UniTask HideAsync(CancellationToken ct = default)
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        protected void Complete(TResult result)
        {
            _tcs?.TrySetResult(result);
        }
    }
}
