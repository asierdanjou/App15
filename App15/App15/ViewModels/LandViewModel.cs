﻿namespace App15.ViewModels
{
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class LandViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Border> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;
        #endregion

        //private LandItemViewModel landItemViewModel; // Se puede cambiar por la clase Land porque heredan de la misma
        //private Land land;    // No marquemos el land como un atributo privado 
        #region Propperties
        public Land Land
        {
            get;
            set;
        }
        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { this.SetValue(ref this.borders, value); }
        }
        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { this.SetValue(ref this.currencies, value); }
        }
        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { this.SetValue(ref this.languages, value); }
        }
        #endregion

        #region Constructors
        //public LandViewModel(LandItemViewModel landItemViewModel)
        public LandViewModel(Land land)
        {
            //this.landItemViewModel = landItemViewModel;
            this.Land = land;   // Le pasa la lista por el constructor
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);
            this.Languages = new ObservableCollection<Language>(this.Land.Languages);
        }
        #endregion

        #region Methods
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Land.Borders)
            {
                var land = MainViewModel.GetInstance().LandsList.
                    Where(l => l.Alpha3Code == border).
                    FirstOrDefault();
                if (land != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name,
                    });
                }
            }
        }
        #endregion
    }
}
