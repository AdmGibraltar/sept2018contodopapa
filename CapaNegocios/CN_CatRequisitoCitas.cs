using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace CapaNegocios
{
    public class CN_CatRequisitoCitas
    {
        public void ConsultaRequisitosCita(RequisitoCita requisitoCita, string Conexion, ref List<RequisitoCita> List)
        {
            try
            {
                CD_CatRequisitoCitas claseCapaDatos = new CD_CatRequisitoCitas();
                claseCapaDatos.ConsultaRequisitosCita(requisitoCita, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void VerCalendario(string Conexion, int Emp, int Cd, int Usuario,ref int refer)
        {
            try
            {
                CD_CatRequisitoCitas claseCapaDatos = new CD_CatRequisitoCitas();
                claseCapaDatos.VerCalendario(Conexion, Emp, Cd, Usuario, ref refer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ListadoPrerequisitosCita_Todos(string Conexion, int Cita, ref List<RequisitoCita> Listado)
        {
            try
            {
                CD_CatRequisitoCitas claseCapaDatos = new CD_CatRequisitoCitas();
                claseCapaDatos.ListadoPrerequisitosCita_Todos(Conexion, Cita, ref Listado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ListadoPrerequisitos_Todos(string Conexion, string CitaVisita, ref List<RequisitoCita> Listado)
        {
            try
            {
                CD_CatRequisitoCitas claseCapaDatos = new CD_CatRequisitoCitas();
                claseCapaDatos.ListadoRequisitos_Cita(Conexion, CitaVisita, ref Listado);
                /*
                if (Lista.Count > 0)
                {
                    Listado.DataSource = Lista;
                    Listado.DataValueField = "Id";
                    Listado.DataTextField = "Descripcion";
                    Listado.DataBind();
                }
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarRequisitosCita(List<RequisitoCita> Listado, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatRequisitoCitas claseCapaDatos = new CD_CatRequisitoCitas();
                claseCapaDatos.InsertarRequisitosCita(Listado, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
