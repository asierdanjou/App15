namespace App15.ViewModels
{
    using Models; 

    public class LandViewModel
    {
        //private LandItemViewModel landItemViewModel; // Se puede cambiar por la clase Land porque heredan de la misma
        //private Land land;    // No marquemos el land como un atributo privado 
        #region Propperties
        public Land Land
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        //public LandViewModel(LandItemViewModel landItemViewModel)
        public LandViewModel(Land land)
        {
            //this.landItemViewModel = landItemViewModel;
            this.Land = land;   // Le pasa la lista por el constructor

        }
        #endregion
    }
}
