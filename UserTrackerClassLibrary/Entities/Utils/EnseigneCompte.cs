using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTracker.Entities.Utils
{
    [NotMapped]
    public class EnseigneCompte
    {
        private Compte compte;

        public Compte Compte
        {
            get { return compte; }
            set { compte = value; }
        }

        private Enseigne enseigne;

        public Enseigne Enseigne
        {
            get { return enseigne; }
            set { enseigne = value; }
        }

    }
}
