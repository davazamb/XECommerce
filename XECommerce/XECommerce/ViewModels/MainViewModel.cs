﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XECommerce.Models;
using XECommerce.Services;

namespace XECommerce.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private DataService dataService;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public LoginViewModel NewLogin { get; set; }
        public UserViewModel UserLoged { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Singleton
            instance = this;
            //Create observable colletions
            Menu = new ObservableCollection<MenuItemViewModel>();
            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();
            //Instance services
            dataService = new DataService();
            //Load data
            LoadMenu();
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

        #region Methods
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
