using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaNegocios;
using CapaDatos;
using System.Reflection;
using SIANWEB.CuentasCorporativas;
using CapaModelo_CC.CuentasCoorporativas;

namespace SIANWEB.CuentasCorporativas
{
    public partial class CatClienteMatriz_ACYS : System.Web.UI.Page
    {

     

       
        private List<CatACYS_DirFiscales> listDirFiscales
        {
            get
            {
                return (List<CatACYS_DirFiscales>)Session["listDirFiscales"];
            }
            set
            {
                Session["listDirFiscales"] = value;
            }
        }

        private List<CatAcys_Productos> listProductos
        {
            get
            {
                return (List<CatAcys_Productos>)Session["listProductos"];
            }
            set
            {
                Session["listProductos"] = value;
            }
        }

        private List<CatAcys_Productos> listProductosKilo
        {
            get
            {
                return (List<CatAcys_Productos>)Session["listProductosKilo"];
            }
            set
            {
                Session["listProductosKilo"] = value;
            }
        }


        private List<CatAcys_Productos> listProductosComensal
        {
            get
            {
                return (List<CatAcys_Productos>)Session["listProductosComensal"];
            }
            set
            {
                Session["listProductosComensal"] = value;
            }
        }

        private List<CatAcys_Productos> listProductosHabitacion
        {
            get
            {
                return (List<CatAcys_Productos>)Session["listProductosHabitacion"];
            }
            set
            {
                Session["listProductosHabitacion"] = value;
            }
        }

        private List<CatAcys_Productos> listProductosIguala
        {
            get
            {
                return (List<CatAcys_Productos>)Session["listProductosIguala"];
            }
            set
            {
                Session["listProductosIguala"] = value;
            }
        }

        private List<CatAcys_Productos> listProductosServicios
        {
            get
            {
                return (List<CatAcys_Productos>)Session["listProductosServicios"];
            }
            set
            {
                Session["listProductosServicios"] = value;
            }
        }



        public static List<String> GetListaControles(Control ctrl)
        {

            List<String> listaControles= new List<string>();
                
                foreach (Control c in ctrl.Controls)
                {
                    if (c.HasControls())
                    {
                        listaControles.AddRange ( GetListaControles(c));
                    }
                    else
                        if(c.ID!=null)
                            if(!c.ID.ToUpper().Contains("LABEL"))
                            listaControles.Add(c.ID);
                }
             return listaControles;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
           var listacontrol= GetListaControles(this);

            if (!Page.IsPostBack)
            {
                int id_ClienteMat = Int32.Parse(Request.QueryString["Id"]);
                int id_matriz = Int32.Parse(Request.QueryString["IdMatriz"]);


                CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz();
                Session["cm_Matriz"] = cm_Matriz;
                

                

                CN_CatCNac_ACYS cm_ACYS = new CN_CatCNac_ACYS();
                CatCNac_ACYS matriz = cm_ACYS.ConsultarACYS_Item(id_ClienteMat);

                Session.Add("matrizOr", matriz);

                object objMatriz = matriz;

                AsignacionCampos.AsignaCamposForma(ref objMatriz, "", this);


                if (matriz.CatACYS_Cliente != null)
                {
                    object objMatriz_Cliente = matriz.CatACYS_Cliente;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_Cliente, "", this);

                    object objMatriz_RecPedido = matriz.CatACYS_RecPedido;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_RecPedido, "", this);

                    object objMatriz_CondPago = matriz.CatACYS_CondPago;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_CondPago, "", this);

                    object objMatriz_ServValor = matriz.CatACYS_ServValor;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_ServValor, "", this);

                    object objMatriz_OtrosApoyos = matriz.CatACYS_OtrosApoyos;
                    AsignacionCampos.AsignaCamposForma(ref objMatriz_OtrosApoyos, "", this);

        

                    //Asigna garantias

                    var datosGar = matriz.CatACYS_Productos_DatosGar.Where(x => x.Id_TG == 1).FirstOrDefault();
                    if (datosGar != null)
                    {
                        this.Fac_Kilo.Value = datosGar.FactorGarantia;
                        this.PNeta_Kilo.Value = datosGar.UPrimaNeta;
                    }

                    datosGar = matriz.CatACYS_Productos_DatosGar.Where(x => x.Id_TG == 2).FirstOrDefault();
                    if (datosGar != null)
                    {
                        this.Fac_Comensal.Value = datosGar.FactorGarantia;
                        this.PNeta_Comensal.Value = datosGar.UPrimaNeta;
                    }

                    datosGar = matriz.CatACYS_Productos_DatosGar.Where(x => x.Id_TG == 3).FirstOrDefault();
                    if (datosGar != null)
                    {
                        this.Fac_Habitacion.Value = datosGar.FactorGarantia;
                        this.PNeta_Habitacion.Value = datosGar.UPrimaNeta;
                    }

                    datosGar = matriz.CatACYS_Productos_DatosGar.Where(x => x.Id_TG == 4).FirstOrDefault();
                    if (datosGar != null)
                    {
                        this.Fac_Iguala.Value = datosGar.FactorGarantia;
                        this.PNeta_Iguala.Value = datosGar.UPrimaNeta;
                    }


                    Session["Nuevo"] = false;
                }
                else
                    Session["Nuevo"] = true;


                listDirFiscales = cm_Matriz.ConsutarDirFiscales();
                listProductos = cm_Matriz.ConsultarProductos(0, id_ClienteMat);
                listProductosKilo = cm_Matriz.ConsultarProductos(1, id_ClienteMat);
                listProductosComensal = cm_Matriz.ConsultarProductos(2, id_ClienteMat);
                listProductosHabitacion = cm_Matriz.ConsultarProductos(3, id_ClienteMat);
                listProductosIguala = cm_Matriz.ConsultarProductos(4, id_ClienteMat);
                listProductosServicios = cm_Matriz.ConsultarProductos(5, id_ClienteMat);



                AsignaPermisosCampos();


                Session.Remove("Fechas_1");
                Session.Remove("Fechas_2");
                Session.Remove("Fechas_3");
                Session.Remove("Fechas_4");

               // matriz.CatACYS_Productos_DatosGar_Fechas

                Dictionary<int, DateTime> FechasCorteDict = new Dictionary<int, DateTime>();
                var fechas1 = matriz.CatACYS_Productos_DatosGar_Fechas.Where(x => x.Id_TG == 1).ToList();

                if (fechas1.Count > 0)
                {
                    foreach (CatACYS_Productos_DatosGar_Fechas fechaGar in matriz.CatACYS_Productos_DatosGar_Fechas)
                    {
                        int mes = fechaGar.Mes.Value;
                        DateTime fecha = fechaGar.FechaCorte.Value;
                        FechasCorteDict.Add(mes, fecha);
                    }
                    Session["Fechas_1"] = FechasCorteDict;
                }
                var fechas2 = matriz.CatACYS_Productos_DatosGar_Fechas.Where(x => x.Id_TG == 2).ToList();

                FechasCorteDict = new Dictionary<int, DateTime>();
                if (fechas2.Count > 0)
                {
                    foreach (CatACYS_Productos_DatosGar_Fechas fechaGar in matriz.CatACYS_Productos_DatosGar_Fechas)
                    {
                        int mes = fechaGar.Mes.Value;
                        DateTime fecha = fechaGar.FechaCorte.Value;
                        FechasCorteDict.Add(mes, fecha);
                    }
                    Session["Fechas_2"] = FechasCorteDict;
                }

                FechasCorteDict = new Dictionary<int, DateTime>();
                var fechas3 = matriz.CatACYS_Productos_DatosGar_Fechas.Where(x => x.Id_TG == 3).ToList();

                if (fechas3.Count > 0)
                {
                    foreach (CatACYS_Productos_DatosGar_Fechas fechaGar in matriz.CatACYS_Productos_DatosGar_Fechas)
                    {
                        int mes = fechaGar.Mes.Value;
                        DateTime fecha = fechaGar.FechaCorte.Value;
                        FechasCorteDict.Add(mes, fecha);
                    }
                    Session["Fechas_3"] = FechasCorteDict;
                }

                FechasCorteDict = new Dictionary<int, DateTime>();
                var fechas4 = matriz.CatACYS_Productos_DatosGar_Fechas.Where(x => x.Id_TG == 4).ToList();

                if (fechas4.Count > 0)
                {
                    foreach (CatACYS_Productos_DatosGar_Fechas fechaGar in matriz.CatACYS_Productos_DatosGar_Fechas)
                    {
                        int mes = fechaGar.Mes.Value;
                        DateTime fecha = fechaGar.FechaCorte.Value;
                        FechasCorteDict.Add(mes, fecha);
                    }
                    Session["Fechas_4"] = FechasCorteDict;
                }



                //cmbDireccionesFiscales.DataSource = cm_Matriz.ComboDireccionesFiscales(id_matriz);
                //cmbDireccionesFiscales.DataBind();

            }




             divGarantias.Visible = chkGarantia.Checked;
            this.divServicios.Visible = chkServicios.Checked;

            AsignacionCampos.DesactivarControles(this, "");

            RadTabStrip1.Enabled = true;
            foreach( RadTab tab in RadTabStrip1.Tabs)
                tab.Enabled = true;


           

        }


        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;

            CN_CatClienteMatriz cm_Matriz = (CN_CatClienteMatriz)Session["cm_Matriz"];

            if (btn.CommandName == "save")
            {
                int idMatriz = Int32.Parse(Request.QueryString["Id"]);
                
                
                CatCNac_ACYS matriz = new CatCNac_ACYS();
                matriz.CatACYS_Cliente = new CatACYS_Cliente();
                matriz.CatACYS_RecPedido = new CatACYS_RecPedido();
                matriz.CatACYS_CondPago = new CatACYS_CondPago();
                matriz.CatACYS_ServValor = new CatACYS_ServValor();
                matriz.CatACYS_OtrosApoyos = new CatACYS_OtrosApoyos();
             
                matriz.CatACYS_Productos_DatosGar = new List<CatACYS_Productos_DatosGar>();
              
                matriz.CatACYS_Productos_DatosGar_Fechas = new List<CatACYS_Productos_DatosGar_Fechas>();
                


                List<CatAcys_Productos> listProdFinal = new List<CatAcys_Productos>() ;
                listProdFinal.AddRange(listProductos);
                listProdFinal.AddRange(listProductosKilo);
                listProdFinal.AddRange(listProductosComensal);
                listProdFinal.AddRange(listProductosHabitacion);
                listProdFinal.AddRange(listProductosIguala);

                matriz.CatAcys_Productos = listProdFinal;

              //  CatClienteMatriz matrizOr = (CatClienteMatriz)Session["matrizOr"];
              //  matriz.Nombre = matrizOr.Nombre;
              //  matriz.Estatus = matrizOr.Estatus;
              //  matriz.FechaInicio = matrizOr.FechaInicio;
              //  matriz.FechaFin = matrizOr.FechaFin;
              ////  matriz.Credito = matrizOr.Credito;

                matriz.Id = idMatriz;
                matriz.CatACYS_Cliente.Id = idMatriz;
                matriz.CatACYS_RecPedido.Id = idMatriz;
                matriz.CatACYS_CondPago.Id = idMatriz;
                matriz.CatACYS_ServValor.Id = idMatriz;
                matriz.CatACYS_OtrosApoyos.Id = idMatriz;
               
                
                

                //matriz.Estatus = true;
                

                //LLena campos a partir del formulario
                object objMatriz = matriz;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "",this);

                object objMatrizS = matriz.CatACYS_Cliente;
                AsignacionCampos.AsignaCamposEntidad(ref objMatrizS, "", this);

                object objMatriz_RecPedidoS = matriz.CatACYS_RecPedido;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz_RecPedidoS, "", this);

                object objMatriz_CondPago = matriz.CatACYS_CondPago;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz_CondPago, "", this);

                object objMatriz_ServValor = matriz.CatACYS_ServValor;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz_ServValor, "", this);

                object objMatriz_OtrosApoyos = matriz.CatACYS_OtrosApoyos;
                AsignacionCampos.AsignaCamposEntidad(ref objMatriz_OtrosApoyos, "", this);



                CatACYS_Productos_DatosGar datosGar = new CatACYS_Productos_DatosGar();

                datosGar.Id_ACYS = idMatriz;
                datosGar.Id_TG = 1;
                datosGar.UPrimaNeta = this.PNeta_Kilo.Value;
                datosGar.FactorGarantia = this.Fac_Kilo.Value;
                matriz.CatACYS_Productos_DatosGar.Add(datosGar);

                datosGar = new CatACYS_Productos_DatosGar();

                datosGar.Id_ACYS = idMatriz;
                datosGar.Id_TG = 2;
                datosGar.UPrimaNeta = this.PNeta_Comensal.Value;
                datosGar.FactorGarantia = this.Fac_Comensal.Value;
                matriz.CatACYS_Productos_DatosGar.Add(datosGar);

                datosGar = new CatACYS_Productos_DatosGar();

                datosGar.Id_ACYS = idMatriz;
                datosGar.Id_TG = 3;
                datosGar.UPrimaNeta = this.PNeta_Habitacion.Value;
                datosGar.FactorGarantia = this.Fac_Habitacion.Value;
                matriz.CatACYS_Productos_DatosGar.Add(datosGar);

                datosGar = new CatACYS_Productos_DatosGar();

                datosGar.Id_ACYS = idMatriz;
                datosGar.Id_TG = 4;
                datosGar.UPrimaNeta = this.PNeta_Iguala.Value;
                datosGar.FactorGarantia = this.Fac_Iguala.Value;
                matriz.CatACYS_Productos_DatosGar.Add(datosGar);

               // foreach (CatACYS_DirFiscales dir in listDirFiscales) matriz.CatACYS_DirFiscales.Add(dir);
                if (Session["Fechas_1"] != null)
                { 
                    var FechasCorte_1 = (Dictionary<int, DateTime>)Session["Fechas_1"];
                    foreach (KeyValuePair<int, DateTime> entry in FechasCorte_1)
                    {
                        CatACYS_Productos_DatosGar_Fechas dfechas = new CatACYS_Productos_DatosGar_Fechas();
                        dfechas.Id_ACYS = idMatriz;
                        dfechas.Id_TG = 1;
                        dfechas.Mes = entry.Key;
                        dfechas.FechaCorte = entry.Value;

                        matriz.CatACYS_Productos_DatosGar_Fechas.Add(dfechas);
                    }
                }
                if (Session["Fechas_2"] != null)
                {
                    var FechasCorte_1 = (Dictionary<int, DateTime>)Session["Fechas_2"];
                    foreach (KeyValuePair<int, DateTime> entry in FechasCorte_1)
                    {
                        CatACYS_Productos_DatosGar_Fechas dfechas = new CatACYS_Productos_DatosGar_Fechas();
                        dfechas.Id_ACYS = idMatriz;
                        dfechas.Id_TG = 2;
                        dfechas.Mes = entry.Key;
                        dfechas.FechaCorte = entry.Value;
                        matriz.CatACYS_Productos_DatosGar_Fechas.Add(dfechas);
                    }
                }
                if (Session["Fechas_3"] != null)
                {
                    var FechasCorte_1 = (Dictionary<int, DateTime>)Session["Fechas_3"];
                    foreach (KeyValuePair<int, DateTime> entry in FechasCorte_1)
                    {
                        CatACYS_Productos_DatosGar_Fechas dfechas = new CatACYS_Productos_DatosGar_Fechas();
                        dfechas.Id_ACYS = idMatriz;
                        dfechas.Id_TG = 3;
                        dfechas.Mes = entry.Key;
                        dfechas.FechaCorte = entry.Value;
                        matriz.CatACYS_Productos_DatosGar_Fechas.Add(dfechas);
                    }
                }
                if (Session["Fechas_4"] != null)
                {
                    var FechasCorte_1 = (Dictionary<int, DateTime>)Session["Fechas_4"];
                    foreach (KeyValuePair<int, DateTime> entry in FechasCorte_1)
                    {
                        CatACYS_Productos_DatosGar_Fechas dfechas = new CatACYS_Productos_DatosGar_Fechas();
                        dfechas.Id_ACYS = idMatriz;
                        dfechas.Id_TG = 4;
                        dfechas.Mes = entry.Key;
                        dfechas.FechaCorte = entry.Value;
                        matriz.CatACYS_Productos_DatosGar_Fechas.Add(dfechas);
                    }
                }



                //cm_Matriz.GuardarACYS(matriz, (Boolean)Session["Nuevo"]);
                RAM1.ResponseScripts.Add("CloseAlert('Los datos se guardaron correctamente');");

            }
        }

        protected void rgDirFiscales_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                   
                }
                this.rgDirFiscales.DataSource = listDirFiscales;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgDirFiscales_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgDirFiscales.EditItems.Count > 0)
                        {
                            //Alerta("Ya está editando un registro.");
                            //e.Canceled = true;
                        }
                        break;
                    case "PerformInsert":
                        PerformInsertDirFiscal(e);
                        break;
                    case "Update":
                        UpdateDirFiscal(e);
                        break;
                    case "Delete":
                        DeleteDirFiscal(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void PerformInsertDirFiscal(GridCommandEventArgs e)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);
            GridItem gi = e.Item;
            List<CatACYS_DirFiscales> dirFiscalesIns = this.listDirFiscales;
            var dirFiscal = new CatACYS_DirFiscales();
            dirFiscal.Id_ClienteMatriz = idMatriz;

            object objMatriz = dirFiscal;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi,this);

            dirFiscalesIns.Add(dirFiscal);

        }

        private void UpdateDirFiscal(GridCommandEventArgs e)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);

            GridItem gi = e.Item;
            int idDirFiscal = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id").ToString());

            List<CatACYS_DirFiscales> dirFiscalesIns = this.listDirFiscales;
            var dirFiscal = dirFiscalesIns.Where(x => x.Id == idDirFiscal).FirstOrDefault();

            dirFiscal.Id_ClienteMatriz = idMatriz;

            object objMatriz = dirFiscal;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi, this);


        }

        private void DeleteDirFiscal(GridCommandEventArgs e)
        {
            GridItem gi = e.Item;
            int idDirFiscal = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id").ToString());

            List<CatACYS_DirFiscales> dirFiscalesIns = this.listDirFiscales;
            var dirFiscal = dirFiscalesIns.Where(x => x.Id == idDirFiscal).FirstOrDefault();
            dirFiscalesIns.Remove(dirFiscal);
        }

        protected void RbModalidad_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected double CalculaSubtotal(double x, double y)
        {
            return x * y;
        }

        protected void rdFechaInicioDocumento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

        }
        protected void rdFechaFinDocumento_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

        }
        protected void BtnAutorizar_Click(object sender, EventArgs e)
        {

        }
        protected void BtnRechazar_Click(object sender, EventArgs e)
        {

        }
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {

        }
        protected void cmbTer_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {

        }
       
        protected void cmbRepresentante_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void cmbDireccionesFiscales_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
             int id_matriz = Int32.Parse(Request.QueryString["IdMatriz"]);        


           // CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz(model);
           //var datosFiscales= cm_Matriz.ComboDireccionesFiscales(id_matriz).Where(x=>x.Id==Int32.Parse(e.Value)).FirstOrDefault();

           //this.txtNombre_Cte.Text = datosFiscales.ClienteDirFis;
           //this.txtMunicipio.Text = datosFiscales.MunicipioDirFis;
           //this.txtEstado.Text = datosFiscales.EstadoDirFis;
           //this.txtCP.Text = datosFiscales.CPDirFis;
           //this.txtRFC.Text = datosFiscales.RFCDirFis;
           //this.txtDireccion.Text = datosFiscales.DireccionDirFis;
           //this.txtColonia.Text = datosFiscales.ColoniaDirFis;
        }

        


        protected void rgAcuerdos_PreRender(object sender, EventArgs e)
        {

        }
        // Acuerdos Kilo

        protected void rgAcuerdos_Kilo_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":

                    break;
                case "PerformInsert":
                    PerformInsertAcys(e, 1);

                    break;
                case "Update":
                    UpdateAcys(e, 1);
                    break;

                case "Delete":
                    DeleteAcys(e, 1);
                    break;

            }
        }

        protected void rgAcuerdos_Kilo_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Kilo_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
                this.rgAcuerdos_Kilo.DataSource = listProductosKilo;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Kilo_ItemDataBound(object sender, GridItemEventArgs e)
        {
          
        }

        protected void rgAcuerdos_Kilo_PreRender(object sender, EventArgs e)
        {

        }

        protected void rgAcuerdos_Comensal_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":

                    break;
                case "PerformInsert":
                    PerformInsertAcys(e, 2);

                    break;
                case "Update":
                    UpdateAcys(e, 2);
                    break;

                case "Delete":
                    DeleteAcys(e, 2);
                    break;

            }
        }

        protected void rgAcuerdos_Comensal_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Comensal_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
                this.rgAcuerdos_Comensal.DataSource = listProductosComensal;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Comensal_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }

        protected void rgAcuerdos_Comensal_PreRender(object sender, EventArgs e)
        {

        }

        protected void rgAcuerdos_Habitacion_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":

                    break;
                case "PerformInsert":
                    PerformInsertAcys(e, 3);

                    break;
                case "Update":
                    UpdateAcys(e, 3);
                    break;

                case "Delete":
                    DeleteAcys(e, 3);
                    break;

            }
        }

        protected void rgAcuerdos_Habitacion_ItemCreated(object sender, GridItemEventArgs e)
        {
 
        }

        protected void rgAcuerdos_Habitacion_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
 
                 this.rgAcuerdos_Habitacion.DataSource = listProductosHabitacion;


            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Habitacion_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Habitacion_PreRender(object sender, EventArgs e)
        {

        }

        protected void rgAcuerdos_Iguala_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":

                    break;
                case "PerformInsert":
                    PerformInsertAcys(e, 4);

                    break;
                case "Update":
                    UpdateAcys(e, 4);
                    break;

                case "Delete":
                    DeleteAcys(e, 4);
                    break;

            }
        }

        protected void rgAcuerdos_Iguala_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Iguala_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
                this.rgAcuerdos_Iguala.DataSource = listProductosIguala;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Iguala_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Iguala_PreRender(object sender, EventArgs e)
        {

        }


        protected void rgAcuerdos_Servicios_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":

                    break;
                case "PerformInsert":
                    PerformInsertAcys(e, 4);

                    break;
                case "Update":
                    UpdateAcys(e, 4);
                    break;

                case "Delete":
                    DeleteAcys(e, 4);
                    break;

            }
        }

        protected void rgAcuerdos_Servicios_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Servicios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
                this.rgAcuerdos_Servicios.DataSource = listProductosIguala;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_Servicios_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgAcuerdos_Servicios_PreRender(object sender, EventArgs e)
        {

        }






        protected void RadDatePicker2_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
        }

        protected void ChkServAsesoria_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AsesoriaListado.Visible = chkServAsesoria.Checked;

            }
            catch (Exception ex)
            {
               
            }
        }

        protected void ChkServTecnicoRelleno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EquipoRellenoListado.Visible = chkServTecnicoRelleno.Checked;
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void ChkServMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MantenimientoPreventivoListado.Visible = chkServMantenimiento.Checked;


            }
            catch (Exception ex)
            {
                
            }
        }

        protected void imgKilo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(1);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void imgComensal_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(2);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void imgHabitacion_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(3);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void imgIguala_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("AbrirCalendario(4);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        protected void rgAcuerdos_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":

                    break;
                case "PerformInsert":
                    PerformInsertAcys(e,0);

                    break;
                case "Update":
                    UpdateAcys(e,0);
                    break;

                case "Delete":
                    DeleteAcys(e,0);
                    break;

            }
        }

        private void PerformInsertAcys(GridCommandEventArgs e, int Id_TG)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);
            GridItem gi = e.Item;
            List<CatAcys_Productos> productos = null;

            switch (Id_TG)
            {
                case 0: productos = this.listProductos; break;
                case 1: productos = this.listProductosKilo; break;
                case 2: productos = this.listProductosComensal; break;
                case 3: productos = this.listProductosHabitacion; break;
                case 4: productos = this.listProductosIguala; break;
            }


            var prod = new CatAcys_Productos();

            object objMatriz = prod;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi, this);

            prod.Id_ACYS = idMatriz;
            prod.Id_TG = Id_TG;
            productos.Add(prod);


        }

        private void UpdateAcys(GridCommandEventArgs e, int Id_TG)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);

            GridItem gi = e.Item;
            int id = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id_Prd").ToString());

            List<CatAcys_Productos> productos = null;
            switch (Id_TG)
            {
                case 0: productos = this.listProductos; break;
                case 1: productos = this.listProductosKilo; break;
                case 2: productos = this.listProductosComensal; break;
                case 3: productos = this.listProductosHabitacion; break;
                case 4: productos = this.listProductosIguala; break;
            }


            var prod = productos.Where(x => x.Id_ACYS == idMatriz && x.Id_Prd == id && x.Id_TG == Id_TG).FirstOrDefault();

            prod.Id_ACYS = idMatriz;
            prod.Id_TG = Id_TG;

            object objMatriz = prod;
            AsignacionCampos.AsignaCamposEntidad(ref objMatriz, "", gi,this);


        }

        private void DeleteAcys(GridCommandEventArgs e, int Id_TG)
        {
            int idMatriz = Int32.Parse(Request.QueryString["Id"]);
            GridItem gi = e.Item;
            int id = Int32.Parse(((Telerik.Web.UI.GridDataItem)(e.Item)).GetDataKeyValue("Id_Prd").ToString());

            List<CatAcys_Productos> productos = null;
            switch (Id_TG)
            {
                case 0: productos = this.listProductos; break;
                case 1: productos = this.listProductosKilo; break;
                case 2: productos = this.listProductosComensal; break;
                case 3: productos = this.listProductosHabitacion; break;
                case 4: productos = this.listProductosIguala; break;
            }


            var prod = productos.Where(x => x.Id_ACYS == idMatriz && x.Id_Prd == id && x.Id_TG == Id_TG).FirstOrDefault();
            productos.Remove(prod);
        }








        protected void rgAcuerdos_ItemCreated(object sender, GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.EditItem && e.Item.ItemIndex!=-1)
            {
                ((RadNumericTextBox)((Telerik.Web.UI.GridDataItem)(e.Item)).FindControl("txtId_Prd")).Enabled = false;
            }

        }

        protected void rgAcuerdos_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                }
                this.rgAcuerdos.DataSource = listProductos;
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgAcuerdos_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }


        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {
            //RadNumericTextBox txtProd = sender as RadNumericTextBox;
            //CN_CatClienteMatriz cm_Matriz = (CN_CatClienteMatriz)Session["cm_Matriz"];

            //var prd=cm_Matriz.ConsultaProductoInfo(Convert.ToInt32(txtProd.Value));

            //(txtProd.Parent.FindControl("txtDescripcion") as RadTextBox).Text = prd.Prd_Descripcion;
            //if(txtProd.Parent.FindControl("txtUnidad")!=null) (txtProd.Parent.FindControl("txtUnidad") as Label).Text = prd.Prd_UniNs;
            //if (txtProd.Parent.FindControl("txtPresentacion") != null) (txtProd.Parent.FindControl("txtPresentacion") as Label).Text = prd.Prd_Presentacion;

            //Boolean EsGarantia = false;

            //if (!EsGarantia)
            //{
            //       (txtProd.Parent.FindControl("dblPrecio") as RadNumericTextBox).Focus();
            //}
            //else
            //{
            //    (txtProd.Parent.FindControl("dblPrecio") as RadNumericTextBox).DbValue = 0.00;
            //    (txtProd.Parent.FindControl("txtCantidad") as RadNumericTextBox).Focus();
            //}
            // ConsultaProductoInfo
        }


        public void AsignaPermisosCampos()
        {
            //CN_CatClienteMatriz cm_Matriz = (CN_CatClienteMatriz)Session["cm_Matriz"];

            //var listaCampos=cm_Matriz.ConsultaPermisosCampos();

            //foreach (CatCNac_PermisosCamposACYS campo in listaCampos)
            //{
            //    if (!String.IsNullOrEmpty(campo.Campo))
            //    {
            //        WebControl ctrl = (WebControl)AsignacionCampos.BuscarControl(this, campo.Campo);
            //        if (ctrl != null)
            //        {
            //            if (campo.ACYS_CENTRAL != "1")
            //            {
            //                ctrl.Enabled = false;
            //                ctrl.BackColor = System.Drawing.ColorTranslator.FromHtml("#e2e2e2");
            //            }
            //            else
            //            {
            //                ctrl.Enabled = true;

            //            }
            //        }
            //    }
            //}

        }


    }
}