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

        private async void SelectLand() // El metodo de navegacion es asincrono
        {
            // Antes de pintar la page hay que relacionar la viewmodel a la page
            // Para eso invocamos la mainviewmodel con GetInstance que es el patron Singleton.
            MainViewModel.GetInstance().Land = new LandViewModel(this); // La propiedad activa (Land) va a ser igual q a this = Pasar toda la clase actual 
            //await Application.Current.MainPage.Navigation.PushAsync(new LandPage()); // Push a la nueva pagina 
            await Application.Current.MainPage.Navigation.PushAsync(new LandTabbedPage()); // Push a la nueva pagina 
        }
        #endregion
    }
}
