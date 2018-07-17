using Dapper.Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Dapper
{
    class Program
    {
        static string strConexao = ConfigurationManager.ConnectionStrings["conexaoBeleza"].ConnectionString;

        static void Main(string[] args)
        {
            Exemplo1();
            Exemplo2();
            Exemplo3();
            Exemplo4();
            Exemplo5();
            Console.ReadKey();
        }

        private static void Exemplo1()
        {
            SqlConnection conexaoBD = new SqlConnection(strConexao);
            conexaoBD.Open();

            var resultado = conexaoBD.Query("Select * from Cliente");

            Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Contato", "Endereco do Cliente");
            foreach (dynamic cliente in resultado)
            {
                Console.WriteLine("{0} - {1} - {2} ", cliente.Id, cliente.Nome, cliente.Endereco);
            }
            conexaoBD.Close();
        }

        private static void Exemplo2()
        {
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                conexaoBD.Open();
                IEnumerable clientes = conexaoBD.Query<Cliente>("Select * from Cliente");

                Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Cliente", "Email");
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine("{0} - {1} - {2}", cliente.Id, cliente.Nome, cliente.Email);
                }
            }
        }

        private static void Exemplo3()
        {
            using (SqlConnection cn = new SqlConnection(strConexao))
            {
                cn.Open();
                Cliente person = cn.Get<Cliente>("EA427461-9FD2-4543-917B-0E969989DD52");
                cn.Close();
            }
        }

        private static void Exemplo4()
        {
            using (SqlConnection cn = new SqlConnection(strConexao))
            {
                cn.Open();
                var predicate = Predicates.Field<Cliente>(f => f.Nome, Operator.Eq, "juca");
                int count = cn.Count<Cliente>(predicate);
                cn.Close();
            }
        }

        private static void Exemplo5()
        {
            using (SqlConnection cn = new SqlConnection(strConexao))
            {
                cn.Open();
                var predicate = Predicates.Field<Cliente>(f => f.DataHoraInclusao, Operator.Le, DateTime.Now);
                IEnumerable<Cliente> list = cn.GetList<Cliente>(predicate);
                foreach (var item in list)
                {
                    string nome = item.Email;
                }
                cn.Close();
            }
        }

    }
}
