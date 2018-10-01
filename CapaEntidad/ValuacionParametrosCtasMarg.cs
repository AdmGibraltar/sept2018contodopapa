using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class ValuacionParametrosCtasMarg
    {
        int _Id_Emp;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        int _Id_Cd;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        int _Id_Vap;

        public int Id_Vap
        {
            get { return _Id_Vap; }
            set { _Id_Vap = value; }
        }
       
        double _Vap_Mano_Obra;

        public double Vap_Mano_Obra
        {
            get { return _Vap_Mano_Obra; }
            set { _Vap_Mano_Obra = value; }
        }
        
        double _Vap_Gasto_Flete_Locales;

        public double Vap_Gasto_Flete_Locales
        {
            get { return _Vap_Gasto_Flete_Locales; }
            set { _Vap_Gasto_Flete_Locales = value; }
        }
        double _Vap_IVA;

        public double Vap_IVA
        {
            get { return _Vap_IVA; }
            set { _Vap_IVA = value; }
        }
        double _Vap_Dias_Cuentas_por_Cobrar;

        public double Vap_Dias_Cuentas_por_Cobrar
        {
            get { return _Vap_Dias_Cuentas_por_Cobrar; }
            set { _Vap_Dias_Cuentas_por_Cobrar = value; }
        }
        int _Vap_Inventario_Key;

        public int Vap_Inventario_Key
        {
            get { return _Vap_Inventario_Key; }
            set { _Vap_Inventario_Key = value; }
        }     
        int _Vap_Credito_Key;

        public int Vap_Credito_Key
        {
            get { return _Vap_Credito_Key; }
            set { _Vap_Credito_Key = value; }
        }
       
        double _Vap_ISR;

        public double Vap_ISR
        {
            get { return _Vap_ISR; }
            set { _Vap_ISR = value; }
        }        
        double _Vap_Cetes;

        public double Vap_Cetes
        {
            get { return _Vap_Cetes; }
            set { _Vap_Cetes = value; }
        }
        double _Vap_Adicional_Cetes;

        public double Vap_Adicional_Cetes
        {
            get { return _Vap_Adicional_Cetes; }
            set { _Vap_Adicional_Cetes = value; }
        }
        double _Vap_Contribucion_Costos_Fijos;

        public double Vap_Contribucion_Costos_Fijos
        {
            get { return _Vap_Contribucion_Costos_Fijos; }
            set { _Vap_Contribucion_Costos_Fijos = value; }
        }        
        double _Vap_Gastos_Admin;

        public double Vap_Gastos_Admin
        {
            get { return _Vap_Gastos_Admin; }
            set { _Vap_Gastos_Admin = value; }
        }
        int _Vap_Inversion_Activos;

        public int Vap_Inversion_Activos
        {
            get { return _Vap_Inversion_Activos; }
            set { _Vap_Inversion_Activos = value; }
        }
        double _Vap_Otros_Gastos_Variable;

        public double Vap_Otros_Gastos_Variable
        {
            get { return _Vap_Otros_Gastos_Variable; }
            set { _Vap_Otros_Gastos_Variable = value; }
        }
    }
}
