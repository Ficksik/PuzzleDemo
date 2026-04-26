using PuzzleDemo.Core.UI;
using PuzzleDemo.Demo;
using PuzzleDemo.Features.StartPuzzleDialog;
using PuzzleDemo.Services.Ads;
using PuzzleDemo.Services.Puzzle;
using PuzzleDemo.Services.Wallet;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PuzzleDemo.Core.Bootstrap
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private DialogPrefabRegistrySO _prefabRegistry;
        [SerializeField] private LoadingOverlay _loadingOverlay;
        [SerializeField] private DemoSceneController _demoSceneController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_uiRoot);
            builder.RegisterInstance(_prefabRegistry);
            builder.RegisterComponent(_loadingOverlay).As<ILoadingOverlay>();

            // Services
            builder.Register<IWalletService, FakeWalletService>(Lifetime.Singleton);
            builder.Register<IAdsService, FakeAdsService>(Lifetime.Singleton);
            builder.Register<IPuzzleStartService, FakePuzzleStartService>(Lifetime.Singleton);

            // UI Framework
            builder.Register<DialogFactory>(Lifetime.Singleton);
            builder.Register<IUIService, UIService>(Lifetime.Singleton);

            // Presenters (new instance for each dialog)
            builder.Register<StartPuzzleDialogPresenter>(Lifetime.Transient);

            // Demo
            builder.RegisterComponent(_demoSceneController);
        }
    }
}
