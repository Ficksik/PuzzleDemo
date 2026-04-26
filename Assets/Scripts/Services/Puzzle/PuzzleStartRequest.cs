using UnityEngine;

namespace PuzzleDemo.Services.Puzzle
{
    public struct PuzzleStartRequest
    {
        public Sprite PreviewSprit;
        public int GridSize;
        public PaymentType PaymentType;

        public PuzzleStartRequest(Sprite sprite, int gridSize, PaymentType paymentType)
        {
            PreviewSprit = sprite;
            GridSize = gridSize;
            PaymentType = paymentType;
        }
    }

    public enum PaymentType
    {
        Free,
        Coins,
        Ads
    }
}
