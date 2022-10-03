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
        private static string Conexao = "Data Source= localhost; Initial Catalog=ONTHEFLY; User Id = sa; Password = *****;";

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
                    Console.WriteLine("\n\t>>> Aeronave(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        //Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_Cadastro, Situacao
                        recebe = reader.GetString(0);
                        Console.Write($"Inscrição: {reader.GetString(0)}\n");
                        Console.Write($"CNPJ: {reader.GetString(1)}\n");
                        Console.Write($"Capacidade: {reader.GetInt32(2)}\n");
                        Console.Write($"Data da Última Venda: {reader.GetDateTime(3).ToShortDateString()}\n");
                        Console.Write($"Data Cadastro: {reader.GetDateTime(4).ToShortDateString()}\n");
                        Console.Write($"Situação: {reader.GetString(5)}\n");


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

        #region Localizar Voo
        public String LocalizarVoo(String sql)
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
                    Console.WriteLine("\n\t>>> Voo Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"ID Voo: {reader.GetString(0)}\n");
                        Console.Write($"Incricao Aeronave: {reader.GetString(1)}\n");
                        Console.Write($"Destino: {reader.GetString(2)}\n");
                        Console.Write($"Data/Hora Voo: {reader.GetDateTime(3).ToShortDateString()}\n");
                        Console.Write($"Data Cadastro: {reader.GetDateTime(4).ToShortDateString()}\n");
                        Console.Write($"Assento Ocupados: {reader.GetInt32(5)}\n");
                        Console.Write($"Situção: {reader.GetString(6)}\n");

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

        #region Localizar IATAS

        public String LocalizarIATAS(String sql)
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
                    Console.WriteLine("\n\t>>> Iata(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        recebe = reader.GetString(0);
                        Console.Write($"Iata: {reader.GetString(0)}");
                        Console.WriteLine($" Cidade: {reader.GetString(1)}");
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

        #region Localizar Passagem
        public String LocalizarPassagem(String sql)
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
                    Console.WriteLine("\n\t>>> Passagem(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        //Passagem_Voo ID_Passagem, ID_Voo, Data_Ultima_Compra, Valor, Situacao
                        recebe = reader.GetString(0);
                        Console.Write($"ID Passagem: {reader.GetString(0)}\n");
                        Console.Write($"ID Voo: {reader.GetString(1)}\n");
                        Console.Write($"Data da Última Compra: {reader.GetDateTime(2).ToShortDateString()}\n");
                        Console.Write($"Valor: {reader.GetDouble(3)}\n");
                        Console.Write($"Situção: {reader.GetString(4)}\n");

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

        #region Localizar Venda
        public String LocalizarVenda(String sql)
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
                    Console.WriteLine("\n\t>>> Venda(s) Localizada(s) <<<\n");
                    while (reader.Read())
                    {
                        //ID_Venda, CPF, Data_Venda, Valor_Total, ID_Passagem
                        recebe = reader.GetString(0);
                        Console.Write($"ID Venda: {reader.GetString(0)}\n");
                        Console.Write($"CPF: {reader.GetString(1)}\n");
                        Console.Write($"Data da Venda: {reader.GetDateTime(2).ToShortDateString()}\n");
                        Console.Write($"Valor Total: {reader.GetDouble(3)}\n");
                        Console.Write($"ID da Passagem: {reader.GetString(4)}\n");

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
                Conecta.Close();
            }
        }
        #endregion

        #region Verifica Dado Existente
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

        #region CPF Bloqueado
        public bool LocalizarBloqueados(string dado, string campo, string tabela)
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
                Conecta.Close();
                Console.WriteLine($"Erro ao acessar o Banco de Dados!!!\n{e.Message}");
                Console.WriteLine("Tecle Enter para continuar....");
                Console.ReadKey();
                return false;
            }
        }
        #endregion
    }

}

    
