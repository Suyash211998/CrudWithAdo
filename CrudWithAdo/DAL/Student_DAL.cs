using CrudWithAdo.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CrudWithAdo.DAL
{
    public class Student_DAL
    {
        SqlConnection connection = null;
        SqlCommand command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json");

            Configuration= builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");

        }

        public List<Student> GetAll()
        {
           List<Student> studentList = new List<Student>();
            using(connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].getStudents";
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while(dr.Read())
                {
                    Student student= new Student();
                    student.Id = dr.GetInt32("Id");
                    student.FirstName = dr.GetString("FirstName");
                    student.LastName = dr.GetString("LastName");
                    student.City = dr.GetString("City");
                    student.MobileNo = dr.GetString("MobileNo");

                    studentList.Add(student);

                }
                connection.Close();
            }
            return studentList;
        }

        public bool Insert(Student model)
        {
            int id = 0;
            using(connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].InsertStudent";
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@City", model.City);
                command.Parameters.AddWithValue("@MobileNo", model.MobileNo);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            return id>0 ? true : false;
        }


        public Student GetById(int id)
        {
            Student student = new Student();
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].GetStudentById";
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    
                    student.Id = dr.GetInt32("Id");
                    student.FirstName = dr.GetString("FirstName");
                    student.LastName = dr.GetString("LastName");
                    student.City = dr.GetString("City");
                    student.MobileNo = dr.GetString("MobileNo");

                    

                }
                connection.Close();
            }
            return student;
        }

        public bool Update(Student model)
        {
            int id = 0;
            using (connection = new SqlConnection(GetConnectionString()))
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].UpdateStudent";
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@City", model.City);
                command.Parameters.AddWithValue("@MobileNo", model.MobileNo);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            return id > 0 ? true : false;
        }


    }
}
