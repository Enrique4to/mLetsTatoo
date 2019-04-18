namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Services;
    using Models;
    using Helpers;
    using Xamarin.Forms;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;

    public class NotasItemViewModel : T_trabajonota

    {
        #region MyRegion

        #endregion
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes 
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public NotasItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands

        #endregion
        #region Methods

        #endregion
    }
}
