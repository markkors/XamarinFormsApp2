using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp2.Model
{
    public class Currency
    {

        private string currencyCode;
        private string currencyName;
        private double rate;
        public Currency(string n,string c)
        {
            this.currencyCode = c;
            this.currencyName = n;
        }

        public double Rate { 
            get { return rate; } 
            set { rate = value; }
        }

        public string Code { 
            get { 
                return currencyCode; 
            } 
            set { 
                currencyCode = value;
            }
        }

        public string Name
        {
            get
            {
                return currencyName;
            }
            set
            {
                currencyName = value;
            }
        }

        public string displayName
        {
            get { 
                return string.Format("{0} [{1}]", this.Name, this.currencyCode);
            }
        }
    }
}
