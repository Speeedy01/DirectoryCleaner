using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCleaner.Models
{
    class FileToLower : IFileAction
    {
        public bool IsValid => true;

        public string Description => Name;

        public string Name => "Write the file name to lower chars";

        public void DoAction(FileInfo file)
        {
            file.MoveTo(Path.Combine(file.Directory.FullName, file.Name.ToLower()));
        }
        public bool EditProperties(InteractionRequest<IConfirmation> interactionRequest)
        {
            return true;
        }

    }
}
