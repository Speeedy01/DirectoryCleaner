using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCleaner.Models
{
    internal interface IBaseAction
    {
        bool IsValid { get; }

        string Description { get; }

        string Name { get; }

        bool EditProperties(InteractionRequest<IConfirmation> interactionRequest);
    }
}
