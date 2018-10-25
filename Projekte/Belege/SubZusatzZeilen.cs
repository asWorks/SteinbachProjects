using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjektDB.Belege
{
    /// <summary>
    /// Summary description for SubZusatzZeilen.
    /// </summary>
    public partial class SubZusatzZeilen : GrapeCity.ActiveReports.SectionReport
    {

        public SubZusatzZeilen()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (CommonTools.Tools.ConfigEntry<int>.GetConfigEntry("ZwischensummeBackgroundÄndern", "1", "") == 1)
            {
                if (txtProzent.Text == string.Empty && txtWert.Text==string.Empty)
                {
                    txtText.BackColor = Color.Gainsboro;
                    txtZeilenwert.BackColor = Color.Gainsboro;
                    txtProzent.BackColor = Color.Gainsboro;
                    txtWert.BackColor = Color.Gainsboro;
                    txtFill.BackColor = Color.Gainsboro;

                }
                else
                {
                    txtText.BackColor = Color.Transparent;
                    txtZeilenwert.BackColor = Color.Transparent;
                    txtProzent.BackColor = Color.Transparent;
                    txtWert.BackColor = Color.Transparent;
                    txtFill.BackColor = Color.Transparent;
                }
            }
            
        }
    }
}
