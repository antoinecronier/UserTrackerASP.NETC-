using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UsersTracker.Entities.Base;

namespace UsersTracker.Entities
{
    public class ClientProduct : EntityBase
    {
        private int idClient;

        public int IdClient
        {
            get { return idClient; }
            set { idClient = value; }
        }

        private Client client;

        [ForeignKey("IdClient")]
        public Client Client
        {
            get { return client; }
            set { client = value; }
        }


        private int idProduct;

        public int IdProduct
        {
            get { return idProduct; }
            set { idProduct = value; }
        }

        private Product product;

        [ForeignKey("IdProduct")]
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }


        private int? number;

        public int? Number
        {
            get { return number; }
            set { number = value; }
        }


        private DateTime buyAt;

        public DateTime BuyAt
        {
            get { return buyAt; }
            set { buyAt = value; }
        }

    }
}