using Cysharp.Threading.Tasks;
using PuzzleDemo.Core.UI;
using PuzzleDemo.Features.StartPuzzleDialog;
using PuzzleDemo.Services.Puzzle;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PuzzleDemo.Demo
{
    public class DemoSceneController : MonoBehaviour
    {
        [SerializeField] private Button _openDialogButton;
        [SerializeField] private StartPuzzleContextConfigSO _contextConfig;

        private IUIService _uiService;
        private IPuzzleStartService _puzzleStartService;

        [Inject]
        public void Construct(IUIService uiService, IPuzzleStartService puzzleStartService)
        {
            _uiService = uiService;
            _puzzleStartService = puzzleStartService;
        }

        private void Start()
        {
            _openDialogButton.onClick.AddListener(OnOpenDialogClicked);
        }

        private void OnOpenDialogClicked() => OpenDialogAsync().Forget();

        private async UniTaskVoid OpenDialogAsync()
        {
            var context = new StartPuzzleContext
            {
                PreviewSprite = _contextConfig.PreviewSprites.Length > 0 ? _contextConfig.PreviewSprites[0] : null,
                GridOptions = _contextConfig.GridOptions
            };

            var result = await _uiService.ShowAsync<StartPuzzleDialog, StartPuzzleContext, StartPuzzleResult>(context);

            if (result.Started)
            {
                await _puzzleStartService.StartAsync(new PuzzleStartRequest(context.PreviewSprite, result.GridSize, result.PaymentType));
                Debug.Log($"[Demo] Puzzle started! Grid: {result.GridSize}x{result.GridSize}, Payment: {result.PaymentType}");
            }
            else
            {
                Debug.Log("[Demo] Puzzle dialog closed without starting");
            }
        }
    }
}
