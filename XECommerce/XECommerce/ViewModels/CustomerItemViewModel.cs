using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XECommerce.Models;
using XECommerce.Services;

namespace XECommerce.ViewModels
{
    public class CustomerItemViewModel : Customer
    {
        #region Attributes
        private NavigationService navigationService;
        private NetService netService;
        private AppService appService;
        private DataService dataService;

        #endregion
        #region Properties
        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }
        #endregion

        #region Contructors
        public CustomerItemViewModel()
        {
            //Services
            navigationService = new NavigationService();
            netService = new NetService();
            appService = new AppService();
            dataService = new DataService();

            //Observable collection
            Departments = new ObservableCollection<DepartmentItemViewModel>();
            //LoadData
            LoadDepartments();
        }


        #endregion

        #region Commands
        public ICommand CustomerDetailCommand { get { return new RelayCommand(CustomerDetail); } }

        private async void CustomerDetail()
        {
            var customerItemViewModel = new CustomerItemViewModel
            {
                Address = Address,
                City = City,
                CityId = CityId,
                CompanyCustomers = CompanyCustomers,
                CustomerId = CustomerId,
                Department = Department,                         
                DepartamentId = DepartamentId,
                FirstName = FirstName,
                IsUpdated = IsUpdated,
                LastName = LastName,
                Latitude = Latitude,
                Longitude = Longitude,
                Orders = Orders,
                Phone = Phone,
                Photo = Photo,
                Sales = Sales,
                UserName = UserName,
               
            };
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentCustomer(customerItemViewModel);
            await navigationService.Navigate("CustomerDetailPage");
        }
        #endregion

        #region Methods
        private async void LoadDepartments()
        {
            var departments = new List<Department>();
            if (netService.IsConnected())
            {
                departments = await appService.GetDepartments();
                dataService.Save(departments);
            }
            else
            {
                departments = dataService.GetDepartments();
            }
            ReloadDepartments(departments);
        }                                                            
                                                     

        private void ReloadDepartments(List<Department> departments)
        {
            Departments.Clear();
            foreach (var department in departments.OrderBy(d => d.Name))
            {
                Departments.Add(new DepartmentItemViewModel
                {
                    Cities = department.Cities,
                    Customers = department.Customers,
                    DepartamentId = department.DepartamentId,
                    Name = department.Name
                });
            }
        }
        #endregion
    }
}
