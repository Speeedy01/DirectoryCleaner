using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCleaner.Models
{
    interface IDirectoryAction : IBaseAction
    {
        void DoAction(System.IO.DirectoryInfo directory);
    }
}
