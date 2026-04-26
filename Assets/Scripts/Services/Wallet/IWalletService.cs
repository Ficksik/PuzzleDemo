using Cysharp.Threading.Tasks;
using System;

namespace PuzzleDemo.Services.Wallet
{
    public interface IWalletService
    {
        int Balance { get; }
        event Action<int> OnBalanceChanged;

        UniTask<bool> TrySpendAsync(int amount);
    }
}
