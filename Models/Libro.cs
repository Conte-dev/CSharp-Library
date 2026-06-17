using CommunityToolkit.Mvvm.ComponentModel;

namespace BibliotecaWPF.Models
{
    public partial class Libro : ObservableObject
    {
        [ObservableProperty]
        private string titolo;

        [ObservableProperty]
        private string autore;

        [ObservableProperty]
        private string isbn;

        [ObservableProperty]
        private int annoPubblicazione;

        [ObservableProperty]
        private string genere;

        [ObservableProperty]
        private StatoPrestito statoPrestito;

        [ObservableProperty]
        private int durataPrestito;
    }
}