using EmployeeManagement.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
namespace StudentWpfApp.Models
{
    public class EmployeeService
    {
        SqlConnection _connection;
        SqlCommand _command;
        MvvmDemoDbContext ObjContext;
        public EmployeeService()
        {
           _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvvmDemoDb"].ConnectionString);
            _command = new SqlCommand();
            _command.Connection = _connection;
            _command.CommandType = CommandType.StoredProcedure;
             ObjContext = new MvvmDemoDbContext();
        }
        public List<EmployeeDto> GetAll()
        {
            List<EmployeeDto> ObjEmployeeList = new List<EmployeeDto>();
            try
            {
                var ObjQuery = from obj in ObjContext.Employees
                               select obj;
                foreach (var employee in ObjQuery)
                {
                    ObjEmployeeList.Add(new EmployeeDto { Id = employee.Id, Name = employee.Name, Age = employee.Age });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjEmployeeList;
        }

        public bool Add(EmployeeDto objNewStudent)
        {
            bool IsAdded = false;
            //Age must be between 21 and 58
            if (objNewStudent.Age < 21 || objNewStudent.Age > 58)
                throw new ArgumentException("Invalid age limit for student");

            try
            {
                var ObjStudent = new Employee();
                ObjStudent.Id = objNewStudent.Id;
                ObjStudent.Name = objNewStudent.Name;
                ObjStudent.Age = objNewStudent.Age;

                ObjContext.Employees.Add(ObjStudent);
                var NoOfRowsAffected = ObjContext.SaveChanges();
                IsAdded = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return IsAdded;
        }

        public bool Update(EmployeeDto objStudentToUpdate)
        {
            bool IsUpdated = false;

            try
            {
                var ObjStudent = ObjContext.Employees.Find(objStudentToUpdate.Id);
                ObjStudent.Name = objStudentToUpdate.Name;
                ObjStudent.Age = objStudentToUpdate.Age;
                var NoOfRowsAffected = ObjContext.SaveChanges();
                IsUpdated = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsUpdated;
        }
        public EmployeeDto Search(int id)
        {
            EmployeeDto ObjStudent = null;

            try
            {
                var ObjEmployeeToFind = ObjContext.Employees.Find(id);
                if (ObjEmployeeToFind != null)
                {
                    ObjStudent = new EmployeeDto()
                    {
                        Id = ObjEmployeeToFind.Id,
                        Name = ObjEmployeeToFind.Name,
                        Age = ObjEmployeeToFind.Age
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjStudent;
        }
        public bool Delete(int id)
        {
            bool IsDeleted = false;
            try
            {
                var ObjEmployeeToDelete = ObjContext.Employees.Find(id);
                ObjContext.Employees.Remove(ObjEmployeeToDelete);
                var NoOfRowsAffected = ObjContext.SaveChanges();
                IsDeleted = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return IsDeleted;
        }

    }
}

