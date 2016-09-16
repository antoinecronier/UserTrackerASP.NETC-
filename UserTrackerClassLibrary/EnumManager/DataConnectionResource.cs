using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersTracker.EnumManager
{
    public enum DataConnectionResource : Int32
    {
        [StringValue("Server=myServer;Port=3306;Database=db_name;Uid=userName;Pwd=password")]
        GENERICMYSQL = 1,
        [StringValue("Server=127.0.0.1;Port=3306;Database=db_name1;Uid=root;Pwd=''")]
        LOCALMYSQL = 2,
        [StringValue("http://localhost:59579/api/")]
        LOCALAPI = 3,
    }
}

