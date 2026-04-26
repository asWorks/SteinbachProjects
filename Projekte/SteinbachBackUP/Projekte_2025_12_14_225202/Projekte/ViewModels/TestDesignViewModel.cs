using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProjektDB.ViewModels 
 
{  
    public class TestDesignViewModel :INotifyPropertyChanged
    {



        public TestDesignViewModel()
        {

            Test = "Neuer Test";
        }

        private string _Test;

        public string Test
        {
            get { return _Test; }
            set 
            {
                _Test = value; 
                OnPropertyChanged("Test");
            }
        }


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
