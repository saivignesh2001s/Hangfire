using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Unconnectedwebapi.Models;

namespace Unconnectedwebapi.Repository
{

    public class Usermethods : IUsermethods
    {
        private readonly IConfiguration configuration;
        string conn = null;
        SqlConnection cn = null;
        SqlDataAdapter ds = null;
        DataSet set = null;
        public Usermethods(IConfiguration configuration) {
            this.configuration = configuration;
            conn =this.configuration.GetSection("ConnectionStrings:default").Value;
            Console.WriteLine(conn);
            cn=new SqlConnection(conn);
            set=new DataSet();
        }
        public user GetUser(int id)
        {
                 user k1 = null;
                DataTable k = syncdata();
              foreach(DataRow row in k.Rows)
            {
                if ((int)row["id"] == id)
                {
                    k1 = new user()
                    {
                        id = Convert.ToInt32(row["id"]),
                        username = row["username"].ToString(),
                        useremail = row["useremail"].ToString(),
                        password = row["password"].ToString()

                    };

                }
            }
            return k1;

                
                       
            
        
        }
        public user postuser(user k)
        {
            try
            {
                DataTable pk = syncdata();
                DataRow dr = pk.NewRow();
                dr["id"] = k.id;
                dr["username"] = k.username;
                dr["password"] = k.password;
                dr["useremail"] = k.useremail;
                set.Tables[0].Rows.Add(dr);


                SqlCommandBuilder d = new SqlCommandBuilder(ds);
                int p = ds.Update(set.Tables[0]);
                if (p == 1)
                {
                    return k;
                }
                else
                {
                    return null;
                }
        }
            catch(Exception ex) 
            {
                Errorlog.Writelog(new string[] { ex.Message + " " + DateTime.Now });
                return null;
            }

        }
        public DataTable syncdata()
        {
                     
           
            ds= new SqlDataAdapter("Select * from users",cn);
            ds.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            ds.Fill(set);
            DataTable ts = set.Tables[0];
            return ts;
           
        }
        public bool updateuser(int id,user user)
        {
            try
            {
                DataTable k = syncdata();
                DataRow t = set.Tables[0].Rows.Find(id);
                if (t != null)
                {
                    t["id"] = Convert.ToInt32(user.id);
                    t["username"] = user.username;
                    t["password"] = user.password;
                    t["useremail"] = user.useremail;
                    SqlCommandBuilder build = new SqlCommandBuilder(ds);
                    int p = ds.Update(set.Tables[0]);
                    if (p == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex) 
            {
                Errorlog.Writelog(new string[] { ex.Message + " " + DateTime.Now });
                return false;
            }
        }
        public bool deleteuser(int id)
        {
            try
            {
                DataTable k = syncdata();
                DataRow dr = k.Rows.Find(id);
                if (dr != null)
                {
                    dr.Delete();
                    SqlCommandBuilder builder1 = new SqlCommandBuilder(ds);
                    int ks = ds.Update(set.Tables[0]);
                    if (ks == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Errorlog.Writelog(new string[] { ex.Message+" "+DateTime.Now});
                return false;
            }
        
        
        }
        public void sendmail(string method, int? id)
        {
            if (method.ToLower() == "get" && id != null)
                Console.WriteLine($"{id} is retrieved");
            if(method.ToLower() == "post" && id != null)
                Console.WriteLine($"{id} is inserted");
            if (method.ToLower() == "delete" && id != null)
                Console.WriteLine($"{id} is deleted");
            if (method.ToLower() == "put" && id != null)
                Console.WriteLine($"{id} is updated");
            if (method.ToLower() == "get" && id == null)
                Console.WriteLine("All users are retrieved");
        }
        public List<user> GetUsers() { 
            DataTable dt=syncdata();
            if (dt != null)
            {
                return (from DataRow d in dt.Rows
                        select new user()
                        {
                            id = Convert.ToInt32(d["id"]),
                            username = d["username"].ToString(),
                            useremail = d["useremail"].ToString(),
                            password = d["password"].ToString()

                        }).ToList();
            }
            else
            {
                return null;
            }
        }
    }
    
}
