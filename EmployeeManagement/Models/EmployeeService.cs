using EmployeeManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWpfApp.Models
{
    public class EmployeeService
    {
        MvvmDemoDbContext ObjContext;
      
        public EmployeeService()
        {
            ObjContext = new MvvmDemoDbContext();
            var version = ConfigurationManager.ConnectionStrings["MvvmDemoDb"].ConnectionString;
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

