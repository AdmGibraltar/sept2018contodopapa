using CapaEntidad;
using CapaDatos;
using System;
using System.Web.UI;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;


namespace CapaNegocios
{
    public class CN__Comun
    {
        //RadComboBox
        public void LlenaCombo(string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(SP, conexion, ref Lista);

                //RadComboBox.Items.Clear(); //ric
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    RadComboBox.DataTextField = "Descripcion";
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(Int32 Id1, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    RadComboBox.DataTextField = "Descripcion";
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(Int32 Id1, Int32 Id2, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox, params bool[] claveString)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, SP, conexion, ref Lista, claveString);

                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    if (claveString.Length > 0)
                    {
                        if (claveString[0])
                            RadComboBox.DataValueField = "IdStr";
                        else
                            RadComboBox.DataValueField = "Id";
                    }
                    else
                        RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }

                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboContr(Int32 Id1, Int32 Id2, Int32 Id3,  string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox, params bool[] claveString)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaComboContr(Id1, Id2, Id3, SP, conexion, ref Lista, claveString);

                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    if (claveString.Length > 0)
                    {
                        if (claveString[0])
                            RadComboBox.DataValueField = "IdStr";
                        else
                            RadComboBox.DataValueField = "Id";
                    }
                    else
                        RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }

                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo<T>(Dictionary<string, object> iPars, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox, string pId, string pDesc) where T : class, new()
        {
            try
            {
                List<T> lista = new List<T>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo<T>(iPars, SP, conexion, ref lista);
                if (lista.Count > 0)
                {
                    RadComboBox.DataSource = lista;
                    RadComboBox.DataValueField = pId;
                    RadComboBox.DataTextField = pDesc;
                    
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo<T>(Dictionary<string, object> iPars, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox, string pId, string pDesc, out List<T> pLista) where T : class, new()
        {
            try
            {
                List<T> lista = new List<T>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo<T>(iPars, SP, conexion, ref lista);
                if (lista.Count > 0)
                {                   
                    RadComboBox.DataSource = lista;
                    RadComboBox.DataValueField = pId;
                    RadComboBox.DataTextField = pDesc;

                    RadComboBox.DataBind();
                }
                pLista = lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo<T>(ref Telerik.Web.UI.RadComboBox RadComboBox, string pId, string pDesc, List<T> pLista) where T : class
        {
            try
            {
                RadComboBox.DataSource = pLista;
                RadComboBox.DataValueField = pId;
                RadComboBox.DataTextField = pDesc;

                RadComboBox.DataBind();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public void LlenaCombo(Int32 Id1, Int32 Id2, int? Id3, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";

                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }

                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LlenaCombo(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string conexion, string SP, ref  Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, Id4, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }

                    try {
                        foreach (Comun I in Lista) {
                            if (I.ValorBool) {
                                RadComboBox.SelectedValue = Convert.ToString(I.Id);
                                break;
                            }
                        }
                    }
                    catch { }
                    
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboTerr(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string conexion, string SP, ref  Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, Id4, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboCRM(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string conexion, string SP, ref  Telerik.Web.UI.RadComboBox RadComboBox, Int32 Id_Cd_Ver)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaComboCRM(Id1, Id2, Id3, Id4, SP, conexion, ref Lista, Id_Cd_Ver);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboUEN(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, string conexion, string SP, ref  Telerik.Web.UI.RadComboBox RadComboBox, int Id_Cd_Ver)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaComboUEN(Id1, Id2, Id3, Id4, SP, conexion, ref Lista, Id_Cd_Ver);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, int? Id5, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, Id4, Id5, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(Int32 Id1, Int32 Id2, Int32 Id3, int? Id4, int? Id5, int? Id6, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, Id4, Id5,Id6, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    if (Lista.ToArray()[0].Relacion != "sin_relacion")
                    {
                        RadComboBox.DataTextField = "Relacion";
                    }
                    else
                    {
                        RadComboBox.DataTextField = "Descripcion";
                    }
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaComboStr(Int32 Id1, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaComboStr(Id1, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "IdStr";
                    RadComboBox.DataTextField = "Descripcion";
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaCombo(string Id1, int? Id2, int? Id3, int? Id4, int? Id5, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, Id4, Id5, SP, conexion, ref Lista);
                if (Lista.Count > 0)
                {
                    RadComboBox.DataSource = Lista;
                    RadComboBox.DataValueField = "Id";
                    RadComboBox.DataTextField = "Descripcion";
                    RadComboBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaComboTabla(Int32 Id1, Int32 Id2, Int32 Id3, string conexion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaComboTabla(Id1, Id2, Id3, SP, conexion, ref Lista);

                for (Int32 i = 0; i <= Lista.Count - 1; i++)
                {
                    Telerik.Web.UI.RadComboBoxItem Item = new Telerik.Web.UI.RadComboBoxItem();
                    Item.Value = Lista[i].Id.ToString();
                    Item.Text = Lista[i].Descripcion;
                    RadComboBox.Items.Add(Item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //RadListBox
        public void LlenaListBox(Int32 Id1, Int32 Id2, string conexion, string SP, ref Telerik.Web.UI.RadListBox RadListBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, SP, conexion, ref Lista);

                if (Lista.Count > 0)
                {
                    RadListBox.DataSource = Lista;
                    RadListBox.DataValueField = "Id";
                    RadListBox.DataTextField = "Descripcion";
                    RadListBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaListBox(Int32 Id1, Int32 Id2, int? Id3, string conexion, string SP, ref Telerik.Web.UI.RadListBox RadListBox)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.LlenaCombo(Id1, Id2, Id3, SP, conexion, ref Lista);

                if (Lista.Count > 0)
                {
                    RadListBox.DataSource = Lista;
                    RadListBox.DataValueField = "Id";
                    RadListBox.DataTextField = "Descripcion";
                    RadListBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CorreoRecuperaDatos(ref CentroDistribucion Oficina, ref ConfiguracionGlobal Configuracion, string conexion)
        {
            try
            {
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.CorreoRecuperaDatos(ref Oficina, ref Configuracion, conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Id Sugerido
        public string Maximo(int Id_Emp, int Id_Cd, string Tabla, string Columna, string Conexion, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.Maximo(Id_Emp, Id_Cd, Tabla, Columna, SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Maximo(int Id_Emp, int Id_Cd, string Tabla, string Columna, ICD_Contexto icdCtx, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.Maximo(Id_Emp, Id_Cd, Tabla, Columna, SP, icdCtx, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Maximo(int Id_Emp, int Id_Cd, string Tabla, string Columna, int Tipo, string Conexion, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.Maximo(Id_Emp, Id_Cd, Tabla, Columna, Tipo, SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string MaximoTerritorio(int Id_Emp, int Id_Cd, int Id_TipoRepresentante,int Id_TipoCliente,int Id_Uen,int Id_Seg, string Conexion, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.MaximoTerritorio(Id_Emp, Id_Cd, Id_TipoRepresentante, Id_TipoCliente, Id_Uen, Id_Seg,SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MaximoTerritorio(int Id_Emp, int Id_Cd, int Id_TipoRepresentante, int Id_TipoCliente, int Id_Uen, int Id_Seg, string Conexion, string SP, string prefix)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.MaximoTerritorio(Id_Emp, Id_Cd, Id_TipoRepresentante, Id_TipoCliente, Id_Uen, Id_Seg, prefix, SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string Maximo(int Id_Emp, string Tabla, string Columna, string Conexion, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.Maximo(Id_Emp, Tabla, Columna, SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Maximo(int Id_Emp, string Tabla, int naturaleza, string Columna, string Conexion, string SP)
        {
            try
            {
                string maximo = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.Maximo(Id_Emp, Tabla, naturaleza, Columna, SP, Conexion, ref maximo);
                return maximo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Deshabilitar(Catalogo ct, string Conexion, ref bool verificador)
        {
            try
            {
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                claseCapaDatos.Deshabilitar(ct, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoverValidadores(ValidatorCollection Validators)
        {
            for (int x = 0; x < Validators.Count; x++)
                Validators[0].IsValid = true;
        }
        public string Actual(int Id_Emp, int Id_Cd, string Emp_Cnx, int? Cte, int? Prd, int? Terr, string mov, DateTime Fecha)
        {
            try
            {
                double actual = 0;
                double actual2 = 0;
                CapaDatos.CD__Comun claseCapaDatos = new CapaDatos.CD__Comun();
                if (mov == "1")
                    claseCapaDatos.Actual(Id_Emp, Id_Cd, Emp_Cnx, Cte, Prd, Terr, "BiFacturado", Fecha, ref actual);
                else if (mov == "2")
                    claseCapaDatos.Actual(Id_Emp, Id_Cd, Emp_Cnx, Cte, Prd, Terr, "BiComodato", Fecha, ref actual);
                else
                {
                    claseCapaDatos.Actual(Id_Emp, Id_Cd, Emp_Cnx, Cte, Prd, Terr, "BiGlobal", Fecha, ref actual);
                }
                return (actual + actual2).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaFactura_ConsecutivoFacElectronica(int Id_Emp, int Id_Cd, int Id_Cfe, int Cfe_TMov, ref int verificador, string Conexion)
        {
            try
            {
                new CD__Comun().ConsultaFactura_ConsecutivoFacElectronica(Id_Emp, Id_Cd, Id_Cfe, Cfe_TMov, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarCdVer(RadComboBoxItem item, ref Sesion sesion)
        {
            try
            {
                sesion.Id_Cd_Ver = Convert.ToInt32(item.Value);
                sesion.Cd_Nombre = item.Text;
                CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                cn_catcalendario.ConsultaCalendarioActual(ref calendario, sesion);
                sesion.CalendarioIni = calendario.Cal_FechaIni;
                sesion.CalendarioFin = calendario.Cal_FechaFin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarFechas(int anio, int Mes, ref Sesion sesion, string SP, ref Telerik.Web.UI.RadComboBox RadComboBox)
        {
            try
            {
                //sesion.Id_Cd_Ver = Convert.ToInt32(item.Value);
                //sesion.Cd_Nombre = item.Text;
                CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                cn_catcalendario.ConsultaCalendarioActual(ref calendario, sesion);
                sesion.CalendarioIni = calendario.Cal_FechaIni;
                sesion.CalendarioFin = calendario.Cal_FechaFin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DesgloceRangoProductos(string cadena)
        {
            StringBuilder condicion = new StringBuilder("");
            string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] split2;

            foreach (string a in split)
            {
                if (a.Contains("-"))
                {
                    split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = Convert.ToInt32(split2[0]); i < Convert.ToInt32(split2[1]) + 1; i++)
                        condicion.Append(i.ToString() + ",");
                }
                else
                    condicion.Append(a + ",");
            }

            string condicionStr = condicion.ToString();
            if (condicionStr.Length > 0)
            {
                if (condicionStr[condicionStr.Length - 1] == ',')
                    condicionStr = condicionStr.Substring(0, condicionStr.Length - 1);
            }
            else
                condicionStr = null;
            return condicionStr;
        }

        public string ValidarRango(string cadena)
        {
            string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in split)
            {
                if (a.StartsWith("-"))
                    return a.ToString();
            }
            return "";
        }

        public void ExportarExcel(String nombreArchivo, String tabla)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xls");
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"; //Excel
                System.IO.StringWriter sw = new System.IO.StringWriter();
                sw.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv='content-type' content='text/html; charset=UTF-8' />");
                sw.WriteLine("<title>");
                sw.WriteLine("Page-");
                sw.WriteLine(Guid.NewGuid().ToString());
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.Write(tabla);
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Clase genérica para convertir una Lista Genérica de elementos en
        /// un objeto DataTable
        /// </summary>
        /// <typeparam name="T">Tipo de datos de los elementos de la Lista. 
        /// Debe ser una clase con un constructor sin parámetros. ver referencia de clases genericas</typeparam>

        public static class Convertidor<T> where T : new()
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="items"></param>
            /// <returns></returns>

            public static DataTable ListaToDatatable(List<T> items)
            {

                // Instancia del objeto a devolver

                DataTable dataTable = new DataTable();

                // Información del tipo de datos de los elementos del List

                Type itemsType = typeof(T);

                // Recorremos las propiedades para crear las columnas del datatable

                foreach (PropertyInfo prop in itemsType.GetProperties())
                {

                    // Crearmos y agregamos una columna por cada propiedad de la entidad

                    DataColumn column = new DataColumn(prop.Name);

                    column.DataType = prop.PropertyType;

                    dataTable.Columns.Add(column);

                }



                int j;

                // ahora recorremos la colección para guardar los datos
                // en el DataTable

                foreach (T item in items)
                {

                    j = 0;

                    object[] newRow = new object[dataTable.Columns.Count];

                    // Volvemos a recorrer las propiedades de cada item para
                    // obtener su valor guardarlo en la fila de la tabla

                    foreach (PropertyInfo prop in itemsType.GetProperties())
                    {

                        newRow[j] = prop.GetValue(item, null);

                        j++;

                    }

                    dataTable.Rows.Add(newRow);

                }

                // Devolver el objeto creado
                return dataTable;

            }



            /// <summary>
            /// Métod encargado de recorrer el DataTable y asignar propiedades al objeto
            /// </summary>
            /// <returns>Una lista de objetos T</returns>

            public static List<T> DataTableToLista(DataTable tabla)
            {

                List<T> lista = new List<T>();

                T elemento;

                for (int i = 0; i < tabla.Rows.Count; i++)
                {

                    // Información del tipo de datos de los elementos del List

                    Type itemsType = typeof(T);

                    elemento = new T();



                    foreach (PropertyInfo prop in itemsType.GetProperties())
                    {

                        //Establecemos cada una de las propiedades

                        prop.SetValue(elemento, ValorDefault(tabla.Rows[i][prop.Name]), null);

                    }

                    lista.Add(elemento);



                }

                return lista;

            }



            //Esta parte hay que buscar hacerla de una mejor modo
            /// <summary>
            /// Método que se encarga de validar los DBNull y convertirlos en una cadena vacia
            /// </summary>
            /// <returns>El mismo objeto de entrada validado</returns>

            private static object ValorDefault(object objeto)
            {

                if (objeto == System.DBNull.Value)

                    return "";

                else

                    return objeto;

            }

        }

     


    }
}
