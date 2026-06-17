using BibliotecaWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace BibliotecaWPF.Services
{
    public partial class DataService
    {
        private static readonly string Cartella = Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..", "..", "..", "Services"));

        private static readonly string LibriFile = Path.Combine(Cartella, "libri.json");
        private static readonly string SociFile = Path.Combine(Cartella, "soci.json");
        private static readonly string PrestitiFile = Path.Combine(Cartella, "prestiti.json");

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public static void Salva(
            ObservableCollection<Libro> libri,
            ObservableCollection<Socio> soci,
            ObservableCollection<Prestito> prestiti)
        {
            try
            {
                Directory.CreateDirectory(Cartella);
                File.WriteAllText(LibriFile, JsonSerializer.Serialize(libri, Options));
                File.WriteAllText(SociFile, JsonSerializer.Serialize(soci, Options));
                File.WriteAllText(PrestitiFile, JsonSerializer.Serialize(prestiti, Options));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore salvataggio: " + ex.Message);
            }
        }

        public static void Carica(
            ObservableCollection<Libro> libri,
            ObservableCollection<Socio> soci,
            ObservableCollection<Prestito> prestiti)
        {
            try
            {
                Directory.CreateDirectory(Cartella);

                libri.Clear();
                soci.Clear();
                prestiti.Clear();

                if (File.Exists(LibriFile))
                {
                    var listaLibri = JsonSerializer.Deserialize<List<Libro>>(
                        File.ReadAllText(LibriFile), Options);
                    if (listaLibri != null)
                        foreach (var l in listaLibri)
                            libri.Add(l);
                }

                if (File.Exists(SociFile))
                {
                    var listaSoci = JsonSerializer.Deserialize<List<Socio>>(
                        File.ReadAllText(SociFile), Options);
                    if (listaSoci != null)
                        foreach (var s in listaSoci)
                            soci.Add(s);
                }

                if (File.Exists(PrestitiFile))
                {
                    var listaPrestiti = JsonSerializer.Deserialize<List<Prestito>>(
                        File.ReadAllText(PrestitiFile), Options);
                    if (listaPrestiti != null)
                        foreach (var p in listaPrestiti)
                            prestiti.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore caricamento: " + ex.Message);
            }
        }

        public static void RisolviRiferimenti(
            ObservableCollection<Libro> libri,
            ObservableCollection<Socio> soci,
            ObservableCollection<Prestito> prestiti)
        {
            foreach (var p in prestiti)
            {
                p.Libro = libri.FirstOrDefault(l => l.Isbn == p.LibroISBN);
                p.Socio = soci.FirstOrDefault(s => s.Id == p.SocioID);
            }
        }
        public static void PulisciDati(
                ObservableCollection<Libro> libri,
                ObservableCollection<Socio> soci,
                ObservableCollection<Prestito> prestiti)
        {
            try
            {
                // svuota le collezioni in memoria
                libri.Clear();
                soci.Clear();
                prestiti.Clear();

                // sovrascrive i file con liste vuote
                File.WriteAllText(LibriFile, "[]");
                File.WriteAllText(SociFile, "[]");
                File.WriteAllText(PrestitiFile, "[]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore pulizia dati: " + ex.Message);
            }
        }
    }
}