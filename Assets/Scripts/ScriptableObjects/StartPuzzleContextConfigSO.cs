using PuzzleDemo.Features.StartPuzzleDialog;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Puzzle/Start Puzzle Context Config")]
    public class StartPuzzleContextConfigSO : ScriptableObject
    {
        [SerializeField] private Sprite[] _previewSprites;
        [SerializeField] private GridOption[] _gridOptions;

        public Sprite[] PreviewSprites => _previewSprites;
        public GridOption[] GridOptions => _gridOptions;
    }
}
