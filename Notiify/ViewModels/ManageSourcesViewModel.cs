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
            CollectionMouseDown = new RelayCommand(CollectionMouseDownExecute);
            NewFolder = new RelayCommand(NewFolderExecute);
            Clone = new RelayCommand(CloneExecute);
            MoveUp = new RelayCommand(MoveUpExecute);
            MoveDown = new RelayCommand(MoveDownExecute);
            Delete = new RelayCommand(DeleteExecute);
        }

        public Source SelectedSource
        {
            get { return _selectedSource; }
            set { Set(ref _selectedSource, value); }
        }

        public ICommand CollectionMouseDown { get; }
        public ICommand NewFolder { get; }
        public ICommand Clone { get; }
        public ICommand MoveUp { get; }
        public ICommand MoveDown { get; }
        public ICommand Delete { get; }

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

        private void DeleteExecute()
        {
            SelectedSource.Remove();
            ResetSelection();
        }

        private void CollectionMouseDownExecute()
        {
            ResetSelection();
        }

        private void ResetSelection()
        {
            SelectedSource = null;
        }
    }
}