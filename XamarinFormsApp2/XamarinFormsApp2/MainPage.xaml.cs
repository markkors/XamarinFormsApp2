using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp2.ViewModel;


namespace XamarinFormsApp2
{
    public partial class MainPage : ContentPage
    {

        MainViewModel vm = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = vm; 
            
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
           //vm.apiTest();
           if(vm.selectedCurrency != null && vm.selectedCurrency.Rate > 0)
            {
                DisplayAlert(
                    String.Format("Koers {0}",vm.selectedCurrency.Name), 
                    String.Format("{0} Euro kost {1} {2}",vm.Amount.ToString(),(vm.selectedCurrency.Rate*vm.Amount).ToString() ,vm.selectedCurrency.Name), 
                    "OK");
            }
           
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!double.TryParse(e.NewTextValue, out double value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}
