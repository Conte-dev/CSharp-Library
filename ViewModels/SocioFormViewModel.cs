using BibliotecaWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;

namespace BibliotecaWPF.ViewModels
{
    public partial class SocioFormViewModel : ObservableObject
    {
        [ObservableProperty] private string nome;
        [ObservableProperty] private string cognome;
        [ObservableProperty] private string id;
        [ObservableProperty] private string email;

        public Action<Socio> OnSocioCreato { get; set; }

        [RelayCommand]
        private void Salva()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                MessageBox.Show("Inserire il nome del socio");
                return;
            }

            if (string.IsNullOrWhiteSpace(Cognome))
            {
                MessageBox.Show("Inserire il cognome del socio");
                return;
            }

            if (string.IsNullOrWhiteSpace(Id))
            {
                MessageBox.Show("Inserire l'ID del socio");
                return;
            }

            if (!long.TryParse(Id, out _))
            {
                MessageBox.Show("L'ID deve contenere solo numeri");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            {
                MessageBox.Show("Email non valida");
                return;
            }

            var socio = new Socio
            {
                Nome = Nome,
                Cognome = Cognome,
                Id = Id,
                Email = Email
            };

            OnSocioCreato?.Invoke(socio);

            Svuota();
        }

        private void Svuota()
        {
            Nome = string.Empty;
            Cognome = string.Empty;
            Id = string.Empty;
            Email = string.Empty;
        }
    }
}