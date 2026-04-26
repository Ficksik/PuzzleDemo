using System;

namespace PuzzleDemo.Core.UI
{
    /// <summary>
    /// Fullscreen blocking loading indicator.
    /// Show() returns an IDisposable scope; overlay hides automatically when the scope is disposed.
    /// Usage: using (_loadingOverlay.Show()) { await SomeLongOperation(); }
    /// </summary>
    public interface ILoadingOverlay
    {
        IDisposable Show();
    }
}
