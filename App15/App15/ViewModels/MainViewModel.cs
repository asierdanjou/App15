using App15.Models;
using System.Collections.Generic;

namespace App15.ViewModels
{
    class MainViewModel
    {
        #region Properties
        public List<Land> LandsList
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; } // No la instanciamos en el constructor para no carga desde el principio en memoria
        public LandViewModel Land { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
