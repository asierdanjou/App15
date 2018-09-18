namespace App15.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Services;
    using Models;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        //private ObservableCollection<Land> lands; // La observablecollection se basaba en Land
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;
        //private List<Land> landsList;
        #endregion

        #region Properties
        public ObservableCollection<LandItemViewModel> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }

        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();  // Back a la navegacion por codigo
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();  // Back a la navegacion por codigo
                return;
            }

            //var list = (List<Land>)response.Result; // Variable local y perderiamos su informacion
            //this.landsList = (List<Land>)response.Result;
            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
            //this.Lands = new ObservableCollection<Land>(list); // En el constructor le pasamos la lista de paises
            this.Lands = new ObservableCollection<LandItemViewModel>(
                this.ToLandItemViewModel()); // En el constructor le pasamos la lista de paises
                                             // this.ToLandItemViewModel() --> transformacion de lista. 
                                             // landsList viene de Land.cs
                                             // y aunque la clase LandItemViemModel hereda de la clase Land, ambas listas no son compatibles.
            this.IsRefreshing = false;
        }

        // Convierte una lista Land en una lista LandItemViewModel de forma rapida y no elemento por elemento
        // Si lo hacemos con For(i) elemento por elemento nos va a tardar mucho
        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            //return this.landsList.Select(l => new LandItemViewModel
            return MainViewModel.GetInstance().LandsList.Select(l => new LandItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) || 
                        l.Capital.ToLower().Contains(this.Filter.ToLower()) 
                    )
                );
            }
        }
        #endregion

        // Hemos cambiado la lista this.landsList por this.ToLandItemViewModel() por criterios de arquitectura
        // ya que Land.cs es un modelo y no puede contener propiedades variables. Si en cambio LandItemViewModel.cs.

        // Antes era una lista de tipo Land y ahora es una de tipo LandItemViewModel
        // Para evitar ponerle el comando al Land que es un modelo y por criterios de programacion hay que evitarlo.

    }
}
