using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UsersTracker.Entities.Base;

namespace UsersTracker.Entities
{
    public class Product : EntityBase
    {
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private Decimal? price;

        public Decimal? Price
        {
            get { return price; }
            set { price = value; }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private Enseigne enseigne;

        public Enseigne Enseigne
        {
            get { return enseigne; }
            set { enseigne = value; }
        }

    }
}