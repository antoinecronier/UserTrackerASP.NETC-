using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTracker.Entities.Base
{
    public class EntityBase
    {
        private int id;

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
