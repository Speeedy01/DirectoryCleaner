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
    class FileAddYearBracket : IFileAction
    {
        public bool IsValid => true;

        public string Description => Name;

        public string Name => "Surround the year number with brackets ()";

        public void DoAction(FileInfo file)
        {
            int bracketIndex;
            Regex numberRegex = new Regex("[0-9][0-9][0-9][0-9]");
            string orgName = Path.GetFileNameWithoutExtension(file.FullName);
            string modName = Path.GetFileNameWithoutExtension(file.FullName);

            MatchCollection matchCol = numberRegex.Matches(modName);
            foreach (Match match in matchCol)
            {
                if (int.Parse(match.Value) > 1950 &&
                    int.Parse(match.Value) < 2500)
                {
                    //Second bracket )
                    modName = AddSecondBracked(orgName, modName, match);
                    //first bracket (
                    bracketIndex = AddFirstBracked(ref modName, match);

                }
            }
            file.MoveTo(Path.Combine(file.Directory.FullName, modName + file.Extension));

        }

        private static int AddFirstBracked(ref string modName, Match match)
        {
            int bracketIndex;
            if (match.Index == 0)
            {
                modName = modName.Insert(0, "(");
                bracketIndex = 0;
            }
            else
            {
                if (modName[match.Index - 1] != '(')
                {
                    if (modName[match.Index - 1] != ' ')
                    {
                        modName = modName.Remove(match.Index - 1, 1);
                        modName = modName.Insert(match.Index - 1, "(");
                        bracketIndex = match.Index - 1;
                    }
                    else
                    {
                        modName = modName.Insert(match.Index, "(");
                        bracketIndex = match.Index;
                    }
                }
                else
                {
                    bracketIndex = match.Index - 1;
                }
                if (modName[bracketIndex - 1] != ' ')
                {
                    modName = modName.Insert(bracketIndex, " ");
                }

            }

            return bracketIndex;
        }

        private static string AddSecondBracked(string orgName, string modName, Match match)
        {
            if (match.Index + 4 >= orgName.Length)
            {
                modName = modName.Insert(match.Index + 4, ")");
            }
            else
            {
                if (modName[match.Index + 4] != ')')
                {
                    if (modName[match.Index + 4] != ' ')
                    {
                        modName = modName.Remove(match.Index + 4, 1);
                    }
                    modName = modName.Insert(match.Index + 4, ")");
                }
                if (modName[match.Index + 5] != ' ')
                {
                    modName = modName.Insert(match.Index + 5, " ");
                }
            }

            return modName;
        }

        public bool EditProperties(InteractionRequest<IConfirmation> interactionRequest)
        {
            return true;
        }
    }
}
