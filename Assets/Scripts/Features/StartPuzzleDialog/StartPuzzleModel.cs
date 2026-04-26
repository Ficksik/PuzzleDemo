namespace PuzzleDemo.Features.StartPuzzleDialog
{
    public class StartPuzzleModel
    {
        private readonly StartPuzzleContext _context;
        private int _selectedOptionIndex = 0;

        public StartPuzzleModel(StartPuzzleContext context)
        {
            _context = context;
        }

        public StartPuzzleContext Context => _context;

        public int SelectedOptionIndex
        {
            get => _selectedOptionIndex;
            set => _selectedOptionIndex = value;
        }

        public GridOption SelectedOption => _context.GridOptions[_selectedOptionIndex];

        public int CurrentCoinsCost => SelectedOption.CoinsCost;

        public bool IsFreeAvailable => SelectedOption.IsFreeAvailable;
    }
}
