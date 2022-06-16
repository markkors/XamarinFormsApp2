using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using XamarinFormsApp2.Model;

namespace XamarinFormsApp2.ViewModel
{
    public class MainViewModel : System.ComponentModel.INotifyPropertyChanged

    {
        private List<Currency> _Currencies;
        private Currency _selectedCurrency;
        private double _Amount;


        public MainViewModel()
        {
            Currencies = new List<Currency>();
            getCurrencySymbols();
            getCurrencyRates("EUR");
        }


        public void getCurrencySymbols()
        {
            const string baseUrl = "https://openexchangerates.org";
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest("/api/currencies.json", Method.Get);
            request.Timeout = -1;
            RestResponse response = client.Execute(request);
            string data = response.Content.ToString();
            JObject stuff = JObject.Parse(data);
            foreach (JProperty x in (JToken)stuff)
            {
                Currency cTemp = new Currency(x.Value.ToString(),x.Name);
                Currencies.Add(cTemp);
            }

        }


        public void getCurrencyRates(string b)
        {
            const string baseUrl = "https://open.er-api.com";
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(String.Format("v6/latest/{0}",b.ToUpper()), Method.Get);
            request.Timeout = -1;
            RestResponse response = client.Execute(request);
            string data = response.Content.ToString();
            JObject stuff = JObject.Parse(data);
            JToken rates = stuff.Property("rates").Value;

            foreach (JProperty p in rates)
            {
                Currency c  = Currencies.Find(x => x.Code == p.Name);
                if(c != null) c.Rate = (double)p.Value;
                
            }
        }


        public Currency selectedCurrency
        {
            get { 
                return _selectedCurrency;
            }
            set { 
                _selectedCurrency = value;
                OnPropertyChanged();
            }
        }

        public List<Currency> Currencies {
            get { 
                return _Currencies; 
            }
            set { 
                _Currencies = value;
                OnPropertyChanged();
            }
        }

        public double Amount { 
            get {
                return _Amount; 
            }
            set {
                _Amount = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
