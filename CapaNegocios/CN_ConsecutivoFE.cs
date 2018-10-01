using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ConsecutivoFE
    {

        public void ConsultaConsecutivo(int Id_Emp, int Id_Cd, string conexion, ref List<ConsecutivoFE> list)
        {
            try
            {
                CapaDatos.CD_ConsecutivoFE ClaseCapaDatos = new CapaDatos.CD_ConsecutivoFE();
                ClaseCapaDatos.ConsultaConsecutivo(Id_Emp, Id_Cd , conexion, ref list);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void InsertarConsecutivo(ref ConsecutivoFE FactElect, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_ConsecutivoFE claseCapaDatos = new CapaDatos.CD_ConsecutivoFE();
                claseCapaDatos.InsertarConsecutivo(ref FactElect, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConsecutivo(ref ConsecutivoFE FactElect, string conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_ConsecutivoFE claseCapaDatos = new CapaDatos.CD_ConsecutivoFE();
                claseCapaDatos.ModificarConsecutivo(ref FactElect, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaConsecutivo(int Id_Emp, int Cfe_Tmov, string Cfe, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_ConsecutivoFE claseCapaDatos = new CapaDatos.CD_ConsecutivoFE();
                claseCapaDatos.ConsultaConsecutivo(Id_Emp,Cfe_Tmov, Cfe, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
