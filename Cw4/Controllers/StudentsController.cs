using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Cw4.Models;
using Cw4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsContriller : ControllerBase
    {
       
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19054;Integrated Security=True;";
        private IStudentsDal _dbservice;
       
        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            
            
            using (SqlConnection con = new SqlConnection(ConString))
                using(  SqlCommand com=new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = $"select IndexNumber,FirstName,LastName,BirthDate,Name,Semestr from Student inner join Enrollment on Student.IdEnrollment=Enrollment.IdEnrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy";
                      
                    
                    con.Open();
                   SqlDataReader d= com.ExecuteReader();
                   
                   while (d.Read())
                   
                   {
                       var st=new Student();
                       st.IndexNumber = d["IndexNumber"].ToString();
                       st.FirstName = d["FirstName"].ToString();
                       st.LastName = d["LastName"].ToString();
                      st.BirthDate = DateTime.Parse("BirthDate");
                      st.Name = d["Name"].ToString();
                      st.Semester = Int32.Parse("Semester");
                       list.Add(st);
                   }
                }
        
            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetWpisNaSem(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = $"select Name,Semestr,StartDate from Student inner join Enrollment on Student.IdEnrollment=Enrollment.IdEnrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy where indexnumber = @index ";
           
                com.Parameters.AddWithValue("index", indexNumber);
                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var sem=new Semestr();
                    
                    sem.Name = dr["IndexNumber"].ToString();
                    sem.Semester = Int32.Parse("Semester");
                  sem.StartDate = DateTime.Parse("StartDate");
                    return Ok(sem);
                }
            }
        

        return NotFound();
        }
/*
        [HttpGet("ex2")]
        public IActionResult GetStudents2()
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "APBD4";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Lastname","Kowalski");
                var dr = com.ExecuteReader();
                //...
            }

            return NotFound();
        }
        [HttpGet("ex3")]
        public IActionResult GetStudents3()
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "insert into Student(FirstName) values (@firstName)";

                
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    int affectedRows = com.ExecuteNonQuery();

                    com.CommandText = "update into ...";
                    com.ExecuteNonQuery();

                    //...
                    transaction.Commit();
                }catch(Exception exc)
                {
                    transaction.Rollback();
                }

            }

            return Ok();
        }
        */
    }
}