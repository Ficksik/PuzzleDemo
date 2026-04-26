using Cysharp.Threading.Tasks;
using PuzzleDemo.Core.UI;
using PuzzleDemo.Services.Ads;
using PuzzleDemo.Services.Wallet;
using System;
using PuzzleDemo.Services.Puzzle;

namespace PuzzleDemo.Features.StartPuzzleDialog
{
    public class StartPuzzleDialogPresenter
    {
        private readonly IWalletService _walletService;
        private readonly IAdsService _adsService;
        private readonly ILoadingOverlay _loadingOverlay;

        private StartPuzzleDialogView _view;
        private StartPuzzleModel _model;
        private Action<StartPuzzleResult> _onComplete;

        public StartPuzzleDialogPresenter(
            IWalletService walletService,
            IAdsService adsService,
            ILoadingOverlay loadingOverlay)
        {
            _walletService = walletService;
            _adsService = adsService;
            _loadingOverlay = loadingOverlay;
        }

        public void Bind(StartPuzzleDialogView view, StartPuzzleModel model, Action<StartPuzzleResult> onComplete)
        {
            _view = view;
            _model = model;
            _onComplete = onComplete;

            InitializeView();
            SubscribeToViewEvents();
            SubscribeToWalletEvents();
        }

        private void InitializeView()
        {
            _view.SetPreview(_model.Context.PreviewSprite);
            _view.SetGridOptions(_model.Context.GridOptions);
            _view.SetCoinsCost(_model.CurrentCoinsCost);
            _view.SetFreeAvailable(_model.IsFreeAvailable);
            _view.SetBalance(_walletService.Balance);
            UpdateCoinsButtonState();
        }

        private void SubscribeToViewEvents()
        {
            _view.OnGridSizeSelected += OnGridSizeSelected;
            _view.OnFreeClicked += OnFreeClicked;
            _view.OnCoinsClicked += OnCoinsClicked;
            _view.OnAdsClicked += OnAdsClicked;
            _view.OnCloseClicked += OnCloseClicked;
        }

        private void SubscribeToWalletEvents()
        {
            _walletService.OnBalanceChanged += OnBalanceChanged;
        }

        private void OnGridSizeSelected(int optionIndex)
        {
            _model.SelectedOptionIndex = optionIndex;
            _view.SetCoinsCost(_model.CurrentCoinsCost);
            _view.SetFreeAvailable(_model.IsFreeAvailable);
            UpdateCoinsButtonState();
        }

        private void OnFreeClicked()
        {
            if (!_model.IsFreeAvailable)
                return;

            CompleteWithResult(true, PaymentType.Free);
        }

        private void OnCoinsClicked() => OnCoinsClickedAsync().Forget();

        private async UniTaskVoid OnCoinsClickedAsync()
        {
            var cost = _model.CurrentCoinsCost;
            bool success;
            using (_loadingOverlay.Show())
            {
                success = await _walletService.TrySpendAsync(cost);
            }

            if (success)
                CompleteWithResult(true, PaymentType.Coins);
        }

        private void OnAdsClicked() => OnAdsClickedAsync().Forget();

        private async UniTaskVoid OnAdsClickedAsync()
        {
            bool success;
            using (_loadingOverlay.Show())
            {
                success = await _adsService.ShowAsync();
            }

            if (success)
                CompleteWithResult(true, PaymentType.Ads);
        }

        private void OnCloseClicked()
        {
            CompleteWithResult(false, PaymentType.Free);
        }

        private void OnBalanceChanged(int newBalance)
        {
            _view.SetBalance(newBalance);
            UpdateCoinsButtonState();
        }

        private void UpdateCoinsButtonState()
        {
            var canAfford = _walletService.Balance >= _model.CurrentCoinsCost;
            _view.SetCoinsButtonInteractable(canAfford);
        }

        public void Unbind()
        {
            if (_view != null)
            {
                _view.OnGridSizeSelected -= OnGridSizeSelected;
                _view.OnFreeClicked -= OnFreeClicked;
                _view.OnCoinsClicked -= OnCoinsClicked;
                _view.OnAdsClicked -= OnAdsClicked;
                _view.OnCloseClicked -= OnCloseClicked;
            }

            _walletService.OnBalanceChanged -= OnBalanceChanged;
        }

        private void CompleteWithResult(bool started, PaymentType paymentType)
        {
            Unbind();

            var result = new StartPuzzleResult
            {
                Started = started,
                GridSize = _model.SelectedOption.Size,
                PaymentType = paymentType
            };

            _onComplete?.Invoke(result);
        }
    }
}
