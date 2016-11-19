﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XECommerce.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int OrderId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        public int StateId { get; set; }

        public DateTime Date { get; set; }

        public string Remarks { get; set; }

        [ManyToOne]
        public Customer Customer { get; set; }

        [ManyToOne]
        public State State { get; set; }

        [ManyToOne]
        public Company Company { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<OrderDetail> OrderDetails { get; set; }

        public override int GetHashCode()
        {
            return OrderId;
        }

    }

}
