using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaModelo_CC.CuentasCoorporativas;

namespace CapaNegocios
{
    public class CN_CatCNac_ACYS
    {

       

        public CN_CatCNac_ACYS()
        {
         
        }


        public List<CatCNac_ACYS> ConsultarACYS(int idMatriz)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS();
            return CACYS.ConsultarACYS(idMatriz);
        }



        public CatCNac_ACYS ConsultarACYS_Item(int id)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS();
            return CACYS.ConsultarACYS_Item(id);
        }

        public List<CatCNac_ACYS> ConsultarACYS_Item(int id_Matriz, string Nombre)
        {
            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS();
            return CACYS.ConsultarACYS_Item(id_Matriz, Nombre);
        }

        public CapAcy ConsultaAcys(int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion)
        {
            try
            {
                CD_CatCNac_ACYS clsCD = new CD_CatCNac_ACYS();
                return clsCD.ConsultaAcys(Id_Emp, Id_Cd, Id_Acs, Id_AcsVersion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<spCNac_DatosACYSCN_Result> ConsultaDatosACYSCN(string clienteSIAN)
        {

            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS();
            return CACYS.ConsultaDatosACYSCN(clienteSIAN);
        }


        public Boolean EsFranquicia(int Cd)
        {

            CD_CatCNac_ACYS CACYS = new CD_CatCNac_ACYS();
            return CACYS.EsFranquicia(Cd);


        }

    }
}
