using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Notiify.Classes;
using Notiify.Helpers;

namespace Notiify.ViewModels
{
    public class ManageSourcesViewModel : ViewModelBase
    {
        private Source _selectedSource;

        public ManageSourcesViewModel()
        {
            NewFolder = new RelayCommand(NewFolderExecute);
            Clone = new RelayCommand(CloneExecute);
            MoveUp = new RelayCommand(MoveUpExecute);
            MoveDown = new RelayCommand(MoveDownExecute);
            Rename = new RelayCommand(RemoveExecute);
        }

        public Source SelectedSource
        {
            get { return _selectedSource; }
            set { Set(ref _selectedSource, value); }
        }

        public ICommand NewFolder { get; }
        public ICommand Clone { get; }
        public ICommand MoveUp { get; }
        public ICommand MoveDown { get; }
        public ICommand Rename { get; }

        private void NewFolderExecute()
        {
            SelectedSource = SourceHelper.NewFolder();
        }

        private void CloneExecute()
        {
            SelectedSource = SelectedSource.NewClone();
        }

        private void MoveUpExecute()
        {
            SelectedSource = SelectedSource.MoveUp();
        }

        private void MoveDownExecute()
        {
            SelectedSource = SelectedSource.MoveDown();
        }

        private void RemoveExecute()
        {
            if (Popup.Show(
                $"Are you sure you want to remove \"{SelectedSource.Name}\"?",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.No)
            {
                return;
            }

            var selectedSourceIndex = SelectedSource.GetIndex();

            SelectedSource.Remove();

            var priorIndex = selectedSourceIndex - 1;
            if (priorIndex >= 0 && App.Sources.Count > priorIndex)
            {
                SelectedSource = App.Sources[priorIndex];
            }
        }
    }
}