using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;


namespace CapaNegocios
{
    public class CN_wfrmCampanas
    {
        /// <summary>
        /// Consulta un listado de campañas
        /// </summary>
        public void ConsultaCampanas(string Conexion, int id_Emp, int Id_Cd, ref List<Campanas> List)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.ConsultaCampanas(Conexion, id_Emp, Id_Cd, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Consulta una campaña individual
        /// </summary>
        public void ConsultaCampana(ref Campanas campana, string Conexion)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.ConsultaCampana(ref campana, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCampanaAplicaciones(string Conexion, int id_Emp, int id_Cd, int id_Cam, ref List<AplicacionCampana> List)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.ConsultaCampanaAplicaciones(Conexion, id_Emp, id_Cd, id_Cam, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Cosnulta un listado de las metas de una campaña
        /// </summary>
        public void ConsultaCampanasMetasLista(string Conexion, int id_Emp, int id_Cd, int id_Cam, ref List<CampanasMetas> List)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.ConsultaCampanasMetasLista(Conexion, id_Emp, id_Cd, id_Cam, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta las metas de una campaña, en base a la UEN y los representantes de la UEN
        /// </summary>
        public void ConsultaCampanasMetas(string Conexion, int id_Emp, int id_Cd, int id_Uen, int? id_Cam, ref List<CampanasMetas> List)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.ConsultaCampanasMetas(Conexion, id_Emp, id_Cd, id_Uen, id_Cam, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarCampana(int Id_Emp, int Id_Cam, string Conexion, ref int verificador)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.EliminarCampana(Id_Emp, Id_Cam, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarCampanaAplicacion(int Id_Emp, int Id_Cam, int Id_Apl, string Conexion, ref int verificador)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.EliminarCampanaAplicacion(Id_Emp, Id_Cam, Id_Apl, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarCampana(ref Campanas campana, string Conexion, ref int verificador)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.InsertarCampana(ref campana, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarCampanaMetas(int id_Cam, ref List<CampanasMetas> listametasCampana, string Conexion, ref int verificador)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.GuardarCampanaMetas(id_Cam, ref listametasCampana, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarCampana(ref Campanas campana, string Conexion, ref int verificador)
        {
            try
            {
                CD_wfrmCampanas claseCapaDatos = new CD_wfrmCampanas();
                claseCapaDatos.ModificarCampana(ref campana, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
