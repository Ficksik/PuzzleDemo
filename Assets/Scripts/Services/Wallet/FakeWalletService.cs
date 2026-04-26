using Cysharp.Threading.Tasks;
using System;

namespace PuzzleDemo.Services.Wallet
{
    public class FakeWalletService : IWalletService
    {
        private int _balance = 100;

        public int Balance => _balance;

        public event Action<int> OnBalanceChanged;

        public UniTask<bool> TrySpendAsync(int amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                OnBalanceChanged?.Invoke(_balance);
                return UniTask.FromResult(true);
            }

            return UniTask.FromResult(false);
        }
    }
}
