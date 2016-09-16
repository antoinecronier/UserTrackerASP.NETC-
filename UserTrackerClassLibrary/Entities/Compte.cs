using System;
using System.Collections.Generic;
using System.Linq;
using UsersTracker.Entities.Base;

namespace UsersTracker.Entities
{
    public class Compte : EntityBase
    {
        private Decimal? solde;

        public Decimal? Solde
        {
            get { return solde; }
            set { solde = value; }
        }

        private String number;

        public String Number
        {
            get { return number; }
            set { number = value; }
        }

    }
}