using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
  public  class CN_CatUen
    {
      public void ConsultaUen(int Id_Emp, string Conexion, ref List<Uen> List)
        {
            try
            {
                CD_CatUen claseCapaDatos = new CD_CatUen();
                claseCapaDatos.ConsultaUen(Id_Emp, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public void InsertarUen(Uen uen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatUen claseCapaDatos = new CD_CatUen();
                claseCapaDatos.InsertarUen(uen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public void ModificarUen(Uen uen, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatUen claseCapaDatos = new CD_CatUen();
                claseCapaDatos.ModificarUen(uen, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



      public void ConsultaUen_Usuario(ref List<Uen> list, int Id_Emp, int? Id_U, string Conexion)
      {
          try
          {
              CD_CatUen claseCapaDatos = new CD_CatUen();
              claseCapaDatos.ConsultaUen_Usuario(ref list, Id_Emp, Id_U, Conexion);

          }
          catch (Exception ex)
          {
              throw ex;
          }
      }

      public IEnumerable<CatUEN> ObtenerUENsDeRepresentante(int idEmp, int idCd, int idRik, Sesion sesion)
      {
          CD_CatUen cdCatUen = new CD_CatUen();
          return cdCatUen.ConsultarUENsDeRepresentante(idEmp, idCd, idRik, sesion.Emp_Cnx_EF);
      }

      /// <summary>
      /// Regresa el conjunto de unidades de negocio asociados a una empresa
      /// </summary>
      /// <param name="s">Sesión del usuario en operación</param>
      /// <param name="ibt">Transacción de la capa de negocio</param>
      /// <returns>IEnumerable[CatUEN]</returns>
      public IEnumerable<CatUEN> ObtenerUEnsDeEmpresa(Sesion s, IBusinessTransaction ibt)
      {
          CD_CatUen cdCatUen = new CD_CatUen();
          return cdCatUen.ConsultarPorEmpresa(s.Id_Emp, ibt.DataContext);
      }
    }
}
