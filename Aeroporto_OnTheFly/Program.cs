using System;
using System.Data.SqlClient;

namespace Aeroporto_OnTheFly
{
    internal class Program
    {
        #region Menu Principal
        static void Menu()
        {
            string op;

            do
            {

                Console.WriteLine("\n|°°°°°°°°°° BEM VINDO AO AEROPORTO ON THE FLY °°°°°°°°°|");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|°°°°°°°°°°°°°°°°°°  MENU DE OPÇÕES  °°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|   opção 1 : Passageiro                               |");
                Console.WriteLine("|   opção 2 : Companhia Aerea                          |");
                Console.WriteLine("|   opção 3 : Aeronave                                 |");
                Console.WriteLine("|   opção 4 : Voo                                      |");
                Console.WriteLine("|   opção 5 : Passagens Voo                            |");
                Console.WriteLine("|   opção 6 : Venda                                    |");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|                                                      |");
                Console.WriteLine("|   opção 0 : Sair                                     |");
                Console.WriteLine("|______________________________________________________|");
                Console.WriteLine("\nInforme a opção que deseja realizar");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "5" && op != "6" && op != "7" && op != "8" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }

            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "5" && op != "6" && op != "7" && op != "8");

            SqlConnection conexaosql = new SqlConnection();
            ConexaoBanco conn = new ConexaoBanco();
            SqlConnection conexaoslq = new SqlConnection(conn.Caminho());

            switch (op)
            {

                case "1":
                    Passageiro();
                    Menu();
                    break;

                case "2":
                    Companhia();
                    Console.Clear();
                    Menu();
                    break;

                case "3":
                    Aeronaves();
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    
                    Console.Clear();
                    Menu();
                    break;

                case "5":
                    
                    Console.Clear();
                    Menu();
                    break;

                case "6":
                    
                    Console.Clear();
                    Menu();
                    break;

                case "7":

                    Console.Clear();
                    Menu();
                    break;

                case "8":

                    Console.Clear();
                    Menu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
        #endregion

        #region Menu Passageiro
        static void Passageiro()
        {
            Console.Clear();
            Passageiro pessoa = new();
            string op;
            do
            {
                
                Console.WriteLine("\n|°°°°°°°°°°°°°°°°°° MENU PASSAGEIRO °°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  1 - Cadastrar Passageiro                           |");
                Console.WriteLine("|  2 - Selecionar Passageiro Específico               |");
                Console.WriteLine("|  3 - Exibir Lista de Passageiro                     |");
                Console.WriteLine("|  4 - Alterar dados de Passageiros                   |");
                Console.WriteLine("|  0 - Encerrar                                       |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|_____________________________________________________|");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }
            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "0");

            switch (op)
            {
                case "1":
                    pessoa.CadastrarPassageiro();
                    Console.Clear();
                    Menu();
                    break;

                case "2":
                    pessoa.LocalizarPassageiro();
                    Console.Clear();
                    Menu();
                    break;

                case "3":
                    pessoa.ConsultarListaPassageiro();
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    pessoa.UpdatePassageiro();
                    Console.Clear();
                    Menu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
            
        }
        #endregion

        #region Menu Companhia
        static void Companhia()
        {
            Console.Clear();
            CompanhiaAerea companhia = new();

            string op;
            do
            {

                Console.WriteLine("\n|°°°°°°°°°°°°°°°°°°  MENU COMPANHIA  °°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  1 - Cadastrar Companhia Aerea                      |");
                Console.WriteLine("|  2 - Selecionar Companhia Aerea                     |");
                Console.WriteLine("|  3 - Exibir Lista de Companhia Aerea                |");
                Console.WriteLine("|  4 - Alterar dados de Companhia Aerea               |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  0 - Encerrar                                       |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|_____________________________________________________|");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }
            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "0");

            switch (op)
            {
                case "1":
                    companhia.CadastrarCompanhia();
                    Console.Clear();
                    Menu();
                    break;

                case "2":
                    companhia.LocalizarCompanhia();
                    Console.Clear();
                    Menu();
                    break;

                case "3":
                    companhia.ConsultarListaCompanhia();
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    companhia.UpdateCompanhia();
                    Console.Clear();
                    Menu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }

        }
        #endregion

        
        #region Menu Aeronaves
        static void Aeronaves()
        {

            Console.Clear();
            Aeronave aero = new();
            string op;
            do
            {

                Console.WriteLine("\n|°°°°°°°°°°°°°°°°°° MENU AERONAVE °°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  1 - Cadastrar Aeronave                             |");
                Console.WriteLine("|  2 - Selecionar Passageiro Específico               |");
                Console.WriteLine("|  3 - Exibir Lista de Passageiro                     |");
                Console.WriteLine("|  4 - Alterar dados de Passageiros                   |");
                Console.WriteLine("|  0 - Encerrar                                       |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|_____________________________________________________|");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }
            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "0");

            switch (op)
            {
                case "1":
                    aero.CadastrarAeronave();
                    Console.Clear();
                    Menu();
                    break;

                case "2":
                    aero.LocalizarAeronave();
                    Console.Clear();
                    Menu();
                    break;

                case "3":
                    aero.ConsultarListaAeronaves(); //NAO TA LENDO
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    aero.UpdateAeronave();
                    Console.Clear();
                    Menu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }

        }
        #endregion

        #region Menu Voo
        static void Voo()
        {

            Voo voo = new();

            Console.Clear();
            string op;
            do
            {

                Console.WriteLine("\n|°°°°°°°°°°°°°°°°°°°° MENU VOO °°°°°°°°°°°°°°°°°°°°°|");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|  1 - Cadastrar Voo                                  |");
                Console.WriteLine("|  2 - Selecionar Voo Específico                      |");
                Console.WriteLine("|  3 - Exibir Lista de Voos                           |");
                Console.WriteLine("|  4 - Alterar dados do Voo                           |");
                Console.WriteLine("|  0 - Encerrar                                       |");
                Console.WriteLine("|                                                     |");
                Console.WriteLine("|_____________________________________________________|");

                op = Console.ReadLine();
                if (op == "0")
                    return;
                if (op != "1" && op != "2" && op != "3" && op != "4" && op != "0")
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida!");
                }
            } while (op != "1" && op != "2" && op != "3" && op != "4" && op != "0");

            switch (op)
            {
                case "1":
                    Console.Clear();
                    Menu();
                    break;

                case "2":
                    Console.Clear();
                    Menu();
                    break;

                case "3":
                    Console.Clear();
                    Menu();
                    break;

                case "4":
                    Console.Clear();
                    Menu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }

        }
        #endregion
        static void Main(string[] args)
        {
            //SqlConnection conexaosql = new SqlConnection();
            //ConexaoBanco conn = new ConexaoBanco();
            //SqlConnection conexaoslq = new SqlConnection(conn.Caminho());

            Menu();
        }
    }
}
