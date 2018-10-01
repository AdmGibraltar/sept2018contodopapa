namespace LibreriaReportes
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    
    using System.Data;
    using CapaEntidad;
    using CapaNegocios;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for ValProyectoImpresion_SubProd.
    /// </summary>
    public partial class ValProyectoImpresion_SubProd : Telerik.Reporting.Report
    {
        public ValProyectoImpresion_SubProd()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            
            //

            this.DataSource = null;
        }

        #region Propiedades

        private List<TipoMoneda> _listaTipoMoneda;
        public List<TipoMoneda> ListaTipoMoneda
        {
            get { return _listaTipoMoneda; }
            set { _listaTipoMoneda = value; }
        }

        public string Id_Emp
        {
            get { return this.ReportParameters["@Id_Emp"].Value.ToString(); }
            set { this.ReportParameters["@Id_Emp"].Value = value; }
        }

        public string Id_Cd
        {
            get { return this.ReportParameters["@Id_Cd"].Value.ToString(); }
            set { this.ReportParameters["@Id_Cd"].Value = value; }
        }

        public string Id_Vap
        {
            get { return this.ReportParameters["@Id_Vap"].Value.ToString(); }
            set { this.ReportParameters["@Id_Vap"].Value = value; }
        }

        public string Conexion
        {
            get { return this.ReportParameters["@Conexion"].Value.ToString(); }
            set { this.ReportParameters["@Conexion"].Value = value; }
        }

        #endregion

        private void ValProyectoImpresion_SubProd_NeedDataSource(object sender, EventArgs e)
        {
            try
            {
                //Transfer the ReportParameter value to the parameter of the select command
                this.sqlConnection1.ConnectionString = this.Conexion;

                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Emp"].Value = this.Id_Emp;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Cd"].Value = this.Id_Cd;
                this.sqlDataAdapter1.SelectCommand.Parameters["@Id_Vap"].Value = this.Id_Vap;

                //Take the Telerik.Reporting.Processing.Report instance and set the adapter as it's DataSource
                Telerik.Reporting.Processing.Report report = (Telerik.Reporting.Processing.Report)sender;
                //report.DataSource = this.sqlDataAdapter1;

                DataTable tabla = new DataTable();
                this.sqlDataAdapter1.Fill(tabla);
                this.PartidasCalcularPrecioLista(ref tabla);

                report.DataSource = tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void detail_ItemDataBinding(object sender, EventArgs e)
        {

        }


        private void PartidasCalcularPrecioLista(ref DataTable tabla)
        {
            foreach (DataRow row in tabla.Rows)
            {
                double precioProductoAceptado = 0;
                int Id_Emp = Convert.ToInt32(row[0]);
                int Id_Cd = Convert.ToInt32(row[1]);
                int Id_Cte = Convert.ToInt32(row[4]);
                int Id_Prd = Convert.ToInt32(row[7]);

                //obtener precio especial del producto 
                //para el cliente actual de la factura
                //desde la CAPTURA de SOLICITUDES DE PRECIOS ESPECIALES
                VentanaPrecioEspecialPro precioEspecialPro = null;
                new CN_PrecioEspecial().PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, this.Conexion
                    , Id_Emp, Id_Cd, Id_Cte, Id_Prd /* , Convert.ToInt32(cmbMoneda.SelectedValue) */);

                if (precioEspecialPro != null && precioEspecialPro.Ape_PreEsp > 0)
                {
                    /*
                     * NOTA: si el precio está en dólares u otro tipo de moneda, 
                     * se hace la conversión al tipo de moneda de la Valuacion de proyectos
                     */
                    if (precioEspecialPro.Id_Mon != 1) // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                    {
                        //Consultar tipo de cambio
                        double tipoCambioFactura = 1; // MONEDA = PESO (1) siempre en captura de valuacion proyectos
                        double tipoCambioPrecioEspecial = 0;
                        foreach (TipoMoneda tm in this.ListaTipoMoneda)
                        {
                            if (tm.Id_Mon == precioEspecialPro.Id_Mon)
                            {
                                tipoCambioPrecioEspecial = tm.Mon_TipCambio;
                            }
                        }
                        precioProductoAceptado = (precioEspecialPro.Ape_PreEsp * tipoCambioPrecioEspecial) / tipoCambioFactura;
                    }
                    else
                    {
                        precioProductoAceptado = precioEspecialPro.Ape_PreEsp;
                    }
                }
                else
                {
                    //Si no hay un precio especial en SOLICITUD DE PRECIOS ESPECIALES
                    //va por el precio del catalogo CLIENTE-PRODUCTO, si no hay toma el precio AAA normal del producto

                    //obtener precio AAA
                    float precioAAA = 0;
                    new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precioAAA, Id_Emp, Id_Cd, Id_Prd, this.Conexion);

                    //obtener precio especial de producto
                    //desde el catálogo CAT_CLIENTEPRODUCTO
                    float precioPublicoCAT_CLIENTEPRODUCTO = 0;
                    ClienteProd clienteProd = new ClienteProd();
                    clienteProd.Id_Emp = Id_Emp;
                    clienteProd.Id_Cd = Id_Cd;
                    clienteProd.Id_Cte = Id_Cte;
                    clienteProd.Id_Prd = Id_Prd;
                    new CN_CatClienteProd().ClienteProductoPrecioPublico_Consultar(ref clienteProd, this.Conexion, ref precioPublicoCAT_CLIENTEPRODUCTO);

                    precioProductoAceptado = precioPublicoCAT_CLIENTEPRODUCTO > 0 ? precioPublicoCAT_CLIENTEPRODUCTO : precioAAA;
                }

                row[12] = precioProductoAceptado;
            }
        }


    }
}