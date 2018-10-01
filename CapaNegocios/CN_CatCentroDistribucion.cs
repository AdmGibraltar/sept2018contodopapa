using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatCentroDistribucion
    {

        public void ConsultarCentroDistribucionCombo(int Id1, int Id2, int Id3, int Id4, ref List<CrmLista> Lista, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentroDistribucionCombo(Id1, Id2, Id3, Id4, ref Lista, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verisón que acepta una transacción de la capa de negocio
        /// </summary>
        /// <param name="Oficina"></param>
        /// <param name="Id_Cd"></param>
        /// <param name="Id_Emp"></param>
        /// <param name="ibt">Transacción de la capa de negocio</param>
        public void ConsultarCentroDistribucion(ref CentroDistribucion Oficina, int Id_Cd, int Id_Emp, IBusinessTransaction ibt)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentroDistribucion(ref Oficina, Id_Cd, Id_Emp, ibt.DataContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentroDistribucion(ref CentroDistribucion Oficina, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentroDistribucion(ref Oficina, Id_Cd, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentroDistribucionLista(string Conexion, ref List<CentroDistribucion> list)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentroDistribucionLista(Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCentroDistribucionCombo(string Conexion, ref List<CentroDistribucion> list)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentroDistribucionLista(Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCentroDistribucion_DatosValProyecto(ref CentroDistribucion Oficina, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentroDistribucion_DatosValProyecto(ref Oficina, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCentroDistribucion_DatosValProyecto(ref CentroDistribucion VarOficina, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ActualizarCentroDistribucion_DatosValProyecto(ref VarOficina, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCentroDistribucion(ref CentroDistribucion Oficina, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.GuardarCentroDistribucion(ref Oficina, Conexion, ref verificador, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCentroDistribucion(ref CentroDistribucion Oficina, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.GuardarCentroDistribucion(ref Oficina, Conexion, ref verificador, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCentroDistribucion_DatosValuacionProyectos(ref CentroDistribucion centroDistribucion, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ModificarCentroDistribucion_DatosValuacionProyectos(ref centroDistribucion, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCentroDistribucion(int Id_Cd, string Conexion, ref int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.EliminarCentroDistribucion(Id_Cd, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarIva(int Id_Emp, int Id_Cd, ref double Iva, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultarIva(Id_Emp, Id_Cd, Conexion, ref Iva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCobranzaCentroDistribucion(ref List<CentroDistribucion> ListaCentroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultarCobranzaCentroDistribucion(ref ListaCentroDistribucion, Id_Cd, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCentroDistribucion_Cobranza(ref CentroDistribucion ListaCentroDistribucion, Sesion sesion, int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ModificarCentroDistribucion_Cobranza(ref ListaCentroDistribucion, sesion, verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarCentroDistribucion_Cobranza(List<CentroDistribucion> ListaCentroDistribucion, int Id_Cd, Sesion sesion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.AgregarCentroDistribucion_Cobranza(ListaCentroDistribucion, Id_Cd, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void EliminarCentroDistribucion_Cobranza(ref CentroDistribucion ListaCentroDistribucion, int Id_Cd, Sesion sesion)
        //{
        //    try
        //    {
        //        CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
        //        cd.EliminarCentroDistribucion_Cobranza(ref ListaCentroDistribucion, Id_Cd, sesion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ConsultarRentabilidadCentroDistribucion(ref List<CentroDistribucion> ListaCentroDistribucion, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultarRentabilidadCentroDistribucion(ref ListaCentroDistribucion, Id_Cd, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCentroDistribucion_Rentabilidad(ref CentroDistribucion ListaCentroDistribucion, Sesion sesion, int verificador)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ModificarCentroDistribucion_Rentabilidad(ref ListaCentroDistribucion, sesion, verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarCentroDistribucion_Rentabilidad(List<CentroDistribucion> ListaCentroDistribucion, int Id_Cd, Sesion sesion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.AgregarCentroDistribucion_Rentabilidad(ListaCentroDistribucion, Id_Cd, sesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void EliminarCentroDistribucion_Rentabilidad(ref CentroDistribucion ListaCentroDistribucion, Sesion sesion)
        //{
        //    try
        //    {
        //        CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
        //        cd.EliminarCentroDistribucion_Rentabilidad(ref ListaCentroDistribucion, sesion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ConsultaCentrosPropios(ref List<CentroDistribucion> list, int Id_Emp, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCentrosPropios(ref list, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCd_Usuario(ref List<CentroDistribucion> list, int Id_Emp, int? Id_U, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.ConsultaCd_Usuario(ref list, Id_Emp, Id_U, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarZona(string zonas, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                cd.AgregarZona(zonas, Id_Cd, Id_Emp, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edsg28062017
        public Boolean ValidarVariasUEN(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion)
        {
            try
            {
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();
                return cd.ValidarVariasUEN(Id_Emp, Id_Cd, Id_Cte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
