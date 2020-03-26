using System.Collections.Generic;
using System.Data.SqlClient;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsContriller : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19054;Integrated Security=True";
        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            using (SqlConnection con = new SqlConnection(ConString))
                using(  SqlCommand com=new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select * from Student";
                      
                    
                    con.Open();
                   SqlDataReader d= com.ExecuteReader();
                   while (d.Read())
                   {
                       var st=new Student();
                       st.IndexNumber = d["IndexNumber"].ToString();
                       st.FirstName = d["Firstname"].ToString();
                       st.LastName = d["LastName"].ToString();
                       list.Add(st);
                   }
                }
         //  con.Open();
         //  con.Dispose();
            return Ok(list);
        }
    }
}