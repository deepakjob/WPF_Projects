using EmployeeManagement.Commands;
using StudentWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudentWpfApp.ViewModels
{
    internal class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        EmployeeService ObjEmployeeService;
        public EmployeeViewModel()
        {
            ObjEmployeeService = new EmployeeService();
            LoadData();
            CurrentEmployees = new EmployeeDto();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<EmployeeDto> EmployeeList;
        public ObservableCollection<EmployeeDto> EmployeeLists
        {
            get { return EmployeeList; }
            set { EmployeeList = value; OnPropertyChanged("EmployeeLists"); }
        }
        private void LoadData()
        {
            EmployeeLists = new ObservableCollection<EmployeeDto>(ObjEmployeeService.GetAll());
        }
        #endregion
        #region SaveOperation
        private EmployeeDto CurrentEmployee;
        public EmployeeDto CurrentEmployees
        {
            get { return CurrentEmployee; }
            set { CurrentEmployee = value; OnPropertyChanged("CurrentEmployees"); }
        }
       
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        #region DeleteOperation
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }
        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeeService.Add(CurrentEmployees);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
               // Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeeService.Update(CurrentEmployees);
                if (IsUpdated)
                {
                    Message = "Employee updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region SearchOperation
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        public void Search()
        {
            try
            {
                var ObjStudent = ObjEmployeeService.Search(CurrentEmployees.Id);
                if (ObjStudent != null)
                {
                    CurrentEmployees = ObjStudent;
                }
                else
                {
                    Message = "Employee not found";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

       

        public void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeeService.Delete(CurrentEmployees.Id);
                if (IsDeleted)
                {
                    Message = "Employee deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

    }
}
