using System.Net.Mail;

/// <summary>
/// Descripción breve de Correo
/// </summary>
public class Correo
{
    public Correo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string EnviarCorreo(string para, string asunto, string copia, string cuerpo, string desde, string file)
    {
        /*-------------------------MENSAJE DE CORREO----------------------*/
        string sal = "0";
        //Creamos un nuevo Objeto de mensaje
        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

        //Direccion de correo electronico a la que queremos enviar el mensaje
        mmsg.To.Add(para);

        //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

        //Asunto
        mmsg.Subject = asunto;
        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

        //Direccion de correo electronico que queremos que reciba una copia del mensaje
        //mmsg.Bcc.Add(copia); //Opcional

        //Cuerpo del Mensaje
        mmsg.Body = cuerpo;
        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
        mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

        //Correo electronico desde la que enviamos el mensaje
        mmsg.From = new System.Net.Mail.MailAddress(desde);


        // Create  the file attachment for this e-mail message.
        Attachment data = new Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet);
        // Add time stamp information for the file.
        System.Net.Mime.ContentDisposition disposition = data.ContentDisposition;
        disposition.CreationDate = System.IO.File.GetCreationTime(file);
        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

        mmsg.Attachments.Add(data);


        /*-------------------------CLIENTE DE CORREO----------------------*/

        //Creamos un objeto de cliente de correo
        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

        //Hay que crear las credenciales del correo emisor
        cliente.Credentials =
            new System.Net.NetworkCredential("", "");

        //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
        /*
        cliente.Port = 587;
        cliente.EnableSsl = true;
        */

        cliente.Host = "ASPMX.L.GOOGLE.COM"; //Para Gmail "smtp.gmail.com";


        /*-------------------------ENVIO DE CORREO----------------------*/

        try
        {
            //Enviamos el mensaje      
            cliente.Send(mmsg);
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            //Aquí gestionamos los errores al intentar enviar el correo
            sal = "1";
        }

        return sal;

    }

    public string EnviarCorreo(string para, string asunto, string copia, string cuerpo, string desde)
    {
        /*-------------------------MENSAJE DE CORREO----------------------*/
        string sal = "0";
        //Creamos un nuevo Objeto de mensaje
        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

        //Direccion de correo electronico a la que queremos enviar el mensaje
        mmsg.To.Add(para);

        //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

        //Asunto
        mmsg.Subject = asunto;
        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

        //Direccion de correo electronico que queremos que reciba una copia del mensaje
        //mmsg.Bcc.Add(copia); //Opcional

        //Cuerpo del Mensaje
        mmsg.Body = cuerpo;
        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
        mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

        //Correo electronico desde la que enviamos el mensaje
        mmsg.From = new System.Net.Mail.MailAddress(desde);


        /*-------------------------CLIENTE DE CORREO----------------------*/

        //Creamos un objeto de cliente de correo
        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

        //Hay que crear las credenciales del correo emisor
        cliente.Credentials =
            new System.Net.NetworkCredential("", "");

        //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
        /*
        cliente.Port = 587;
        cliente.EnableSsl = true;
        */

        cliente.Host = "ASPMX.L.GOOGLE.COM"; //Para Gmail "smtp.gmail.com";


        /*-------------------------ENVIO DE CORREO----------------------*/

        try
        {
            //Enviamos el mensaje      
            cliente.Send(mmsg);
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            //Aquí gestionamos los errores al intentar enviar el correo
            sal = "1";
        }

        return sal;

    }

}