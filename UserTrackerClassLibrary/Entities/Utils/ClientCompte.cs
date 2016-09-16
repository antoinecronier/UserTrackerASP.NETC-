using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTracker.Entities.Utils
{
    [NotMapped]
    public class ClientCompte
    {
        private Client client;

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        private Compte compte;

        public Compte Compte
        {
            get { return compte; }
            set { compte = value; }
        }

    }
}
