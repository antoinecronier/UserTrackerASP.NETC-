using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersTracker.Entities.Base;

namespace UsersTracker.Entities
{
    public class ClientEnseigne : EntityBase
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


        private int idEnseigne;

        public int IdEnseigne
        {
            get { return idEnseigne; }
            set { idEnseigne = value; }
        }

        private Enseigne enseigne;

        [ForeignKey("IdEnseigne")]
        public Enseigne Enseigne
        {
            get { return enseigne; }
            set { enseigne = value; }
        }

    }
}
