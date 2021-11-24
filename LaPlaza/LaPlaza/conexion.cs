using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LaPlaza
{
    class conexion
    {
        public static MySqlConnection getConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=localhost;database=laplaza; Uid=root;pwd=Venado86");
            conectar.Open();
            return conectar;
        }
        
    }
}
