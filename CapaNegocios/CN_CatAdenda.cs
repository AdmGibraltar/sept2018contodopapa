using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CatAdenda
    {
        public void ConsultaAdenda(Adenda tipoCosto, string Conexion, ref List<Adenda> List)
        {
            try
            {
                CD_CatAdenda claseCapaDatos = new CD_CatAdenda();
                claseCapaDatos.ConsultaAdenda(tipoCosto, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarAdenda(Adenda tipoCosto, string Conexion, DataTable dt, ref int verificador)
        {
            try
            {
                CD_CatAdenda claseCapaDatos = new CD_CatAdenda();
                claseCapaDatos.InsertarAdenda(tipoCosto, Conexion, dt, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAdenda(Adenda tipoCosto, string Conexion, DataTable dt, ref int verificador)
        {
            try
            {
                CD_CatAdenda claseCapaDatos = new CD_CatAdenda();
                claseCapaDatos.ModificarAdenda(tipoCosto, Conexion, dt, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAdenda(Adenda adenda, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CatAdenda claseCapaDatos = new CD_CatAdenda();
                claseCapaDatos.ConsultaAdenda(adenda, dt, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
