using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCleaner.Models
{
    class FileCleanUnderlines : IFileAction
    {
        public bool IsValid => true;

        public string Description => Name;

        public string Name => "Replace all _ with space";

        public void DoAction(FileInfo file)
        {
            file.MoveTo(Path.Combine(file.Directory.FullName, file.Name.Replace("_", " ")));
        }
        public bool EditProperties(InteractionRequest<IConfirmation> interactionRequest)
        {
            return true;
        }

    }
}
