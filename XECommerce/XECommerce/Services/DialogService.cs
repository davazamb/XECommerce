using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XECommerce.Services
{
    public class DialogService
    {
        public async Task ShowMessage(String title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
        } 
    }
}
