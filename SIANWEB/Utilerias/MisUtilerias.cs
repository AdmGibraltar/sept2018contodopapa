using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace SIANWEB.CuentasCorporativas
{
    public class AsignacionCampos
    {

        public static void AsignaCamposForma(ref object entidad, String Prefijo, Control ctrl)
        {
            var properties = entidad.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {

                Control control = null;

                control = (Control)BuscarControl(ctrl, "txt" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadTextBox":
                            if (property.GetValue(entidad, null) != null)
                                ((RadTextBox)control).Text = property.GetValue(entidad, null).ToString();
                            break;

                        case "RadNumericTextBox":
                            if (property.GetValue(entidad, null) != null)
                                ((RadNumericTextBox)control).Text = property.GetValue(entidad, null).ToString();
                            break;

                        case "TextBox":
                            if (property.GetValue(entidad, null) != null)
                                ((TextBox)control).Text = property.GetValue(entidad, null).ToString();
                            break;


                        case "Label":
                            if (property.GetValue(entidad, null) != null)
                                ((Label)control).Text = property.GetValue(entidad, null).ToString();
                            break;


                        default:
                            control = null;
                            break;
                    }

                }

                control = (Control)BuscarControl(ctrl, "chk" + Prefijo + property.Name);
                if (control != null)
                {

                    switch (control.GetType().Name)
                    {
                        case "CheckBox":
                            if (property.GetValue(entidad, null) != null)
                                ((CheckBox)control).Checked = Boolean.Parse(property.GetValue(entidad, null).ToString());
                            break;

                        default:
                            break;
                    }
                }

                control = (Control)BuscarControl(ctrl, "rd" + Prefijo + property.Name);
                if (control != null)
                {

                    switch (control.GetType().Name)
                    {
                        case "RadDatePicker":
                            if (property.GetValue(entidad, null) != null)
                                ((RadDatePicker)control).SelectedDate = DateTime.Parse(property.GetValue(entidad, null).ToString());
                            break;

                        default:
                            break;
                    }
                }



                control = (Control)BuscarControl(ctrl, "tp" + Prefijo + property.Name);
                if (control != null)
                {

                    switch (control.GetType().Name)
                    {
                        case "RadTimePicker":
                            if (property.GetValue(entidad, null) != null)
                                ((RadTimePicker)control).SelectedDate = DateTime.Parse(property.GetValue(entidad, null).ToString());
                            break;

                        default:
                            break;
                    }
                }

                control = (Control)BuscarControl(ctrl, "cmb" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadComboBox":
                            if (property.GetValue(entidad, null) != null)
                                ((RadComboBox)control).SelectedValue = property.GetValue(entidad, null).ToString();
                            break;

                        default:
                            break;
                    }

                }


                control = (Control)BuscarControl(ctrl, "csb" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadComboBox":
                            if (property.GetValue(entidad, null) != null)
                                ((RadComboBox)control).SelectedValue = property.GetValue(entidad, null).ToString();
                            break;

                        default:
                            break;
                    }

                }




                //control=property.GetValue(entidad, null);
            }
        }

        public static void AsignaCamposEntidad(ref object entidad, String Prefijo, Control ctrl)
        {

            var properties = entidad.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                Control control = null;

                control = (Control)BuscarControl(ctrl, "txt" + Prefijo + property.Name);
                if (control != null)
                    switch (control.GetType().Name)
                    {
                        case "RadTextBox":
                            if (((RadTextBox)control).Text != "")
                                property.SetValue(entidad, ((RadTextBox)control).Text, null);
                            break;

                        case "RadNumericTextBox":
                            if (((RadNumericTextBox)control).Text != "")
                                property.SetValue(entidad, Int32.Parse(((RadNumericTextBox)control).Text), null);
                            break;

                        case "TextBox":
                            if (((TextBox)control).Text != "")
                                property.SetValue(entidad, ((TextBox)control).Text, null);
                            break;

                        case "Label":
                            if (((Label)control).Text != "")
                                property.SetValue(entidad, ((Label)control).Text, null);
                            break;



                        default:
                            control = null;
                            break;
                    }


                control = (Control)BuscarControl(ctrl, "dbl" + Prefijo + property.Name);
                if (control != null)
                    switch (control.GetType().Name)
                    {


                        case "RadNumericTextBox":
                            if (((RadNumericTextBox)control).Text != "")
                                property.SetValue(entidad, Double.Parse(((RadNumericTextBox)control).Text), null);
                            break;


                        default:
                            control = null;
                            break;
                    }


                control = (Control)BuscarControl(ctrl, "chk" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "CheckBox":
                            property.SetValue(entidad, ((CheckBox)control).Checked, null);
                            break;

                        case "RadioButton":
                            property.SetValue(entidad, ((RadioButton)control).Checked, null);
                            break;

                        case "RadComboBox":
                            property.SetValue(entidad, ((RadComboBox)control).SelectedValue, null);
                            break;

                        default:
                            break;
                    }
                }


                control = (Control)BuscarControl(ctrl, "rd" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadDatePicker":
                            property.SetValue(entidad, ((RadDatePicker)control).SelectedDate, null);
                            break;

                        default:
                            break;
                    }
                }


                control = (Control)BuscarControl(ctrl, "tp" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadTimePicker":
                            if (((RadTimePicker)control).SelectedDate != null)
                                property.SetValue(entidad, ((RadTimePicker)control).SelectedDate.Value.ToString("hh:mm"), null);
                            break;

                        default:
                            break;
                    }
                }

                control = (Control)BuscarControl(ctrl, "cmb" + Prefijo + property.Name);
                if (control != null)
                {
                     switch (control.GetType().Name)
                    {
                        case "RadComboBox":
                            if (((RadComboBox)control).SelectedValue != "")
                                property.SetValue(entidad, Int32.Parse(((RadComboBox)control).SelectedValue), null);
                            break;

                        default:
                            break;
                    }

                }

                control = (Control)BuscarControl(ctrl, "csb" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadComboBox":
                            if (((RadComboBox)control).SelectedValue != "")
                                property.SetValue(entidad, ((RadComboBox)control).SelectedValue, null);
                            break;

                        default:
                            break;
                    }

                }


            }


        }

        public static void AsignaCamposEntidad(ref object entidad, String Prefijo, GridItem item, Control ctrl)
        {

            var properties = entidad.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                Control control = null;

                control = (Control)BuscarControl(item, "txt" + Prefijo + property.Name);
                if (control != null)
                    switch (control.GetType().Name)
                    {
                        case "RadTextBox":
                            if (((RadTextBox)control).Text != "")
                                property.SetValue(entidad, ((RadTextBox)control).Text, null);
                            break;

                        case "RadNumericTextBox":
                            if (((RadNumericTextBox)control).Text != "")
                                property.SetValue(entidad, Int32.Parse(((RadNumericTextBox)control).Text), null);
                            break;

                        case "TextBox":
                            if (((TextBox)control).Text != "")
                                property.SetValue(entidad, ((TextBox)control).Text, null);
                            break;

                        case "Label":
                            if (((Label)control).Text != "")
                                property.SetValue(entidad, ((Label)control).Text, null);
                            break;

                        default:
                            control = null;
                            break;
                    }


                control = (Control)BuscarControl(item, "dbl" + Prefijo + property.Name);
                if (control != null)
                    switch (control.GetType().Name)
                    {
                        case "RadNumericTextBox":
                            if (((RadNumericTextBox)control).Text != "")
                                property.SetValue(entidad, Double.Parse(((RadNumericTextBox)control).Text), null);
                            break;

                        default:
                            control = null;
                            break;
                    }


                control = (Control)BuscarControl(ctrl, "chk" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "CheckBox":
                            property.SetValue(entidad, ((CheckBox)control).Checked, null);
                            break;

                        case "RadioButton":
                            property.SetValue(entidad, ((RadioButton)control).Checked, null);
                            break;

                        case "RadComboBox":
                            property.SetValue(entidad, ((RadComboBox)control).SelectedValue, null);
                            break;

                        default:
                            break;
                    }
                }


                control = (Control)BuscarControl(ctrl, "rd" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadDatePicker":
                            property.SetValue(entidad, ((RadDatePicker)control).SelectedDate, null);
                            break;

                        default:
                            break;
                    }
                }


                control = (Control)BuscarControl(ctrl, "tp" + Prefijo + property.Name);
                if (control != null)
                {
                    switch (control.GetType().Name)
                    {
                        case "RadTimePicker":
                            if (((RadTimePicker)control).SelectedDate != null)
                                property.SetValue(entidad, ((RadTimePicker)control).SelectedDate.Value.ToString("hh:mm"), null);
                            break;

                        default:
                            break;
                    }
                }

            }


        }

        public static Control BuscarControl(Control ctrl, String idCtrl)
        {
            Control ctrlRetornar = null;

            if (ctrl.FindControl(idCtrl) != null) ctrlRetornar = ctrl.FindControl(idCtrl);
            else
            {

                foreach (Control c in ctrl.Controls)
                {
                    if (c.HasControls())
                    {
                        ctrlRetornar = BuscarControl(c, idCtrl);
                        if (ctrlRetornar != null) break;
                    }

                }
            }

            if (ctrlRetornar == null)
                return null;
            else
                return ctrlRetornar;
        }

        public static Control BuscarControl(GridItem ctrl, String idCtrl)
        {
            Control ctrlRetornar = null;

            if (ctrl.FindControl(idCtrl) != null) ctrlRetornar = ctrl.FindControl(idCtrl);
            else
            {

                foreach (Control c in ctrl.Controls)
                {
                    if (c.HasControls())
                    {
                        ctrlRetornar = BuscarControl(c, idCtrl);
                        if (ctrlRetornar != null) break;
                    }

                }
            }

            if (ctrlRetornar == null)
                return null;
            else
                return ctrlRetornar;
        }


        public static void DesactivarControles(Control ctrl, String idCtrl)
        {
                foreach (Control c in ctrl.Controls)
                {
                    if (c.HasControls())
                    {
                        DesactivarControles(c, idCtrl);
                    }
                    else
                    {
                        try
                        {
                            ((WebControl)c).Enabled = false;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
        }



    }
}