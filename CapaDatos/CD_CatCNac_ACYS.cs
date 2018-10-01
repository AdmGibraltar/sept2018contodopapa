using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CapaModelo_CC.CuentasCoorporativas;


namespace CapaDatos
{

    public class CD_CatCNac_ACYS
    {
        sianwebmty_CCEntities model = new sianwebmty_CCEntities();
        public CD_CatCNac_ACYS()
        {
           
        }


        public List<CatCNac_ACYS> ConsultarACYS(int idMatriz)
        {
            var res = model.CatCNac_ACYS.Where(x => x.Id_Matriz == idMatriz).ToList();
            return res;
        }

        public CatCNac_ACYS ConsultarACYS_Item(int id)
        {
            var res = model.CatCNac_ACYS.Where(x => x.Id == id).FirstOrDefault();
            return res;
        }

        public List<CatCNac_ACYS> ConsultarACYS_Item(int id_Matriz, string Nombre)
        {
            var res = model.CatCNac_ACYS.Where(x => x.Nombre.Contains(Nombre) && x.Id_Matriz == id_Matriz).ToList();
            return res;
        }

        // EDSG 28022017 Cuentas Nacionales
        public CapAcy ConsultaAcys(int Id_Emp, int Id_Cd, int Id_Acs, int Id_AcsVersion)
        {
            CapAcy ret = null;
            //sianwebmty_gEntities ctx = new sianwebmty_gEntities();

            ret = model.CapAcys.Where(x => x.Id_Emp == Id_Emp && x.Id_Cd == Id_Cd && x.Id_Acs == Id_Acs && x.Id_AcsVersion == Id_AcsVersion).FirstOrDefault();

            return ret;
        }


        public List<spCNac_DatosACYSCN_Result> ConsultaDatosACYSCN(string clienteSIAN)
        {

            var ret = model.spCNac_DatosACYSCN(clienteSIAN).ToList();
            return ret;
        }

        public Boolean EsFranquicia(int Cd)
        {
            var ret = model.spCNac_EsFranquicia(Cd).FirstOrDefault();

            if (ret == null) return false;
            else
                return ret.Value;
        }

    }
}
