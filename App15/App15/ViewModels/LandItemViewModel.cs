namespace App15.ViewModels
{
    using Views;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    // Por el momento es una clase que hereda la otra clase pero no hace nada mas
    // Son replicas. La clase LandItemViewModel es una copia de la clase Land.
    public class LandItemViewModel : Land   // Esta clase va a heredar de la clase Land
    {
        #region Commands
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectLand);
            }
        }

        private async void SelectLand()
        {
            MainViewModel.GetInstance().Land = new LandViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new LandPage());
        }
        #endregion
    }
}
