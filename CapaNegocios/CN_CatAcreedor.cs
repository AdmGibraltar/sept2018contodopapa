using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatAcreedor
    {
        public void InsertarAcreedor(Acreedor acreedor, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatAcreedor().InsertarAcreedor(acreedor, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAcreedor(Acreedor acreedor, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatAcreedor().ModificarAcreedor(acreedor, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AutorizarAcreedor(Acreedor acreedor, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CatAcreedor().AutorizarAcreedor(acreedor, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAcreedor(Acreedor acreedor, string Conexion)
        {
            try
            {
                new CD_CatAcreedor().ConsultaAcreedor(acreedor, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAcreedor(Acreedor acreedor, string Conexion, ref List<Acreedor> list)
        {
            try
            {
                new CD_CatAcreedor().ConsultaAcreedor(acreedor, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidaRFC(int Id_Acr, string Acr_RFC, string Conexion)
        {
            bool Result = false;
            try
            {
                Result = (new CD_CatAcreedor().ValidaRFC(Id_Acr, Acr_RFC, Conexion));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        //jfcv 26oct2016 consulta por numero generado  punto 4
        public void ConsultaAcreedorPorNumero(Acreedor acreedor, string Conexion)
        {
            try
            {
                new CD_CatAcreedor().ConsultaAcreedorPorNumero(acreedor, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
