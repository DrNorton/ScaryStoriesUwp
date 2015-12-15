using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.ViewModels.Shell
{
    public class NavMenuItem
    {
        public string Background { get; set; }
        public string Label { get; set; }
        public int Symbol { get; set; }
        public char SymbolAsChar
        {
            get
            {
                return (char)this.Symbol;
            }
        }

        public Type ViewModelType { get; set; }
        public object Arguments { get; set; }
    }
}
