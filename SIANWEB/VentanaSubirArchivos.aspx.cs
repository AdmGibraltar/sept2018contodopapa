using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace SIANWEB
{
    public partial class VentanaSubirArchivos : System.Web.UI.Page
    {
       private string tempPath = @"~/uploads/temp";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdAddFile_Click(object sender, EventArgs e)
        {
            FileUpload f = fUpload;

            // No se hace nada si no hay fichero
            if (!f.HasFile)
                return;

            // Se crea un Item para el ListBox
            //  - Value: Nombre del fichero
            //  - Text : Texto para mostrar
            ListItem item = new ListItem();
            item.Value = f.FileName;
            item.Text = f.FileName +
                        " (" + f.FileContent.Length.ToString("N0") +
                        " bytes).";

            // Se sube el fichero a la carpeta temporal
            f.SaveAs(Server.MapPath(Path.Combine(tempPath, item.Value)));

            // Se deja el nombre del fichero en el ListBox
            lstFiles.Items.Add(item);
        }

        protected void cmdDelFile_Click(object sender, EventArgs e)
        {
            ListBox lb = lstFiles;
            // Se comprueba que exista algún item seleccionado
            if (lb.SelectedItem == null)
                return;

            // Se elimina el fichero seleccionado
            borraEntrada(lb.SelectedItem.Value);
        }

        protected void cmdSendMail_Click(object sender, EventArgs e)
        {
            enviaCorreo();
        } 
        
        /// <summary>
        /// Elimina el fichero de la carpeta temporal y del ListBox.
        /// </summary>
        /// <param name="fileName"></param>
        private void borraEntrada(string fileName)
        {
            string fichero = Server.MapPath(Path.Combine(tempPath, fileName));
            File.Delete(fichero);

            ListItem l = lstFiles.Items.FindByValue(fileName);
            if (l != null)
                lstFiles.Items.Remove(l);
        }

        /// <summary>
        /// Envía el correo electrónico.
        /// Los datos de configuración del servidor de correo SMTP se configuran 
        /// en el fichero web.config.
        /// </summary>
        private void enviaCorreo()
        {
            using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
            {
                // Dirección de destino
                message.To.Add("raul.borquez@gibraltar.com.mx");
                // Asunto 
                message.Subject = "Envio de documentos";
                // Mensaje 
                message.Body ="Documentos para actualización de ordenes de compra";

                // Se recuperan los ficheros
                foreach (ListItem l in lstFiles.Items)
                {
                    // Lectura del nombre del fichero
                    string fichero = Server.MapPath(Path.Combine(tempPath, l.Value));

                    // Adjuntado del fichero a la colección Attachments
                    message.Attachments.Add(new System.Net.Mail.Attachment(fichero));
                }

                // Se envía el mensaje y se informa al usuario
                System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
                string mensaje = string.Empty;
                try
                {
                    smpt.Send(message);
                    mensaje = "Correo enviado con éxito";
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                resultado.Text = mensaje;
            }

            // Se borran los ficheros de la carpeta temporal
            while (lstFiles.Items.Count > 0)
            {
                borraEntrada(lstFiles.Items[0].Value);
            }
        }
    }
}