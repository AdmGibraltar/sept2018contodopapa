namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Rep_RPlaneacionCteA.
    /// </summary>
    public partial class RepCumplimientoVISemDeta : Telerik.Reporting.Report
    {
        public RepCumplimientoVISemDeta()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
              this.DataSource = null; // this.sqlDataAdapter1;
        }

        private void RepCumplimientoVISemDeta_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                this.sqlConnection1.ConnectionString = this.ReportParameters["Conexion"].Value.ToString();
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.ReportParameters["Id_Cd"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Anio"].Value = this.ReportParameters["Anio"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Mes"].Value = this.ReportParameters["Mes"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Semanas"].Value = this.ReportParameters["Semanas"].Value;
                //                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Seg"].Value = this.ReportParameters["Id_Seg"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Rik"].Value = this.ReportParameters["Id_Rik"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Ter"].Value = this.ReportParameters["Id_Ter"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cte"].Value = this.ReportParameters["Id_Cte"].Value;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Tipo"].Value = this.ReportParameters["Tipo"].Value;
                

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                report.DataSource = this.sqlDataAdapter1;

                //if (this.ReportParameters["strSem1"].Value == "") 
                //{this.textBox19.Visible = false;}
                //else
                //{ this.textBox19.Visible = true; }

                if (this.ReportParameters["strSem2"].Value == "")
                { 
                    this.textBox21.Visible = false;
                    this.textBox62.Visible = false;
                    this.textBox34.Visible = false;
                    this.textBox51.Visible = false;
                    this.textBox52.Visible = false;
                }
                else
                { 
                    this.textBox21.Visible = true;
                    this.textBox62.Visible = true;
                    this.textBox34.Visible = true;
                    this.textBox51.Visible = true;
                    this.textBox52.Visible = true;
                }

                if (this.ReportParameters["strSem3"].Value == "")
                { 
                    this.textBox23.Visible = false;
                    this.textBox63.Visible = false;
                    this.textBox44.Visible = false;
                    this.textBox53.Visible = false;
                    this.textBox54.Visible = false;
                }
                else
                { 
                    this.textBox23.Visible = true;
                    this.textBox63.Visible = true;
                    this.textBox44.Visible = true;
                    this.textBox53.Visible = true;
                    this.textBox54.Visible = true;
                }

                if (this.ReportParameters["strSem4"].Value == "")
                { 
                    this.textBox26.Visible = false;
                    this.textBox64.Visible = false;
                    this.textBox43.Visible = false;
                    this.textBox55.Visible = false;
                    this.textBox56.Visible = false;
                }
                else
                { 
                    this.textBox26.Visible = true;
                    this.textBox64.Visible = true;
                    this.textBox43.Visible = true;
                    this.textBox55.Visible = true;
                    this.textBox56.Visible = true;
                }

                if (this.ReportParameters["strSem5"].Value == "")
                {
                    this.textBox32.Visible = false;
                    this.textBox65.Visible = false;
                    this.textBox42.Visible = false;
                    this.textBox57.Visible = false;
                    this.textBox58.Visible = false;
                }
                else
                { 
                    this.textBox32.Visible = true;
                    this.textBox65.Visible = true;
                    this.textBox42.Visible = true;
                    this.textBox57.Visible = true;
                    this.textBox58.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}