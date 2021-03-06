﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XECommerce.Models;
using XECommerce.Pages;
using XECommerce.Services;
using XECommerce.ViewModels;

namespace XECommerce
{
    //Se agrega partial cambiamos el nombre content del xml por Application
    public partial class App : Application
    {
        #region Attributes
        private DataService dataService;
        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static User CurrentUser { get; internal set; }
        #endregion

        #region Constructors
        public App()
        {
            //Agregamos un inicializador de componentes
            InitializeComponent();

            //Modo remember
            dataService = new DataService();
            var user = dataService.GetUser();

            if (user != null && user.IsRemembered)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.LoadUser(user); 
                App.CurrentUser = user;
                MainPage = new MasterPage();
            }else
            {
                MainPage = new LoginPage();
            }

        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
