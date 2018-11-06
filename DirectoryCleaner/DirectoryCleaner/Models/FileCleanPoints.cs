using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DirectoryCleaner.Models
{
    class FileCleanPoints : IFileAction
    {
        public bool IsValid => true;

        public string Description => Name;

        public string Name => "Replace all . with space";

        public void DoAction(FileInfo file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FullName);
            file.MoveTo(Path.Combine(file.Directory.FullName, fileNameWithoutExtension.Replace(".", " ") + file.Extension));
        }
        public bool EditProperties(InteractionRequest<IConfirmation> interactionRequest)
        {
            return true;
        }

    }
}
