﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XECommerce.Models;
using XECommerce.Pages;
using XECommerce.ViewModels;

namespace XECommerce.Services
{
    public class NavigationService
    {
        private DataService dataService;
        public NavigationService()
        {
            dataService = new DataService();
        }
        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                case "CustomersPage":
                    await App.Navigator.PushAsync(new CustomersPage());
                    break;
                case "DeliveriesPage":
                    await App.Navigator.PushAsync(new DeliveriesPage());
                    break;
                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    break;
                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProductsPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    break;
                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    break;
                case "CustomerDetailPage":
                    await App.Navigator.PushAsync(new CustomerDetailPage());
                    break;
                case "LogoutPage":
                    Logout();
                    break;
                default:
                    break;

            }
        }

        //public User GetCurrentUser()
        //{
        //    return App.CurrentUser;
        //}

        private void Logout() 
        {
            App.CurrentUser.IsRemembered = false;
            dataService.UpdateUser(App.CurrentUser);
            App.Current.MainPage = new LoginPage();

        }

        internal void SetMainPage(User user)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.LoadUser(user);

            App.CurrentUser = user;
            App.Current.MainPage = new MasterPage();
        }
    }
}
