using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PuzzleDemo.Core.UI
{
    public class UIService : IUIService
    {
        private readonly DialogFactory _factory;
        private readonly HashSet<Type> _activeDialogs = new();

        public UIService(DialogFactory factory)
        {
            _factory = factory;
        }

        public async UniTask<TResult> ShowAsync<TDialog, TContext, TResult>(TContext context, CancellationToken ct = default)
            where TDialog : BaseDialog<TContext, TResult>
        {
            var dialogType = typeof(TDialog);

            if (!_activeDialogs.Add(dialogType))
            {
                throw new InvalidOperationException($"Dialog {dialogType.Name} is already active");
            }

            try
            {
                var dialog = _factory.GetOrCreate<TDialog>();

                dialog.Initialize(context);
                await dialog.ShowAsync(ct);

                var result = await dialog.WaitForResult().AttachExternalCancellation(ct);

                await dialog.HideAsync(ct);
                return result;
            }
            finally
            {
                _activeDialogs.Remove(dialogType);
            }
        }

        public void Hide<TDialog>() where TDialog : IDialog
        {
            _activeDialogs.Remove(typeof(TDialog));
        }
    }
}
