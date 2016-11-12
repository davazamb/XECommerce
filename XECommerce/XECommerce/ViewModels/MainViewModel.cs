using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XECommerce.ViewModels
{
    public class MainViewModel
    {

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public LoginViewModel NewLogin { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            NewLogin = new LoginViewModel();
            LoadMenu();
        }
        #endregion

        #region Methods
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
                Title = "Salir",
            });
        } 
        #endregion
    }
}
