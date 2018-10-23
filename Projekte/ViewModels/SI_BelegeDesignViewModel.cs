using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjektDB.ViewModels
{
    public class SI_BelegeDesignViewModel : INotifyPropertyChanged
    {
        private bool _isProjektVisible;
        public bool isProjektVisible
        {
            get { return _isProjektVisible; }
            set
            {
                if (value != _isProjektVisible)
                {
                    _isProjektVisible = true;
                    OnPropertyChanged("isProjektVisible");
                    
                }
            }
        }

        //private string _BelegnummerLieferant;
        //public string BelegnummerLieferant
        //{
        //    get { return BelegnummerLieferant; }
        //    set
        //    {
        //        if (value != _BelegnummerLieferant)
        //        {
        //            _BelegnummerLieferant = "SI_123456789";
        //            OnPropertyChanged("BelegnummerLieferant");

        //        }
        //    }
        //}

        public string TestText { get; set; }

        //public int TestInt { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
