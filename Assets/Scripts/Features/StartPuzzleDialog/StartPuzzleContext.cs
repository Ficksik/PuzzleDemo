using UnityEngine;

namespace PuzzleDemo.Features.StartPuzzleDialog
{
    public class StartPuzzleContext
    {
        public Sprite PreviewSprite { get; set; }
        public GridOption[] GridOptions { get; set; }
    }

    [System.Serializable]
    public class GridOption
    {
        [SerializeField] private int _size;
        [SerializeField] private int _coinsCost;
        [SerializeField] private bool _isFreeAvailable;

        public int Size => _size;
        public int CoinsCost => _coinsCost;
        public bool IsFreeAvailable => _isFreeAvailable;

        public GridOption() { }

        public GridOption(int size, int coinsCost, bool isFreeAvailable)
        {
            _size = size;
            _coinsCost = coinsCost;
            _isFreeAvailable = isFreeAvailable;
        }
    }
}
