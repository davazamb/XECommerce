﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XECommerce.Data;
using XECommerce.Models;

namespace XECommerce.Services
{
    public class DataService
    {
        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.First<User>(true);
            }
        }
        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {                   
                    da.Update(user);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario actualizado Ok",
                    Result = user,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public Response InsertUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldUser = da.First<User>(false);
                    if (oldUser != null)
                    {
                        da.Delete(oldUser);
                    }
                    da.Insert(user);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario ingresado Ok",
                    Result = user,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        //public void SaveProducts(List<Product> products)
        //{
        //    using (var da = new DataAccess())
        //    {
        //        var oldProducts = da.GetList<Product>(false);
        //        foreach (var product in oldProducts)
        //        {
        //            da.Delete(product);
        //        }
        //        foreach (var product in products)
        //        {
        //            da.Insert(product);
        //        }
        //    }
        //}

        public List<Product> GetProducts(string productsFilter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Product>(true)
                    .OrderBy(p => p.Description)
                    .Where(p => p.Description.ToUpper().Contains(productsFilter.ToUpper()))
                    .ToList();
            }
        }

        //public List<Product> GetProducts()
        //{
        //    using (var da = new DataAccess())
        //    {
        //        return da.GetList<Product>(true).OrderBy(p => p.Description).ToList();
        //    }
        //}

        public List<Department> GetDepartments()
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Department>(true).OrderBy(d => d.Name).ToList();
            }
        }
        public List<City> GetCities()
        {
            using (var da = new DataAccess())
            {
                return da.GetList<City>(true).OrderBy(d => d.Name).ToList();
            }
        }

        public Response Login(string email, string password)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var user = da.First<User>(true);
                    if(user == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No hay conexion y usuario guardado localmente",
                        };
                    }
                    if(user.UserName.ToUpper() == email.ToUpper() && user.Password == password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Usuario ingresado Ok",
                            Result = user,
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o contraseña incorrectos",
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public List<Customer> GetCustomers(string customersFilter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Customer>(true)
                    .OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
                    .Where(c => c.FirstName.ToUpper().Contains(customersFilter.ToUpper()) ||
                    c.LastName.ToUpper().Contains(customersFilter.ToUpper())).ToList();
            }
        }

        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).ToList();
            }
        }

        public void Save<T>(List<T> list) where T :class
        {
            using (var da = new DataAccess())
            {
                var oldRecords = da.GetList<T>(false);
                foreach (var record in oldRecords)
                {
                    da.Delete(record);
                }
                foreach (var record in list)
                {
                    da.Insert(record);
                }
            }
        }
    }
}
