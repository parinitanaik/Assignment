using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace Assignment2
{
    public class Module:IHttpModule
    {
        public void Dispose() { }
        public void Init(HttpApplication application)
        {
            application.PostRequestHandlerExecute += (new EventHandler(this.begin));
        }
        public void begin(Object obj,EventArgs e)
        {
            


            HttpApplication application= (HttpApplication)obj;
            HttpContext context=application.Context;
            try
            {
                if (context.Request.QueryString.Count > 0)
                {
                    string username = context.Request.QueryString[0];
                    string password = context.Request.QueryString[1];
                    HttpApplication app = (HttpApplication)obj;
                     string ss= app.Session["mysession"].ToString();
                    string s = context.Session["mysession"].ToString();

                    


                    SqlConnection con = new SqlConnection("Data Source=PNEITSH53358D;Initial Catalog=local;Integrated Security=true");
                    SqlCommand cmd = new SqlCommand("select * from UserData where UserName='" + username + "' and Password='" + password + "'", con);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        context.Response.Redirect("Default.aspx");
                        dr.Close();
                    }
                    else
                    {
                        dr.Close();
                        SqlCommand cmd1 = new SqlCommand("insert into UserData values('" + username + "','" + password + "')", con);
                        cmd1.ExecuteNonQuery();
                        context.Response.Redirect("Welcome.aspx");
                    }
                    con.Close();
                }
               
            }
            catch (Exception exp)
            { 
            
            }
        }
    }
}
