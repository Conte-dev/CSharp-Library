using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaWPF.Models
{
     public partial class DashBoardCard : ObservableObject
    {
        public string Titolo { get; set; }
        public string Valore { get; set; }
    }
}
