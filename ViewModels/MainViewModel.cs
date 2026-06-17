using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using BibliotecaWPF.Models;
using CommunityToolkit.Mvvm.Input;
using BibliotecaWPF.Services;

namespace BibliotecaWPF.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        // collezioni
        public ObservableCollection<Libro> Libri { get; set; } = new();
        public ObservableCollection<Socio> Soci { get; set; } = new();
        public ObservableCollection<Prestito> Prestiti { get; set; } = new();

        [ObservableProperty]
        private Libro libroSelezionato;

        [ObservableProperty]
        public double percentualePrestiti;
        // form
        public LibroFormViewModel LibroForm { get; set; }
        public SocioFormViewModel SocioForm { get; set; }
        public DashBoardViewModel DashBoard { get; set; }

        private string _ultimoSocioId;
        private bool _caricamentoInCorso = false;

        public MainViewModel()
        {
            LibroForm = new LibroFormViewModel();
            SocioForm = new SocioFormViewModel();
            DashBoard = new DashBoardViewModel(this);

            LibroForm.OnLibroCreato = AggiungiLibro;
            LibroForm.OnPrestitoCreato = AggiungiPrestito;
            SocioForm.OnSocioCreato = AggiungiSocio;

            _caricamentoInCorso = true;
            DataService.Carica(Libri, Soci, Prestiti);
            DataService.RisolviRiferimenti(Libri, Soci, Prestiti);
            _caricamentoInCorso = false;

            Libri.CollectionChanged += (s, e) => SvuotaLibriCommand.NotifyCanExecuteChanged();
            Libri.CollectionChanged += (s, e) => SalvaTutto();
            Libri.CollectionChanged += (s, e) => DashBoard.AggiornaStatistiche();
            Soci.CollectionChanged += (s, e) => SalvaTutto();
            Prestiti.CollectionChanged += (s, e) => SalvaTutto();
            Prestiti.CollectionChanged += (s, e) => DashBoard.AggiornaStatistiche();
        }

        private void SalvaTutto()
        {
            if (_caricamentoInCorso) return;
            DataService.Salva(Libri, Soci, Prestiti);
        }

        private void AggiungiLibro(Libro libro)
        {
            Libri.Add(libro);
        }
        private void AggiungiSocio(Socio socio)
        {
            _ultimoSocioId = socio.Id;
            Soci.Add(socio);
        }
        private void AggiungiPrestito(Prestito prestito)
        {
            prestito.SocioID = _ultimoSocioId;
            Prestiti.Add(prestito);
        }

        [RelayCommand(CanExecute = nameof(CanRimuovi))]
        private void Rimuovi()
        {
            Libri.Remove(LibroSelezionato);
        }
        private bool CanRimuovi() => LibroSelezionato != null;

        partial void OnLibroSelezionatoChanged(Libro value)
        {
            RimuoviCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanSvuotaLibri))]
        private void SvuotaLibri()
        {
            Libri.Clear();
        }
        private bool CanSvuotaLibri() => Libri.Count > 0;
    }
}