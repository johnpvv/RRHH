using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_frmleftsiab : System.Web.UI.Page
{
    public int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        modFunciones modfunc = new modFunciones();
        Usuarios usr = new Usuarios();
        string gUsr;
        string asCodSistema;
        DataSet ds = new DataSet();

        if (!IsPostBack)
        {
            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();
                ds = usr.menuUser(Session["user"].ToString(), Session["codHosp"].ToString());

                if (Session["tipo"] != null)
                {
                    if (Session["tipo"].ToString() == "1")
                    {
                        //TreeView1.Visible = true;
                        TreeView2.Visible = false;
                        //TreeView5.Visible = false;
                        //TreeView3.Visible = false;
                        //TreeView4.Visible = false;
                    }
                    else if (Session["tipo"].ToString() == "3")
                    {
                        TreeView1.Visible = false;
                        TreeView2.Visible = false;
                        //TreeView5.Visible = false;
                        //TreeView3.Visible = true;
                        //TreeView4.Visible = false;
                    }
                    else
                    {
                        TreeView1.Visible = false;
                        TreeView3.Visible = false;

                        if (usr.mfEsSalud(Session["user"].ToString()) != "1")
                        {
                            if(modConstantes.mfConstante("MICRO") == "NO")
                            { 
                                TreeView2.Visible = true;
                                //TreeView5.Visible = false;
                            }
                            else
                            {
                                //TreeView2.Visible = false;
                                //TreeView5.Visible = true;
                                TreeView2.Visible = true;
                            }
                                

                            //TreeView4.Visible = false;
                        }
                        else
                        {
                            //TreeView2.Visible = false;
                            TreeView2.Visible = true;
                            //TreeView5.Visible = false;
                            //TreeView4.Visible = true;
                        }



                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>self.parent.location='Login.aspx';</script>");
                }

                buscarNodosUser(TreeView2.Nodes, ds);
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }


    }

    private void buscarNodosUser(TreeNodeCollection nodes, DataSet dataSet)
    {
        //revisa el arbol de nodos del menú, y recorre buscando los valores que coindiden con los permisos del ususario - JVV 12/12/2023
        Mensaje mens = new Mensaje();
        List<TreeNode> nodosAEliminar = new List<TreeNode>();

        foreach (TreeNode node in nodes)
        {
            bool encontrado = false;
            //recorre el DataSet para comparar los id de app con los valores de cada nodo
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string targetValue = row["CODIGO"].ToString();
                if (node.Value == targetValue || node.Value == "0")
                {
                    encontrado = true;
                    i++;
                    break;
                }
            }
            if (!encontrado && !nodoHijo(node, dataSet))
            {
                nodosAEliminar.Add(node);
            }

            if (node.ChildNodes.Count > 0)
            {
                buscarNodosUser(node.ChildNodes, dataSet);
            }
        }
        // Elimina los nodos que no estaban en el DataSet y no tienen hijos en el DataSet
        foreach (TreeNode nodeEliminar in nodosAEliminar)
        {
            if (nodeEliminar.Parent != null)
            {
                nodeEliminar.Parent.ChildNodes.Remove(nodeEliminar);
            }
            else
            {
                nodes.Remove(nodeEliminar);
            }
        }
        //if (i == 0)
        //{
        //    mens.mensaje(Page, "Usuario Sin Perfil, debe contactar al Administrador... ");
        //}
    }
    // Función para verificar si un nodo o alguno de sus hijos está en el DataSet
    private bool nodoHijo(TreeNode parentNode, DataSet dataSet)
    {
        foreach (TreeNode childNode in parentNode.ChildNodes)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string targetValue = row["CODIGO"].ToString();
                if (childNode.Value == targetValue || childNode.Value == "0")
                {
                    return true;
                }
            }
            if (childNode.ChildNodes.Count > 0 && nodoHijo(childNode, dataSet))
            {
                return true;
            }
        }
        return false;
    }
}