using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UsersTracker.Entities.Base;

namespace UsersTracker.Entities
{
    public class Client : EntityBase
    {
        private String firstname;

        public String Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private String lastname;

        public String Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        private Compte compte;

        public Compte Compte
        {
            get { return compte; }
            set { compte = value; }
        }

        private List<Enseigne> enseignes;
        
        [NotMapped]
        public List<Enseigne> Enseignes
        {
            get { return enseignes; }
            set { enseignes = value; }
        }

    }
}