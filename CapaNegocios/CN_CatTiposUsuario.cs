using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;

namespace CapaNegocios
{
  public  class CN_CatTiposUsuario
    {
        public void ConsultaTiposDeUsuario(TipoUsuario tipoUsuario, string conexion, ref System.Collections.Generic.List<TipoUsuario> List)
        {
            try
            {
                CapaDatos.CD_CatTiposUsuario claseCapaDatos = new CapaDatos.CD_CatTiposUsuario();
                claseCapaDatos.ConsultaTiposDeUsuario(tipoUsuario, conexion,ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaTiposDeUsuarioPorNombre(TipoUsuario tipoUsuario, string conexion, ref System.Collections.Generic.List<TipoUsuario> List)
        {
            try
            {
                CapaDatos.CD_CatTiposUsuario claseCapaDatos = new CapaDatos.CD_CatTiposUsuario();
                claseCapaDatos.ConsultaTiposDeUsuarioPorNombre(tipoUsuario, conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarTipoUsuario(TipoUsuario tipoUsuario, string conexion, ref Telerik.Web.UI.RadGrid RadGrid, ref int Item)
        {
            try
            {
                System.Collections.Generic.List<Permiso> Lista = new System.Collections.Generic.List<Permiso>();
                CapaDatos.CD_CatTiposUsuario claseCapaDatos = new CapaDatos.CD_CatTiposUsuario();
                claseCapaDatos.InsertarTipoUsuario(ref tipoUsuario, conexion,ref Lista);

                if (Lista.Count > 1)
                {
                    RadGrid.DataSource = Lista;
                    RadGrid.DataBind();
                    Item = 1;
                }
                else
                {
                    Item = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarTipoUsuario(TipoUsuario tipoUsuario, string conexion, ref int Verificador)
        {
            try
            {
                CapaDatos.CD_CatTiposUsuario claseCapaDatos = new CapaDatos.CD_CatTiposUsuario();
                claseCapaDatos.ModificarTipoUsuario(tipoUsuario, conexion,ref Verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
