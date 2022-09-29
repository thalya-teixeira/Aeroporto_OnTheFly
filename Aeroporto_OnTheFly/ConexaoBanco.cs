using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class ConexaoBanco
    {
        //conectar com o banco de dados // obj de string de conexao // initial catalog é o USE do sql
        private static string Conexao = "Data Source= localhost; Initial Catalog=ONTHEFLY; User Id = sa; Password = th031425;";

        private static SqlConnection Conecta = new SqlConnection(Conexao);

        public ConexaoBanco()
        {

        }

        public string Caminho()
        {
            return Conexao;
        }

        #region Insert Dados
        public void InsertDados(String sql)
        {
            try
            {
                Conecta.Open();
                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                cmd.ExecuteNonQuery();
                Conecta.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Select Dados
        public void SelectDados(String sql)
        {
            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                cmd.ExecuteNonQuery();

                Conecta.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Localizar Dados Passageiro
        public String LocalizarDadosPassageiro(String sql)
        {

            String recebe = "";

            try
            {

                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                   // CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao
                    Console.WriteLine("\n\t>>> Passageiro(s) Localizado(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"CPF: {reader.GetString(0)}\n");
                        Console.Write($"Nome: {reader.GetString(1)}\n");
                        Console.Write($"Data de Nascimento: {reader.GetDateTime(2).ToShortDateString()}\n");
                        Console.Write($"Sexo: {reader.GetString(3)}\n");
                        Console.Write($"Data da Última Compra: {reader.GetDateTime(4).ToShortDateString()}\n");
                        Console.Write($"Data de Cadastro: {reader.GetDateTime(5).ToShortDateString()}\n");
                        Console.Write($"Situação: {reader.GetString(6)}\n");



                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

            return recebe;

        }
        #endregion

        #region Localizar Dados Companhia

        public String LocalizarDadosCompanhia(String sql)
        {
            String recebe = "";

            try
            {

                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    //Companhia_Aerea(CNPJ, RazaoSocial, Data_Abertura, Ultimo_Voo, Situacao

                   Console.WriteLine("\n\t>>> Companhia Aerea(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"CNPJ: {reader.GetString(0)}\n");
                        Console.Write($"Razão Social: {reader.GetString(1)}\n");
                        Console.Write($"Data de Abertura: {reader.GetDateTime(2).ToShortDateString()}\n");
                        Console.Write($"Data do Último Voo: {reader.GetDateTime(3).ToShortDateString()}\n");
                        Console.Write($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}\n");
                        Console.Write($"Situção: {reader.GetString(5)}\n");


                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recebe;
        }
        #endregion

        #region Localizar Aeronave
        public String LocalizarAeronave(String sql)
        {
            String recebe = "";

            try
            {

                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                SqlDataReader reader = null;

                using (reader = cmd.ExecuteReader())
                {
                    //Inscricao_Aeronave, CNPJ, Capacidade, Ultimo_Venda, Data_Cadastro, Situacao
                    Console.WriteLine("\n\t>>> Aeronaves Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"Inscricao: {reader.GetString(0)}\n");
                        Console.Write($"CNPJ: {reader.GetString(1)}\n");
                        Console.Write($"Capacidade: {reader.GetInt32(2)}\n");
                        Console.Write($"Data do Último Venda: {reader.GetDateTime(3).ToShortDateString()}\n");
                        Console.Write($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}\n");
                        Console.Write($"Situção: {reader.GetString(5)}\n");


                        Console.WriteLine("\n");
                    }
                }
                Conecta.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return recebe;
        }
        #endregion

        #region Update Dados
        public void UpdateDados(String sql)
        {

            try
            {
                Conecta.Open();

                SqlCommand cmd = new SqlCommand(sql, Conecta);
                cmd.Connection = Conecta;
                cmd.ExecuteNonQuery();

                Conecta.Close();

            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Verifica CNPJ Dado Existente
        public bool VerificaExiste(string dado, string campo, string tabela)
        {
            string queryString = $"SELECT {campo} FROM {tabela} WHERE {campo} = '{dado}'";

            try
            {
                SqlCommand command = new SqlCommand(queryString);
                command.Connection = Conecta;
                Conecta.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Conecta.Close();
                        return true;
                    }
                    else
                    {
                        Conecta.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($"Erro ao acessar o Banco de Dados! \n{e.Message}");
                Console.WriteLine("Tecle ENTER para continuar...");
                Console.ReadKey();
                return false;
            }
        }
        #endregion

        

        #region Tratamento Dado
        public string TratamentoDado(string dado)
        {
            string dadoTratado = dado.Replace(".", "").Replace("-", "").Replace("'", "").Replace("]", "").Replace("[", "");
            return dadoTratado;
        }
        #endregion

    }
}

    
