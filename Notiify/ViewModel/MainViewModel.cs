using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.NotificationTypes;

namespace Notiify.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Notifications = new ObservableCollection<INotification>();
            GenerateNotification =
                new RelayCommand(
                    () =>
                    {
                        Notifications.Add(new TextNotification
                        {
                            Title = "test",
                            Content = new Random().NextDouble().ToString()
                        });
                    });
        }

        public ObservableCollection<INotification> Notifications { get; }
        public ICommand GenerateNotification { get; }
    }
}