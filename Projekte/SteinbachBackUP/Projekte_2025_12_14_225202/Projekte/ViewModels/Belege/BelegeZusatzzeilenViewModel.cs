using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace ProjektDB.ViewModels
{
    public class BelegeZusatzzeilenViewModel: PropertyChangedBase
    {
        //Comment for Git
        private string _Pos;
        public string Pos
        {
            get { return _Pos; }
            set
            {
                if (value != _Pos)
                {
                    _Pos = value;
                    NotifyOfPropertyChange(() => Pos);
                   
                }
            }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    NotifyOfPropertyChange(() => Text);
                    
                }
            }
        }

        private string _Typ;
        public string Typ
        {
            get { return _Typ; }
            set
            {
                if (value != _Typ)
                {
                    _Typ = value;
                    NotifyOfPropertyChange(() => Typ);
                   
                }
            }
        }

        private string _Prozent;
        public string Prozent
        {
            get { return _Prozent; }
            set
            {
                if (value != _Prozent)
                {
                     _Prozent = value;
                     NotifyOfPropertyChange(() => Prozent);
                }
            
            }
        }
     
        private string _Wert;
        public string Wert
        {
            get { return _Wert; }
            set
            {
                if (value != _Wert)
                {
                    _Wert = value;
                    NotifyOfPropertyChange(() => Wert);
                   
                }
            }
        }

        private string _Zeilenwert;
        public string Zeilenwert
        {
            get { return _Zeilenwert; }
            set
            {
                if (value != _Zeilenwert)
                {
                    _Zeilenwert = value;
                    NotifyOfPropertyChange(() => Zeilenwert);
                   
                }
            }
        }

        private string _Zeilensaldo;
        public string Zeilensaldo
        {
            get { return _Zeilensaldo; }
            set
            {
                if (value != _Zeilensaldo)
                {
                    _Zeilensaldo = value;
                    NotifyOfPropertyChange(() => Zeilensaldo);
                    
                }
            }
        }

 
    }
}
