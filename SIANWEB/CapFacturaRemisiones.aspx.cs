using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;


namespace SIANWEB
{
    public partial class CapFacturaRemisiones : System.Web.UI.Page
    {    
        #region Propiedades
        //Propiedad de lista de productos (partidas) de la factura
        private List<Remision> ListaRemisionesFactura
        {
            get { return (List<Remision>)Session["ListaRemisionesFactura" + Session.SessionID]; }
            set { Session["ListaRemisionesFactura" + Session.SessionID] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!Page.IsPostBack)
                {
                    CN_CapFactura fac = new CN_CapFactura();
                    string[] datosGen = fac.ConsultaFacturacion_DatosGeneralesFacturacion(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver);
                    lblEmpresaId.Text = sesion.Id_Emp.ToString();
                    lblEmpresaNombre.Text = datosGen[0];
                    lblSucursalId.Text = sesion.Id_Cd_Ver.ToString();
                    lblSucursalNombre.Text = datosGen[1];
                    lblRegionId.Text = datosGen[3];
                    lblRegionNombre.Text = datosGen[2];
                    lblUsuarioId.Text = sesion.Id_U.ToString();
                    lblUsuarioNombre.Text = sesion.U_Nombre;

                    this.ListaRemisionesFactura = new List<Remision>();

                    double ancho = 0;
                    foreach (GridColumn gc in rgRemisiones.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    rgRemisiones.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgRemisiones.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                    rgRemisiones.Rebind();
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region "Métodos para manejar la lista dinámica de Productos de una orden de compra"

        protected void ListaRemisiones_AgregarRemisiones(Remision remision)
        {
            List<Remision> lista = this.ListaRemisionesFactura;

            //buscar cliente de remision en la lista para ver si ya existe
            for (int i = 0; i < lista.Count; i++)
            {
                Remision rem = lista[i];
                if (remision.Id_Cte != rem.Id_Cte)
                {
                    throw new Exception("rgRemisiones_insert_repetida");
                }
                if(remision.Id_Tm != rem.Id_Tm)
                {
                    throw new Exception("rgRemisiones_insert_Id_Tm_repetida");
                }
            }
            lista.Add(remision);
            this.ListaRemisionesFactura = lista;
        }

        protected void ListaRemisiones_ModificarRemisiones(Remision remision)
        {
            List<Remision> lista = this.ListaRemisionesFactura;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                Remision rem = lista[i];
                if (rem.Id_Rem == Convert.ToInt32(this.HD_Id_Rem.Value))
                {
                    lista[i] = remision;
                    break;
                }
            }
            this.ListaRemisionesFactura = lista;
        }

        protected void ListaRemisiones_EliminarRemision(int id_Rem)
        {
            List<Remision> lista = this.ListaRemisionesFactura;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                Remision rem = lista[i];
                if (rem.Id_Rem == id_Rem)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.ListaRemisionesFactura = lista;
        }

        protected void ListaProductosOrdenCompra_EliminarTodos()
        {
            this.ListaRemisionesFactura.Clear();
        }

        #endregion

        #region Metodos

        protected void rgRemisiones_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (rgRemisiones.EditItems.Count > 0)
                        {
                            Alerta("Ya está editando un registro");
                            e.Canceled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgRemisiones_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgRemisiones.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void rgRemisiones_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //Llenar Grid
                    rgRemisiones.DataSource = this.ListaRemisionesFactura;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgRemisiones_NeedDataSource"));
            }
        }

        protected void rgRemisiones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    string txtId_Rem = ((RadNumericTextBox)editItem.FindControl("txtId_Rem")).ClientID.ToString();
                    string lblVal_txtId_Rem = ((Label)editItem.FindControl("lblVal_txtId_Rem")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "lblVal_txtId_RemClientId='", lblVal_txtId_Rem, "';"
                        , "txtId_RemClientId='", txtId_Rem, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        jsControles = string.Concat(
                            jsControles
                            , "return ValidaFormEdit(\"actualizar\");");

                        this.HD_Id_Rem.Value = ((RadNumericTextBox)editItem.FindControl("txtId_Rem")).Text;

                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                    ((RadNumericTextBox)editItem.FindControl("txtId_Rem")).Focus();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgRemisiones_ItemDataBound"));
            }
        }

        protected void rgRemisiones_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Remision remision = new Remision();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                remision.Id_Emp = sesion.Id_Emp;
                remision.Id_Cd = sesion.Id_Cd_Ver;
                remision.Id_Rem = Convert.ToInt32((insertedItem["Id_Rem"].FindControl("txtId_Rem") as RadNumericTextBox).Text);
                string[] datePart = (insertedItem["Rem_Fecha"].FindControl("lblRem_FechaEdit") as Label).Text.Split(new char[] { '/' });
                remision.Rem_Fecha = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));
                remision.Id_Cte = Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                remision.NombreCliente = (insertedItem["NombreCliente"].FindControl("lblNombreClienteEdit") as Label).Text;
                remision.Rem_Estatus = (insertedItem["Rem_Estatus"].FindControl("lblRem_EstatusEdit") as Label).Text;
                remision.Rem_EstatusStr = (insertedItem["Rem_EstatusStr"].FindControl("lblRem_EstatusStrEdit") as Label).Text;

                //agregar producto de orden de compra a la lista
                this.ListaRemisiones_AgregarRemisiones(remision);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRemisiones_insert_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        protected void rgRemisiones_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Remision remision = new Remision();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                
                remision.Id_Emp = sesion.Id_Emp;
                remision.Id_Cd = sesion.Id_Cd_Ver;
                remision.Id_Rem = Convert.ToInt32((editedItem["Id_Rem"].FindControl("txtId_Rem") as RadNumericTextBox).Text);
                string[] datePart = (editedItem["Rem_Fecha"].FindControl("lblRem_FechaEdit") as Label).Text.Substring(0,10).Split(new char[] { '/' });
                remision.Rem_Fecha = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));
                remision.Id_Cte = Convert.ToInt32((editedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                remision.NombreCliente = (editedItem["NombreCliente"].FindControl("lblNombreClienteEdit") as Label).Text;
                remision.Rem_Estatus = (editedItem["Rem_Estatus"].FindControl("lblRem_EstatusEdit") as Label).Text;
                remision.Rem_EstatusStr = (editedItem["Rem_EstatusStr"].FindControl("lblRem_EstatusStrEdit") as Label).Text;

                //agregar remision de la lista
                this.ListaRemisiones_ModificarRemisiones(remision);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRemisiones_update_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        protected void rgRemisiones_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                int Id_Rem = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Rem"]);
                this.ListaRemisiones_EliminarRemision(Id_Rem);
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgRemisiones_delete_error");
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        protected void txtId_Rem_TextChanged(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Remision remision = new Remision();

            RadNumericTextBox txtId_Rem = (RadNumericTextBox)sender;
            remision.Id_Emp = sesion.Id_Emp;
            remision.Id_Cd = sesion.Id_Cd_Ver;
            remision.Id_Rem = Convert.ToInt32(txtId_Rem.Text);
            CN_CapRemision cn_remision = new CN_CapRemision();


            Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)txtId_Rem.Parent;
            //limpia los datos de la remision
            txtId_Rem.Text = string.Empty;
            ((Label)tabla.FindControl("lblRem_FechaEdit")).Text = string.Empty;
            ((Label)tabla.FindControl("lblId_CteEdit")).Text = string.Empty;
            ((Label)tabla.FindControl("lblNombreClienteEdit")).Text = string.Empty;
            ((Label)tabla.FindControl("lblRem_EstatusEdit")).Text = string.Empty;
            ((Label)tabla.FindControl("lblRem_EstatusStrEdit")).Text = string.Empty;
            
            //validar remision
            if (cn_remision.ConsultaRemisionFacturacion(ref remision, sesion.Emp_Cnx))//si encuentra la remision
            {
                if (remision.Rem_Estatus.ToUpper() == "B")
                {
                    this.DisplayMensajeAlerta("RemisionEstatusBaja");
                }
                else
                {
                    if (remision.Rem_Estatus.ToUpper() == "C")
                    {
                        this.DisplayMensajeAlerta("RemisionEstatusCapturado");
                    }
                    else
                    {
                        //calcular producto pendiente a facturar.
                        int cantidadPendienteFacturar = 0;

                        if (remision.ListRemisionDetalle.Count > 0)
                        {
                            foreach (RemisionDet remisionDet in remision.ListRemisionDetalle)
                            {
                                if (remisionDet.Rem_CantF == null)
                                {
                                    cantidadPendienteFacturar += remisionDet.Rem_Cant;// -Convert.ToInt32(remisionDet.Rem_CantF);
                                }
                                else
                                {
                                    if (remisionDet.Rem_Cant > Convert.ToInt32(remisionDet.Rem_CantF))
                                    {
                                        cantidadPendienteFacturar += remisionDet.Rem_Cant - Convert.ToInt32(remisionDet.Rem_CantF);
                                    }
                                }
                            }
                        }
                        if (cantidadPendienteFacturar > 0)
                        {
                            txtId_Rem.Text = remision.Id_Rem.ToString();
                            ((Label)tabla.FindControl("lblRem_FechaEdit")).Text = remision.Rem_Fecha.ToShortDateString();
                            ((Label)tabla.FindControl("lblId_CteEdit")).Text = remision.Id_Cte.ToString();
                            ((Label)tabla.FindControl("lblNombreClienteEdit")).Text = remision.NombreCliente;
                            ((Label)tabla.FindControl("lblRem_EstatusEdit")).Text = remision.Rem_Estatus;
                            ((Label)tabla.FindControl("lblRem_EstatusStrEdit")).Text = remision.Rem_EstatusStr;
                        }
                        else
                        {
                            this.DisplayMensajeAlerta("RemisionNoProdPendienteFacturar");
                        }
                    }
                }
            }
            else
            {
                this.DisplayMensajeAlerta("RemisionNoExiste");
            }
        }

        #endregion

        #region Funciones

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("rgRemisiones_insert_repetida"))
                    Alerta("Remisión tiene distinto cliente");
                else
                    if (mensaje.Contains("RemisionNoProdPendienteFacturar"))
                        Alerta("Remisión no tiene producto pendiente de facturar");
                    else
                        if (mensaje.Contains("RemisionEstatusBaja"))
                            Alerta("Remisión se encuentra en estatus \"Baja\"");
                        else
                            if (mensaje.Contains("RemisionEstatusCapturado"))
                                Alerta("Remisión se encuentra en estatus \"Capturado\"");
                            else
                                if (mensaje.Contains("RemisionNoExiste"))
                                    Alerta("La remisión no existe");
                                else
                                    if (mensaje.Contains("rgRemisiones_NeedDataSource"))
                                        Alerta("Error al cargar el grid de remisiones");
                                    else
                                        if (mensaje.Contains("rgRemisiones_ItemDataBound"))
                                            Alerta("Error al momento de preparar un registro para edición");
                                        else
                                            if (mensaje.Contains("rgRemisiones_insert_error"))
                                                Alerta("Error al momento de agregar la remisión");
                                            else
                                                if (mensaje.Contains("rgRemisiones_update_error"))
                                                    Alerta("Error al momento de actualizar la remisión");
                                                else
                                                    if (mensaje.Contains("rgRemisiones_delete_error"))
                                                        Alerta("Error al momento de actualizar la remisión");
                                                    else
                                                        if (mensaje.Contains("rgRemisiones_insert_Id_Tm_repetida"))
                                                            Alerta("Remisión tiene distinto tipo de movimiento");
                                                        else
                                                            Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

        #endregion

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
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

        #endregion
    }
}