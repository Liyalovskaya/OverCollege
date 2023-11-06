using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OC.Core
{
    public static class StringHelper
    {
        public static string DisplayOption<T>(List<Operation> operations, string title, int highlightId = -1)
        {
            var str = "";
            if (!operations.Any(x => x is T))
            {
                return str;
            }

            var displayIdx = 0;
            str += $"\n\n=====[{title}]===========================\n";
            for (var i = 0; i < operations.Count; i++)
            {
                if (operations[i] is not T) continue;

                if (displayIdx % 4 == 0 && displayIdx != 0)
                {
                    str += "\n";
                }
                if (displayIdx % 4 > 0)
                {
                    str += "\t\t";
                }

                var content = operations[i].Content();
                if (i == highlightId)
                {
                    str += $"<color=#00FF00><link=\"{i}\">{displayIdx + 1}. {content}</link></color>";
                }
                else
                {
                    str += $"<link=\"{i}\">{displayIdx + 1}. {content}</link>";
                }
                displayIdx++;
            }

            return str;
        }
    }
}