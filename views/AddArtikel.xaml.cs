using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;

namespace ProjektDB.views
{
    /// <summary>
    /// Interaction logic for AddArtikel.xaml
    /// </summary>
    public partial class AddArtikel : Window
    {

        public event Action<DTO.dtoAddArtikel> LagerlisteUpdated;
        
        public AddArtikel(Window wOwner)
        {
            InitializeComponent();

            var vModel = new ViewModels.AddArtikelViewModel();
            vModel.ListeUpdated += new Action<DTO.dtoAddArtikel>(vModel_ListeUpdated);
            Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
            this.Owner = wOwner;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        }


        public AddArtikel()
        {
            InitializeComponent();

            var vModel = new ViewModels.AddArtikelViewModel();
            vModel.ListeUpdated += new Action<DTO.dtoAddArtikel>(vModel_ListeUpdated);
            Caliburn.Micro.ViewModelBinder.Bind(vModel, this, null);
           
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }





        void vModel_ListeUpdated(DTO.dtoAddArtikel obj)
        {
            OnLagerListeUpdated(obj);
            txtMenge.Focus();
            txtMenge.Text = string.Empty;
        }

     
        void OnLagerListeUpdated(DTO.dtoAddArtikel liste)
        {
            if (LagerlisteUpdated != null)
            {
                LagerlisteUpdated(liste);
            }

        }
    }
}
