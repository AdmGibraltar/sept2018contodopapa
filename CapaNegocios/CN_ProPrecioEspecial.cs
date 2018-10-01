using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_PrecioEspecial
    {
        public void ConsultaVentanaPrecioEspecial_ComboCliente(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion, ref string Cte_NomComercial)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaVentanaPrecioEspecial_ComboCliente(Id_Emp, Id_Cd, Id_Cte, Conexion, ref Cte_NomComercial);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaPrecioEspecial_ComboProducto(int Id_Emp, int Id_Cd, int Id_Prd, string Conexion, ref string Prd_Descripcion)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaVentanaPrecioEspecial_ComboProducto(Id_Emp, Id_Cd, Id_Prd, Conexion, ref Prd_Descripcion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarVentanaPrecioEspecial(PrecioEspecial VentanaPrecioEspecial, string Conexion, ref string verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.InsertarVentanaPrecioEspecial(VentanaPrecioEspecial, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEmailsPt1(int Id_Emp, int Id_Cd, string Conexion, ref string pipelist)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultarEmailsPt1(Id_Emp, Id_Cd, Conexion, ref pipelist);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEmailsPt2(int Id_Emp, int Id_Cd, string Conexion, string Email, ref string nombre, ref int? id)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultarEmailsPt2(Id_Emp, Id_Cd, Conexion, Email, ref nombre, ref id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaPrecioEspecial(ref PrecioEspecial VentanaPrecioEspecial, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaVentanaPrecioEspecial(ref VentanaPrecioEspecial, Conexion, Id_Emp, Id_Cd, folio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVentanaPrecioEspecialPro(PrecioEspecial ape, string Conexion, ref List<VentanaPrecioEspecialPro> List)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaVentanaPrecioEspecialPro(ape, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActualizaProveedor(PrecioEspecial ape, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ActualizaProveedor(ape, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PrecioEspecialProductoCliente_Consulta(ref VentanaPrecioEspecialPro precioEspecialPro, string Conexion, int id_Emp, int id_Cd, int id_Cli, int id_Prd)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, Conexion, id_Emp, id_Cd, id_Cli, id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PrecioEspecialSolicitudesVencidas_Consulta(ref List<VentanaPrecioEspecialPro> lista, string Conexion, int id_Emp, int id_Cd, int id_Cli, int id_Prd/*, int id_Mon*/)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.PrecioEspecialSolicitudesVencidas_Consulta(ref lista, Conexion, id_Emp, id_Cd, id_Cli, id_Prd/*, id_Mon*/);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarVentanaPrecioEspecial(PrecioEspecial VentanaPrecioEspecial, string Conexion, ref string verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ModificarVentanaPrecioEspecial(VentanaPrecioEspecial, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProAutPrecioEspecial_Lista(AutPrecioEspecial AutPrecioEspecial, string Conexion, ref List<AutPrecioEspecial> List)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaProAutPrecioEspecial_Lista(AutPrecioEspecial, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProAutPrecioEspecialVencido(ref int Vencido, int Id_Emp, int Id_Cd, int Id_Ape, string Conexion)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaProAutPrecioEspecialVencido(ref Vencido, Id_Emp, Id_Cd, Id_Ape, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProAutPrecioEspecial(ref PrecioEspecial ape, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaProAutPrecioEspecial(ref ape, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaProveedorSeleccionado(PrecioEspecial ape, string Conexion, ref int verificador, ref bool tieneProveedorNS)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                 claseCapaDatos.ConsultaProveedorSeleccionado(ape, Conexion, ref verificador, ref  tieneProveedorNS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaVentanaPrecioEspecialCte(PrecioEspecial ape, string Conexion, ref List<VentanaPrecioEspecialCte> List)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaVentanaPrecioEspecialCte(ape, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarPrecioEspecial(PrecioEspecial ape, string Conexion, List<VentanaPrecioEspecialPro> List, ref int verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.AutorizarPrecioEspecial(ape, Conexion, List, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaProductoCliente(ref List<Clientes> List_cte, string Conexion,ref DateTime? Ape_FecInicio, ref DateTime? Ape_FecFin)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaProductoCliente(ref List_cte, Conexion,ref Ape_FecInicio,ref Ape_FecFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EnviarPrecioEspecial(PrecioEspecial ape, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.EnviarPrecioEspecial(ape, Conexion, ref verificador );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEnvio(ref PrecioEspecial pe, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.ConsultaEnvio(ref pe, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaDatosEmail(ref PrecioEspecial pe, int Id_Ape, Sesion sesion)
        {
            try
            {
                CD_ProPrecioEspecial pp = new CD_ProPrecioEspecial();
                pp.ConsultaDatosEmail(ref pe,  Id_Ape, sesion);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void Eliminar(PrecioEspecial p, string Conexion,ref int verificador)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.Eliminar(p, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Versión que acepta una transacción de negocio.
        /// </summary>
        /// <param name="precioEspecialPro"></param>
        /// <param name="icdCtx">Contexto de conexión a la fuente de datos</param>
        /// <param name="id_Emp"></param>
        /// <param name="id_Cd"></param>
        /// <param name="id_Cli"></param>
        /// <param name="id_Prd"></param>
        public void PrecioEspecialProductoCliente_Consulta(ref VentanaPrecioEspecialPro precioEspecialPro, IBusinessTransaction ibt, int id_Emp, int id_Cd, int id_Cli, int id_Prd)
        {
            try
            {
                CD_ProPrecioEspecial claseCapaDatos = new CD_ProPrecioEspecial();
                claseCapaDatos.PrecioEspecialProductoCliente_Consulta(ref precioEspecialPro, ibt.DataContext, id_Emp, id_Cd, id_Cli, id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
