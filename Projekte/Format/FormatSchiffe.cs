using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ProjektDB.Format
{
    public class FormatSchiffe
    {

        public static void AltePlantEinträgeEingeblendet(views.EditSchiff schiff,bool state)
        {

            schiff.lblPlant1.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;
            schiff.txtPlant1.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;

            schiff.lblPlant2.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;
            schiff.txtPlant2.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;

            schiff.lblPlant3.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;
            schiff.txtPlant3.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;

            schiff.lblPlant4.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;
            schiff.txtPlant4.Visibility = state == true ? Visibility.Visible : Visibility.Collapsed;
        
        
        }
    }
}
