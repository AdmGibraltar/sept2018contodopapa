using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_EntradaVirtual
    {
        public void ConsultaVentanaEntradaVirtual_ComboCliente(int Id_Emp, int Id_Cd, int Id_Cte, string Conexion, ref string Cte_NomComercial)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaVentanaProEntradaVirtual_ComboCliente(Id_Emp, Id_Cd, Id_Cte, Conexion, ref Cte_NomComercial);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarEntradaSalida(EntradaSalida entsal, List<EntradaSalidaDetalle> listaDetalle, ref string verificadorStr, int strEmp, string Conexion, int Id_Env)
        {
            try
            {
                CD_ProEntradaVirtual cd_capEntradaSalida = new CD_ProEntradaVirtual();
                cd_capEntradaSalida.GuardarEntradaSalida(entsal, listaDetalle, ref verificadorStr, strEmp, Conexion, Id_Env);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVentanaEntradaVirtual_ComboProducto(int Id_Emp, int Id_Cd, int Id_Prd, string Conexion, ref string Prd_Descripcion)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaVentanaProEntradaVirtual_ComboProducto(Id_Emp, Id_Cd, Id_Prd, Conexion, ref Prd_Descripcion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarVentanaEntradaVirtual(EntradaVirtual VentanaEntradaVirtual, string Conexion, ref string verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.InsertarVentanaEntradaVirtual(VentanaEntradaVirtual, Conexion, ref verificador);

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
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
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
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultarEmailsPt2(Id_Emp, Id_Cd, Conexion, Email, ref nombre, ref id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentanaEntradaVirtual(ref EntradaVirtual VentanaEntradaVirtual, string Conexion, int Id_Emp, int Id_Cd, int folio)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaVentanaProEntradaVirtual(ref VentanaEntradaVirtual, Conexion, Id_Emp, Id_Cd, folio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVentanaEntradaVirtualPro(EntradaVirtual ape, string Conexion, ref List<EntradaVirtualDet> List)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaVentanaProEntradaVirtualDet(ape, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEntradaVirtualDetallemov(EntradaVirtual ape, string Conexion, ref List<EntradaVirtualDetalleMov> List)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaEntradaVirtualDetallemov(ape, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaEntradaVirtualSaldo(EntradaVirtual ape, string Conexion, ref List<EntradaVirtualDet> List)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaEntradaVirtualSaldo(ape, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       /* 
        public void EntradaVirtualProductoCliente_Consulta(ref EntradaVirtualDet EntradaVirtualPro, string Conexion, int id_Emp, int id_Cd, int id_Cli, int id_Prd)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.EntradaVirtualProductoCliente_Consulta(ref EntradaVirtualPro, Conexion, id_Emp, id_Cd, id_Cli, id_Prd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
       
        public void ModificarVentanaEntradaVirtual(EntradaVirtual VentanaEntradaVirtual, string Conexion, ref string verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ModificarVentanaEntradaVirtual(VentanaEntradaVirtual, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProAutEntradaVirtual_Lista(AutEntradaVirtual AutEntradaVirtual, string Conexion, ref List<AutEntradaVirtual> List)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaProAutEntradaVirtual_Lista(AutEntradaVirtual, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaProAutEntradaVirtualVencido(ref int Vencido, int Id_Emp, int Id_Cd, int Id_Ape, string Conexion)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaProAutEntradaVirtualVencido(ref Vencido, Id_Emp, Id_Cd, Id_Ape, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProAutEntradaVirtual(ref EntradaVirtual ape, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaProAutEntradaVirtual(ref ape, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaProveedorSeleccionado(EntradaVirtual ape, string Conexion, ref int verificador, ref bool tieneProveedorNS)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaProveedorSeleccionado(ape, Conexion, ref verificador, ref  tieneProveedorNS);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        public void AutorizarEntradaVirtual(EntradaVirtual ape, string Conexion, List<EntradaVirtualDet> List, ref int verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.AutorizarEntradaVirtual(ape, Conexion, List, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaProductoCliente(ref List<Clientes> List_cte, string Conexion, ref DateTime? Ape_FecInicio, ref DateTime? Ape_FecFin)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaProductoCliente(ref List_cte, Conexion, ref Ape_FecInicio, ref Ape_FecFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EnviarEntradaVirtual(EntradaVirtual ape, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.EnviarEntradaVirtual(ape, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



     

        public void ConsultaEnvio(ref EntradaVirtual pe, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.ConsultaEnvio(ref pe, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(EntradaVirtual p, string Conexion, ref int verificador)
        {
            try
            {
                CD_ProEntradaVirtual claseCapaDatos = new CD_ProEntradaVirtual();
                claseCapaDatos.Eliminar(p, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
