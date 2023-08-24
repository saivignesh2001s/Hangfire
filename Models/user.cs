using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Web.Helpers;

namespace Unconnectedwebapi.Models
{
    public class user
    {
        public int id
        {
            get;set;
        }
        public string username
        {
            get;set;
        }
        public string useremail
        {
            get;set;
        }
        public string password
        {
            get;set;
        }
    }
    public class CustomException
    {
        public int statuscode
        {
            get; set;
        }
        public string message
        {
            get;set;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
           
        }
    }

    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            CustomException cs = new CustomException()
            {
                statuscode = 500,
                message="Internal server Error"
                
            };
            context.Result =new JsonResult(cs);
        }
    }
    public static class Errorlog
    {
        public static void Writelog(string[] k)
        {
            try
            {
                List<string> list = new List<string>();
                string[] read = File.ReadAllLines("read.txt");
                if (read != null)
                {
                    for(int i=0; i < read.Length; i++)
                    {
                        list.Add(read[i]);
                    }
                    for (int i = 0; i < k.Length; i++)
                    {
                        list.Add(k[i]);
                    }
                   

                    File.WriteAllLines("read.txt", list);
                }
            }
            catch
            {
                File.WriteAllLines("read.txt", k);

            }

           
        }
    }
}
