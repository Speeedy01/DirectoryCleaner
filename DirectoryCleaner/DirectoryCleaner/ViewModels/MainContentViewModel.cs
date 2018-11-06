using DirectoryCleaner.Models;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DirectoryCleaner.ViewModels
{
    internal class MainContentViewModel : BindableBase
    {

        public ObservableCollection<IBaseAction> AvailableActions { get; set; }
        private IBaseAction selectedAvaiableAction;
        public IBaseAction SelectedAvaiableAction
        {
            get { return selectedAvaiableAction; }
            set { SetProperty(ref selectedAvaiableAction, value); }
        }

        public ObservableCollection<IBaseAction> ToRunActions { get; set; }
        private IBaseAction selectedToRunAction;
        public IBaseAction SelectedToRunAction
        {
            get { return selectedToRunAction; }
            set { SetProperty(ref selectedToRunAction, value); }
        }

        private string actionDirectory;
        public string ActionDirectory
        {
            get { return actionDirectory; }
            set { SetProperty(ref actionDirectory, value); }
        }
      
        public DelegateCommand SelectActionDirectoryCommand { get; private set; }
        public DelegateCommand AddActionCommand { get; set; }
        public DelegateCommand RemoveActionCommand { get; set; }
        public DelegateCommand AddDefaultCommand { get; set; }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }

        public MainContentViewModel()
        {
            AvailableActions = new ObservableCollection<IBaseAction>();

            AvailableActions.AddRange(
                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IBaseAction)) && t.IsClass)
                    .Select(fileActionType => (IBaseAction)Activator.CreateInstance(fileActionType, null)));

            ToRunActions = new ObservableCollection<IBaseAction>();

            SelectActionDirectoryCommand = new DelegateCommand(SelectDirectory);
            AddActionCommand = new DelegateCommand(AddAction, CanAddAction).ObservesProperty(() => SelectedAvaiableAction);
            RemoveActionCommand = new DelegateCommand(RemoveAction, CanRemoveAction).ObservesProperty(() => SelectedToRunAction);

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        private void SelectDirectory()
        {
            var dialog = new WPFFolderBrowser.WPFFolderBrowserDialog("Choose folder")
            {
                InitialDirectory = ActionDirectory
            };
            if (dialog.ShowDialog() == true)
            {
                ActionDirectory = dialog.FileName;
            }
        }

        private void AddAction()
        {
            var selectedType = SelectedAvaiableAction.GetType();
            var actionToAdd = (IBaseAction)Activator.CreateInstance(selectedType);
            if (actionToAdd.EditProperties(ConfirmationRequest))
            {
                ToRunActions.Add(actionToAdd);
            }           
        }

        private bool CanAddAction()
        {
            return SelectedAvaiableAction != null;
        }

        private void RemoveAction()
        {
            ToRunActions.Remove(SelectedToRunAction);
        }

        private bool CanRemoveAction()
        {
            return SelectedToRunAction != null;
        }

        private void EditToRunAction()
        {
            SelectedToRunAction?.EditProperties(ConfirmationRequest);
        }
    }
}
