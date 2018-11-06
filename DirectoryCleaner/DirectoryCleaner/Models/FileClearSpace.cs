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
    class FileClearSpace : IFileAction
    {
        private static readonly Regex regex = new Regex("[ ]{2,}", RegexOptions.Compiled);

        public bool IsValid => true;

        public string Description => Name;

        public string Name => "Clean up spaces";

        public void DoAction(FileInfo file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FullName);
            fileNameWithoutExtension = regex.Replace(fileNameWithoutExtension, " ");
            file.MoveTo(Path.Combine(file.Directory.FullName, fileNameWithoutExtension + file.Extension));
        }

        public bool EditProperties(InteractionRequest<IConfirmation> interactionRequest)
        {
            return true;
        }

    }
}
