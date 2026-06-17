using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaWPF.Models
{
    public partial class Socio : ObservableObject
    {
        [ObservableProperty]
        private string nome;

        [ObservableProperty]
        private string cognome;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string email;
    }
}
