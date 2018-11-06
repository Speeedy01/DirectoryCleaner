using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DirectoryCleaner.ViewModels
{
    class FileReplaceStringViewModel : BindableBase
    {
        public FileReplaceStringViewModel()
        {
            Accept = new DelegateCommand(() => DialogResult = true);
            Accept = new DelegateCommand(() => DialogResult = false);
        }

        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }

        private string before;
        public string Before
        {
            get { return before; }
            set { SetProperty(ref before, value); }
        }
        private string after;
        public string After
        {
            get { return after; }
            set { SetProperty(ref after, value); }
        }

        private ICommand accept;
        public ICommand Accept
        {
            get { return accept; }
            set { SetProperty(ref accept, value); }
        }

        private ICommand cancel;
        public ICommand Cancel
        {
            get { return cancel; }
            set { SetProperty(ref cancel, value); }
        }
    }
}
