using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XECommerce.Models;
using XECommerce.Services;

namespace XECommerce.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private DataService dataService;
        private AppService appService;
        private NetService netService;
        private string productsFilter;
        private string customersFilter;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<ProductItemViewModel> Products { get; set; }
        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }
        public LoginViewModel NewLogin { get; set; }
        public UserViewModel UserLoged { get; set; }
        public CustomerItemViewModel CurrentCustomer { get; set; }

        public string ProductsFilter
        { 
        set
            {
                if (productsFilter != value)
                {
                    productsFilter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsFilter"));
                    if(string.IsNullOrEmpty(productsFilter))
                    {
                       LoadLocalProducts();
                    }
                }
            }
            get
            {
                return productsFilter;
            }
        }



        public string CustomersFilter
        {
            set
            {
                if (customersFilter != value)
                {
                    customersFilter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CustomersFilter"));
                    if (string.IsNullOrEmpty(customersFilter))
                    {
                        LoadLocalCustomers();
                    }
                }
            }
            get
            {
                return customersFilter;
            }
        }




        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Singleton
            instance = this;
            //Create observable colletions
            Menu = new ObservableCollection<MenuItemViewModel>();
            Products = new ObservableCollection<ProductItemViewModel>();
            Customers = new ObservableCollection<CustomerItemViewModel>();
            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            CurrentCustomer = new CustomerItemViewModel();
            //Instance services
            dataService = new DataService();
            appService = new AppService();
            netService = new NetService();
            //Load data
            LoadMenu();
            LoadProducts();
            LoadCustomers();
        }





        #endregion

        #region Singleton

        private static MainViewModel instance;


        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Commands
        public ICommand SearchProductCommand { get { return new RelayCommand(SearchProduct); } }
        public ICommand SearchCustomerCommand { get { return new RelayCommand(SearchCustomer); } }

        private void SearchProduct()
        {
            var products = dataService.GetProducts(ProductsFilter);
            ReloadProducts(products);
        }
        private void SearchCustomer()
        {
            var customers = dataService.GetCustomers(CustomersFilter);
            ReloadCustomers(customers);
        }



        #endregion

        #region Methods
        public void SetCurrentCustomer(CustomerItemViewModel customerItemViewModel)
        {
            CurrentCustomer = customerItemViewModel;
        }
        private void LoadLocalCustomers()
        {
            var customers = dataService.Get<Customer>(true);
            ReloadCustomers(customers);
        }
        private async void LoadCustomers()
        {
            var customers = new List<Customer>();
            if (netService.IsConnected())
            {
                customers = await appService.Get<Customer>("Customers");
                dataService.Save(customers);
            }
            else
            {
                customers = dataService.Get<Customer>(true);
            }
            ReloadCustomers(customers);
        }

        private void ReloadCustomers(List<Customer> customers)
        {
            Customers.Clear();
            foreach (var customer in customers.OrderBy(c => c.FirstName).ThenBy(c => c.LastName))
            {
                Customers.Add(new CustomerItemViewModel
                {
                    Address = customer.Address,
                    City = customer.City,
                    CityId = customer.CityId,
                    CompanyCustomers = customer.CompanyCustomers,
                    CustomerId = customer.CustomerId,
                    Department = customer.Department,
                    DepartamentId = customer.DepartamentId,
                    FirstName = customer.FirstName,
                    IsUpdated = customer.IsUpdated,
                    LastName = customer.LastName,
                    Latitude = customer.Latitude,
                    Longitude = customer.Longitude,
                    Orders = customer.Orders,
                    Phone = customer.Phone,
                    Photo = customer.Photo,
                    Sales = customer.Sales,
                    UserName = customer.UserName,
                   
                });
            }
        }

        private void LoadLocalProducts()
        {
            var products = dataService.Get<Product>(true);
            ReloadProducts(products);

        }
        private async void LoadProducts()
        {
            var products = new List<Product>();
            if (netService.IsConnected())
            {
                products = await appService.Get<Product>("Products");
                dataService.Save(products);
            }
            else
            {
                products = dataService.Get<Product>(true);
            }
            ReloadProducts(products);

        }

        private void ReloadProducts(List<Product> products)
        {
            Products.Clear();
            foreach (var product in products.OrderBy(p => p.Description))
            {
                Products.Add(new ProductItemViewModel
                {
                    BarCode = product.BarCode,
                    Category = product.Category,
                    CategoryId = product.CategoryId,
                    Company = product.Company,
                    CompanyId = product.CompanyId,
                    Description = product.Description,
                    Image = product.Image,
                    Inventories = product.Inventories,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Remarks = product.Remarks,
                    Stock = product.Stock,
                    Tax = product.Tax,
                    TaxId = product.TaxId,
                    OrderDetails = product.OrderDetails
                });
            }
        }

        public void LoadUser(User user)
        {
            UserLoged.FullName = user.FullName;
            UserLoged.Photo = user.PhotoFullPath;
        }
        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_product",
                PageName = "ProductsPage",
                Title = "Productos",
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_cliente",
                PageName = "CustomersPage",
                Title = "Clientes",
            }); Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_pedido",
                PageName = "OrdersPage",
                Title = "Pedidos",
            }); Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_entrega",
                PageName = "DeliveriesPage",
                Title = "Entregas",
            }); Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_sync",
                PageName = "SyncPage",
                Title = "Sincronizar",
            }); Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_configurar",
                PageName = "SetupPage",
                Title = "Configurar",
            }); Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_salir",
                PageName = "LogoutPage",
                Title = "Cerrar Sesion",
            });
        } 
        #endregion
    }
}
