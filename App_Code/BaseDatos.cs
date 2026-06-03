using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de BaseDatos
/// </summary>
public class BaseDatos
{
    public BaseDatos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //

    }

    public string EjecutarComando(System.Data.SqlClient.SqlConnection con, string sSql)
    {
        string lsRet = "";

        try
        {
            SqlCommand com = new SqlCommand(sSql, con);
            com.ExecuteNonQuery();

        }
        catch (Exception e)
        {
            lsRet = e.Message;
        }
        //con.Close();
        return lsRet;
    }

    public System.Data.SqlClient.SqlConnection fnGetConn()
    {
        System.Data.SqlClient.SqlConnection con;
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            con = new SqlConnection(connStr);
            con.Open();
        }
        catch (Exception e)
        {
            con = null;
        }
        return con;
    }

    public System.Data.SqlClient.SqlConnection fnGetConnRH()
    {
        System.Data.SqlClient.SqlConnection con;
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConRH"].ConnectionString;
            con = new SqlConnection(connStr);
            con.Open();
        }
        catch (Exception e)
        {
            con = null;
        }
        return con;
    }

    public DataSet Fill(System.Data.SqlClient.SqlConnection con, string sql)
    {

        try
        {
            // Conexion
            //System.Data.SqlClient.SqlConnection con = null;
            BaseDatos db = new BaseDatos();
            //con = db.fnGetConn();

            SqlCommand cm = new SqlCommand();

            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet(); ;

            cm.CommandText = sql;
            cm.CommandTimeout = 300;
            cm.CommandType = CommandType.Text;
            cm.Connection = con;


            da.Fill(ds);
            //con.Close();
            return ds;
        }
        catch (Exception e)
        {
            return null;

        }

    }

    public string ExecuteScalar(System.Data.SqlClient.SqlConnection con, string sql)
    {

        // Conexion
        //System.Data.SqlClient.SqlConnection con = null;
        BaseDatos db = new BaseDatos();
        string lsCod;
        try
        {
            //con = db.fnGetConn();
            SqlCommand cmd = new SqlCommand(sql, con);


            lsCod = cmd.ExecuteScalar().ToString();
            //con.Close();
        }
        catch (Exception e)
        {
            lsCod = "";

        }
        //con.Close();

        return lsCod;

    }



    public string ExecuteLista(System.Data.SqlClient.SqlConnection con, StringCollection myCol)
    {
        string lsCod = "";


        SqlCommand command = con.CreateCommand();
        SqlTransaction transaction;

        // Start a local transaction.
        transaction = con.BeginTransaction("SampleTransaction");

        // Must assign both transaction object and connection
        // to Command object for a pending local transaction
        command.Connection = con;
        command.Transaction = transaction;
        try
        {


            foreach (Object obj in myCol)
            {
                command.CommandText = obj.ToString();
                command.ExecuteNonQuery();
            }


            // Attempt to commit the transaction.
            transaction.Commit();
            Console.WriteLine("Both records are written to database.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
            lsCod = ex.Message;
            // Attempt to roll back the transaction.
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred
                // on the server that would cause the rollback to fail, such as
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
                lsCod = ex.Message;
            }
        }





        return lsCod;

    }


}