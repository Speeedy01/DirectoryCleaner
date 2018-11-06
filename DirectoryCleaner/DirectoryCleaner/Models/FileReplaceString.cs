using DirectoryCleaner.ViewModels;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCleaner.Models
{
    class FileReplaceString : BindableBase, IFileAction
    {
        private string before;
        public string Before
        {
            get { return before; }
            set
            {
                SetProperty(ref before, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string after;
        public string After
        {
            get { return after; }
            set
            {
                SetProperty(ref after, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public bool IsValid => string.IsNullOrWhiteSpace(Before) || after == null;

        public string Description => "Replace a specific string in file name";

        public string Name => $"Replace {Before} with {After}";

        public void DoAction(FileInfo file)
        {
            file.MoveTo(Path.Combine(file.Directory.FullName, file.Name.Replace(Before, After)));
        }

        public bool EditProperties(InteractionRequest<IConfirmation> interactionRequest)
        {
            bool dialogConfirmed = false;
            var viewModel = new FileReplaceStringViewModel
            {
                DialogResult = null,
                Before = Before,
                After = After
            };
            var view = new Views.FileReplaceStringView
            {
                DataContext = viewModel
            };

            interactionRequest.Raise(new Confirmation { Title = Description, Content = view }, c =>
            {
                dialogConfirmed = c.Confirmed;
                if (c.Confirmed)
                {
                    Before = viewModel.Before;
                    After = viewModel.After;
                }
            });

            return dialogConfirmed;
        }
    }
}
