using System;

namespace Notiify.Interfaces
{
    public interface INotification
    {
        string Title { get; }
        DateTime EventDateTime { get; }
        IScannerEventArgs ScannerArgs { get; }
    }
}