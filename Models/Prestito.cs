using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BibliotecaWPF.Models
{
    public partial class Prestito : ObservableObject
    {
        [ObservableProperty]
        private string libroISBN;

        [ObservableProperty]
        private string socioID;

        [JsonIgnore]
        public Libro Libro { get; set; }

        [JsonIgnore]
        public Socio Socio { get; set; }

        [JsonIgnore]
        public  bool InRitardo { get; set; }
    }
}
