using LabCourse_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LabCourse_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradaShkolloreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GradaShkolloreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                    select IDgrada, Grada  
                    from GradaShkollore     
                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerActivitiesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]

        public JsonResult Post(GradaShkollore gr)
        {
            string query = @"
                    insert into GradaShkollore
                    values (@Grada)
                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerActivitiesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Grada", gr.Grada);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Added Successfully!");
        }




        [HttpPut]
        public JsonResult Put(GradaShkollore gr){ 
        
            string query = @"
                    update  GradaShkollore
                    set Grada = @Grada
                    where IDgrada = @IDgrada;
                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerActivitiesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@IDgrada", gr.IDgrada);
                    myCommand.Parameters.AddWithValue("@Grada", gr.Grada);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Updated Successfully!");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from  GradaShkollore
                    where IDgrada = @IDgrada;
                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerActivitiesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IDgrada", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Deleted Successfully!");
        }


    }
}
