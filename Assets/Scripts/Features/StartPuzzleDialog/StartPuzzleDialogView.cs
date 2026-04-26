using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleDemo.Features.StartPuzzleDialog
{
    public class StartPuzzleDialogView : MonoBehaviour
    {
        [SerializeField] private Image _previewImage;
        [SerializeField] private Toggle[] _gridToggles;
        [SerializeField] private TextMeshProUGUI[] _gridLabels;
        [SerializeField] private Button _freeButton;
        [SerializeField] private Button _coinsButton;
        [SerializeField] private Button _adsButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _coinsCostLabel;
        [SerializeField] private TextMeshProUGUI _balanceLabel;

        public event Action<int> OnGridSizeSelected;
        public event Action OnFreeClicked;
        public event Action OnCoinsClicked;
        public event Action OnAdsClicked;
        public event Action OnCloseClicked;

        private void OnEnable()
        {
            for (int i = 0; i < _gridToggles.Length; i++)
            {
                int index = i;
                _gridToggles[i].onValueChanged.AddListener(isOn =>
                {
                    if (isOn)
                        OnGridSizeSelected?.Invoke(index);
                });
            }

            _freeButton.onClick.AddListener(() => OnFreeClicked?.Invoke());
            _coinsButton.onClick.AddListener(() => OnCoinsClicked?.Invoke());
            _adsButton.onClick.AddListener(() => OnAdsClicked?.Invoke());
            _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        }

        private void OnDisable()
        {
            foreach (var toggle in _gridToggles)
                toggle.onValueChanged.RemoveAllListeners();

            _freeButton.onClick.RemoveAllListeners();
            _coinsButton.onClick.RemoveAllListeners();
            _adsButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        public void SetPreview(Sprite sprite)
        {
            _previewImage.sprite = sprite;
        }

        public void SetGridOptions(GridOption[] options)
        {
            for (int i = 0; i < options.Length && i < _gridToggles.Length; i++)
            {
                _gridLabels[i].text = $"{options[i].Size}x{options[i].Size}";
                _gridToggles[i].gameObject.SetActive(true);
                _gridToggles[i].SetIsOnWithoutNotify(i == 0);
            }

            for (int i = options.Length; i < _gridToggles.Length; i++)
            {
                _gridToggles[i].gameObject.SetActive(false);
                _gridToggles[i].SetIsOnWithoutNotify(false);
            }
        }

        public void SetCoinsCost(int cost)
        {
            _coinsCostLabel.text = cost.ToString();
        }

        public void SetCoinsButtonInteractable(bool interactable)
        {
            _coinsButton.interactable = interactable;
        }

        public void SetFreeAvailable(bool beFreeAvailable)
        {
            _adsButton.gameObject.SetActive(!beFreeAvailable);
            _coinsButton.gameObject.SetActive(!beFreeAvailable);
            _freeButton.gameObject.SetActive(beFreeAvailable);
        }

        public void SetBalance(int balance)
        {
            _balanceLabel.text = balance.ToString();
        }
    }
}
