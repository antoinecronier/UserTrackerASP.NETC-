using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTracker.Entities.Utils
{
    [NotMapped]
    public class ProductEnseigneList
    {
        private Product product;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        private int? enseigne;

        public int? Enseigne
        {
            get { return enseigne; }
            set { enseigne = value; }
        }
        
        private List<Enseigne> enseignes;

        public List<Enseigne> Enseignes
        {
            get { return enseignes; }
            set { enseignes = value; }
        }

    }
}
