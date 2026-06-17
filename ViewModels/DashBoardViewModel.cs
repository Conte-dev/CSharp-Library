using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using BibliotecaWPF.Models;
using System.Linq;
namespace BibliotecaWPF.ViewModels
{
    public class StatCard
    {
        public string Titolo { get; set; }
        public string Valore { get; set; }
    }
    public partial class DashBoardViewModel : ObservableObject
    {
        private readonly MainViewModel mainVM;
        [ObservableProperty]
        private int libriInPrestito;
        [ObservableProperty]
        private int totaleLibri;

        public ObservableCollection<StatCard> Cards { get; set; } = new();
        public DashBoardViewModel(MainViewModel mainViewModel)
        {
            mainVM = mainViewModel;
        }
        public void AggiornaStatistiche()
        {
            var libri = mainVM.Libri;
            var prestiti = mainVM.Prestiti;
            var soci = mainVM.Soci;
            TotaleLibri = libri.Count;
            LibriInPrestito = libri.Count(l => l.StatoPrestito == StatoPrestito.Attivo);
            var libriDisponibili = libri.Count(l => l.StatoPrestito == StatoPrestito.Restituito);
            var prestitiInRitardo = libri.Count(l => l.StatoPrestito == StatoPrestito.InRitardo);
            var autoreGroup = libri
                .Where(l => !string.IsNullOrEmpty(l.Autore))
                .GroupBy(l => l.Autore)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();
            var autore = autoreGroup?.Key ?? "-";
            var socioGroup = prestiti
                .Where(p => !string.IsNullOrEmpty(p.SocioID))
                .GroupBy(p => p.SocioID)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();
            var socio = soci.FirstOrDefault(s => s.Id == socioGroup?.Key);
            var socioNome = socio != null ? $"{socio.Nome} {socio.Cognome}" : "-";
            var durate = libri
                .Where(l => l.DurataPrestito > 0)
                .Select(l => l.DurataPrestito)
                .ToList();
            var media = durate.Count > 0 ? (int)durate.Average() : 0;
            var genereGroup = libri
                .Where(l => !string.IsNullOrEmpty(l.Genere))
                .GroupBy(l => l.Genere)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();
            var genere = genereGroup?.Key ?? "-";
            Cards.Clear();
            Cards.Add(new StatCard { Titolo = "Libri in prestito", Valore = LibriInPrestito.ToString() });
            Cards.Add(new StatCard { Titolo = "Libri disponibili", Valore = libriDisponibili.ToString() });
            Cards.Add(new StatCard { Titolo = "In ritardo", Valore = prestitiInRitardo.ToString() });
            Cards.Add(new StatCard { Titolo = "Autore top", Valore = autore });
            Cards.Add(new StatCard { Titolo = "Socio più attivo", Valore = socioNome });
            Cards.Add(new StatCard { Titolo = "Media giorni", Valore = $"{media} gg" });
            Cards.Add(new StatCard { Titolo = "Genere popolare", Valore = genere });
        }
    }
}