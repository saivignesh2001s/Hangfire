using System.Data;
using Unconnectedwebapi.Models;

namespace Unconnectedwebapi.Repository
{
    public interface IUsermethods
    {
        DataTable syncdata();
        List<user> GetUsers();
        user GetUser(int id);
        user postuser(user k);
        void sendmail(string method,int? id);
        bool updateuser(int id,user user);
        bool deleteuser(int id);    
    }
}
