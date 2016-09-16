using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UsersTracker.Entities.Base;

namespace UsersTracker.Entities
{
    public class Enseigne : EntityBase
    {
        private Compte compte;

        public Compte Compte
        {
            get { return compte; }
            set { compte = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Product> products;
        
        [InverseProperty("Enseigne")]
        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

    }
}