using PuzzleDemo.Core.UI;
using UnityEngine;
using VContainer;

namespace PuzzleDemo.Features.StartPuzzleDialog
{
    public class StartPuzzleDialog : BaseDialog<StartPuzzleContext, StartPuzzleResult>
    {
        [SerializeField] private StartPuzzleDialogView _view;

        private StartPuzzleDialogPresenter _presenter;

        [Inject]
        public void Construct(StartPuzzleDialogPresenter presenter)
        {
            _presenter = presenter;
        }

        protected override void OnInitialize(StartPuzzleContext context)
        {
            var model = new StartPuzzleModel(context);
            _presenter.Bind(_view, model, Complete);
        }
    }
}
