using BibliotecaWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Windows;

namespace BibliotecaWPF.ViewModels
{
    public partial class LibroFormViewModel : ObservableObject
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
        private StatoPrestito stato = StatoPrestito.Attivo;

        [ObservableProperty]
        private int durataPrestito;

        [ObservableProperty]
        private int contaLibri;

        public Action<Libro> OnLibroCreato { get; set; }
        public Action<Prestito> OnPrestitoCreato { get; set; }

        [RelayCommand]
        private void Salva()
        {
            //controllo errori
            //Titolo non vuoto
            if (string.IsNullOrWhiteSpace(Titolo))
            {
                MessageBox.Show("Inserire il titolo del libro",
                    "Errore",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            //Autore non vuoto
            if (string.IsNullOrWhiteSpace(Autore))
            {
                MessageBox.Show("Inserire l'autore del libro",
                    "Errore",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            //ISBN numerico 
            if (!long.TryParse(isbn, out _))
            {
                MessageBox.Show("L'ISBN deve contenere solo numeri",
                    "Errore",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            //controllo anno
            if (annoPubblicazione > DateTime.Now.Year)
            {
                MessageBox.Show("L' anno non può essere maggiore di quello corrente",
                "Errore",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            // genere non vuoto 
            if (string.IsNullOrWhiteSpace(Genere))
            {
                MessageBox.Show("Inserire il genere del libro",
                    "Errore",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            //salva il libro nel catalogo 
            var libro = new Libro
            {
                Titolo = Titolo,
                Autore = Autore,
                Isbn = Isbn,
                AnnoPubblicazione = AnnoPubblicazione,
                Genere = Genere,
                StatoPrestito = Stato,
                DurataPrestito = DurataPrestito
            };

            var prestito = new Prestito
            {
                LibroISBN = Isbn,
                SocioID = string.Empty,
            };
            if (OnLibroCreato != null)
            {
                OnLibroCreato.Invoke(libro);
            }
            if (OnPrestitoCreato != null)
            {
                OnPrestitoCreato.Invoke(prestito);
            }
            contaLibri++;
            Svuota();
        }
        // rimuovi libro

        [RelayCommand(CanExecute = nameof(CanRimuovi))]
        private void RimuoviLibro()
        {
            contaLibri--;
        }
        private bool CanRimuovi()
        {
            return contaLibri > 0;
        }
        partial void OnContaLibriChanged(int value)
        {
            RimuoviLibroCommand?.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private void Svuota()
        {
            Titolo = string.Empty;
            Autore = string.Empty;
            Isbn = string.Empty;
            AnnoPubblicazione = 0;
            Genere = string.Empty;
            Stato = StatoPrestito.Attivo;
            DurataPrestito = 0;
        }
    }
}
