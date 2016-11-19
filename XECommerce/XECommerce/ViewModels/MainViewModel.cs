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
        private string filter;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<ProductItemViewModel> Products { get; set; }
        public LoginViewModel NewLogin { get; set; }
        public UserViewModel UserLoged { get; set; }
        public string Filter
        { 
        set
            {
                if (filter != value)
                {
                    filter = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
                        if(string.IsNullOrEmpty(filter))
                        {
                            LoadLocalProducts();
                        }
                    }
}
            }
            get
            {
                return filter;
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
            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            //Instance services
            dataService = new DataService();
            appService = new AppService();
            netService = new NetService();
            //Load data
            LoadMenu();
            LoadProducts();
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

        private void SearchProduct()
        {
            var products = dataService.GetProducts(Filter);
            Products.Clear();
            foreach (var product in products)
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
                });
            }
        }

        #endregion

        #region Methods
        private void LoadLocalProducts()
        {
            var products = dataService.GetProducts();
            ReloadProducts(products);

        }
        private async void LoadProducts()
        {
            var products = new List<Product>();
            if (netService.IsConnected())
            {
                products = await appService.GetProducts();
                dataService.SaveProducts(products);
            }
            else
            {
                products = dataService.GetProducts();
            }
            ReloadProducts(products);

        }

        private void ReloadProducts(List<Product> products)
        {
            Products.Clear();
            foreach (var product in products)
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
                    TaxId = product.TaxId
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
