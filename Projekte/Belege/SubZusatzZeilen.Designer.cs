namespace ProjektDB.Belege
{
    /// <summary>
    /// Summary description for SubZusatzZeilen.
    /// </summary>
    partial class SubZusatzZeilen
    {
        private GrapeCity.ActiveReports.SectionReportModel.PageHeader pageHeader;
        private GrapeCity.ActiveReports.SectionReportModel.Detail detail;
        private GrapeCity.ActiveReports.SectionReportModel.PageFooter pageFooter;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SubZusatzZeilen));
            this.pageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.txtText = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.txtWert = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.txtZeilenwert = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.txtProzent = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.pageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
            this.txtFill = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZeilenwert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProzent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0.03125F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.txtText,
            this.txtWert,
            this.txtZeilenwert,
            this.txtProzent,
            this.txtFill});
            this.detail.Height = 0.2F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtText
            // 
            this.txtText.DataField = "Text";
            this.txtText.Height = 0.2F;
            this.txtText.Left = 2.876F;
            this.txtText.Name = "txtText";
            this.txtText.Style = "font-family: Arial; font-size: 11.25pt; ddo-char-set: 0";
            this.txtText.Text = "Text";
            this.txtText.Top = 0F;
            this.txtText.Width = 1.78F;
            // 
            // txtWert
            // 
            this.txtWert.DataField = "Wert";
            this.txtWert.Height = 0.2F;
            this.txtWert.Left = 5.728F;
            this.txtWert.Name = "txtWert";
            this.txtWert.Style = "font-family: Arial; font-size: 11.25pt; text-align: right; ddo-char-set: 0";
            this.txtWert.Text = "Wert";
            this.txtWert.Top = 0F;
            this.txtWert.Width = 0.9589996F;
            // 
            // txtZeilenwert
            // 
            this.txtZeilenwert.DataField = "Zeilenwert";
            this.txtZeilenwert.Height = 0.2F;
            this.txtZeilenwert.Left = 6.687F;
            this.txtZeilenwert.Name = "txtZeilenwert";
            this.txtZeilenwert.Style = "font-family: Arial; font-size: 11.25pt; text-align: right; ddo-char-set: 0";
            this.txtZeilenwert.Text = "Zeilenwert";
            this.txtZeilenwert.Top = 0F;
            this.txtZeilenwert.Width = 1F;
            // 
            // txtProzent
            // 
            this.txtProzent.DataField = "Prozent";
            this.txtProzent.Height = 0.2F;
            this.txtProzent.Left = 4.656F;
            this.txtProzent.Name = "txtProzent";
            this.txtProzent.Style = "font-size: 11.25pt; text-align: right; ddo-char-set: 0";
            this.txtProzent.Text = "0,0%";
            this.txtProzent.Top = 0F;
            this.txtProzent.Width = 0.6659999F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // txtFill
            // 
            this.txtFill.Height = 0.2F;
            this.txtFill.Left = 5.322F;
            this.txtFill.Name = "txtFill";
            this.txtFill.Top = 0F;
            this.txtFill.Width = 0.677F;
            // 
            // SubZusatzZeilen
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.854167F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZeilenwert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProzent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtText;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtWert;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtZeilenwert;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtProzent;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtFill;
    }
}
