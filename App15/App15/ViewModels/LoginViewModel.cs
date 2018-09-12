namespace App15.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Views;

    // Clase con interface INotifyPropertyChanged
    // Lo ponemos a heredar de la BaseViewModel (antes heredaba directamente de INotifyPropertyChanged)
    public class LoginViewModel : BaseViewModel
    {
        #region Events
        //public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        // Atributos privados de la clase
        // En este ejemplo creamos las que queremos refrecar en tiempo de ejecucion.
        #region Attributes    
        private string email;
        private string password;    // Atributo privado en minuscula
        private bool isRunning;
        private bool isEnabled;
        #endregion

        // Propiedades publicas de la clase (bindadas a los elementos de la LoginPage)
        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            /*
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(this.Password))
                    );
                }
            }
            */
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        // Constructor de elementos. Por defecto son False.
        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "asierdanjou@gmail.com";
            this.Password = "1234";
        }
        #endregion

        // Commandos de la clase. Acciones bindadas.
        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an password.",
                    "Accept");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;
            if (this.Email != "asierdanjou@gmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect.",
                    "Accept");
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            /*
            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                "CORRECT",
                "Accept");
                */
            this.Email = string.Empty;
            this.Password = string.Empty;

            MainViewModel.GetInstance().Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }
        #endregion
    }
}
