using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQLambda
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Password=habil;Persist Security Info=True;User ID=sa;Initial Catalog=fabesul;Data Source=dell-acr\sqlexpress";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from cliente";
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();

            List<Cliente> clientes = new List<Cliente>();

            Cliente cliente = null;

            while (dr.Read())
            {
                cliente = new Cliente();
                cliente.Id = Convert.ToInt32(dr["ccli"]);
                cliente.Nome = Convert.ToString(dr["nomecli"]);
                clientes.Add(cliente);
            }

            //foreach (var linha in clientes)
            //{
            //    Console.WriteLine("CÓDIGO {0,7:D8} CLIENTE {1}", linha.Id, linha.Nome);
            //}


            Cliente F = BuscarPrimeiroComForeach(clientes, 100);
            Cliente L = BuscarPrimeiroComLinq(clientes, 100);
            //Cliente LL = BuscarPrimeiroComLinqLambda(clientes, 10000);
            BuscarPrimeiroComLinqLambda(clientes, "FABE").ForEach(x => Console.WriteLine(x.Nome));

             Console.WriteLine("");
            Console.ReadKey();
            
        }
        public static Cliente BuscarPrimeiroComForeach(List<Cliente> clientes, int id)
        {
            foreach (Cliente item in clientes)
            {
                if (item.Id.Equals(id))
                {
                    return item;
                }
            }
            return null;
        }
        public static Cliente BuscarPrimeiroComLinq(List<Cliente> clientes, int id)
        {
            return (from item in clientes where item.Id.Equals(id) select item).First();
        }
        public static List<Cliente> BuscarPrimeiroComLinqLambda(List<Cliente> clientes, string termo)
        {
            return clientes.Where(x => x.Nome.Contains(termo)).ToList();
        }

        //public static Cliente BuscarPrimeiroComLinqLambda(List<Cliente> clientes, int id){
        //return clientes.First(x => x.Id.Equals(id));
        //}

    }
}
