using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Aeronave
    {
        public string Inscricao_Aeronave { get; set; } //PP, PT, PR, PS, BR [XX-XXX]
        public string CNPJ { get; set; }
        public int Capacidade { get; set; }
        public DateTime Ultima_Venda { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public char Situacao { get; set; }
        //public CompanhiaAerea Companhia { get; set; }
        public ConexaoBanco Banco { get; set; }


        public Aeronave()
        {

        }

        public Aeronave(string inscricao_Aero, string cnpj, int capacidade, DateTime ultima_Venda, DateTime data_Cadastro, char situacao)
        {
            Inscricao_Aeronave = inscricao_Aero;
            CNPJ = cnpj;
            Capacidade = capacidade;
            Ultima_Venda = ultima_Venda;
            Data_Cadastro = data_Cadastro;
            Situacao = situacao;
        }


        ConexaoBanco conn = new();

        #region CadastrarAeronave
        public void CadastrarAeronave()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DE AERONAVE <<<");

            if (!VerificaCNPJ())
                return;

            if (!CadastraIdAeronave())
                return;

            int cap = 0;
            do
            {
                Console.Write("Informe a capacidade da aeronave: ");
                cap = int.Parse(Console.ReadLine());
                if (cap < 100 || cap > 500)
                {

                    Console.WriteLine("Capacidade informada inválida!\nInforme novamente!");
                }
                Capacidade = cap;

            } while (cap < 100 || cap > 500);

            Ultima_Venda = DateTime.Now;

            Data_Cadastro = DateTime.Now;

            //Situação
            Console.Write("Situação [A] - Ativo ou [I] - Inativo: ");
            Situacao = char.Parse(Console.ReadLine().ToUpper().Trim());
            while ((this.Situacao.CompareTo('A') != 0) && (this.Situacao.CompareTo('I') != 0))
            {
                Console.WriteLine("\nOpção invalida, digite novamente");
                Console.Write("\nSituação [A] - Ativo ou [I] - Inativo: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper().Trim());
            }

            //gravar no banco
            Console.WriteLine("");
            Console.Write("\nDeseja efetuar a gravação? 1 - SIM OU 2 - NÃO : ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
               
                string sql = $"INSERT INTO Aeronave (Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_Cadastro, Situacao) VALUES ('{this.Inscricao_Aeronave}' , '{this.CNPJ}' , '{this.Capacidade}' , '{this.Ultima_Venda}' , " +
                $"'{this.Data_Cadastro}' , '{this.Situacao}');";
                conn.InsertDados(sql);

                Console.WriteLine("Gravação realizada com sucesso! Pressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Não foi possível efetuar a gravação!");
            }
        }
        #endregion

        #region Select Aeronave Especifica
        public void LocalizarAeronave()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Aeronave Especifica <<<");
            Console.Write("\nDigite o Inscrição Aeronave: ");
            Inscricao_Aeronave = Console.ReadLine();

            while (Inscricao_Aeronave == "0") 
            {
                Console.WriteLine("\nInscrição inválida. Tente novamente");
                Console.Write("Incrição: ");
                Inscricao_Aeronave = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja localizar aeronave no cadastro? Digite 1 - [SIM] 2 - [NÃo]: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_Cadastro, Situacao FROM Aeronave WHERE Inscricao_Aeronave=('{this.Inscricao_Aeronave}');";
                conn.LocalizarAeronave(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização da aeronave! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Selecionar Lista Aeronave
        public void ConsultarListaAeronaves()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Aeronave <<<");
            Console.WriteLine("\nDeseja ver a lista de Aeronaves? Digite 1- Sim ou 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_Cadastro, Situacao FROM Aeronave";
                conn.LocalizarAeronave(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dos companhias! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Update Comapanhia Aerea
        public void UpdateAeronave()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n>>> Editar Dados da Aeronave <<<");
            Console.Write("\nIncrição: ");
            Inscricao_Aeronave = Console.ReadLine();

            while (Inscricao_Aeronave == "0")
            {
                Console.WriteLine("\nInscricao INVÁLIDO! Informe novamente.");
                Console.Write("CNPJ: ");
                CNPJ = Console.ReadLine();
            }

            sql = $"SELECT Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_Cadastro, Situacao FROM Aeronave WHERE Inscricao_Aeronave=('{this.Inscricao_Aeronave}');";
            Banco = new ConexaoBanco();

            if (!string.IsNullOrEmpty(Banco.LocalizarAeronave(sql)))
            {
                Console.WriteLine("\nDeseja efetuar a alteração? 1 - Sim ou 2 - Não");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1 - Capacidade");
                    Console.WriteLine("2 - Data Ultima Venda");
                    Console.WriteLine("3 - Situação");
                    Console.Write("\nDigite: ");
                    opc = int.Parse(Console.ReadLine());

                    while (opc < 1 || opc > 3)
                    {
                        Console.WriteLine("\nDigite uma opção válida:");
                        Console.Write("\nDigite: ");
                        opc = int.Parse(Console.ReadLine());
                    }

                    switch (opc)
                    {
                        case 1:
                            Console.Write("\nAlterar a capacidade: ");
                            Capacidade = int.Parse(Console.ReadLine());
                            sql = $"Update Aeronave Set Capacidade=('{this.Capacidade}') Where Inscricao_Aeronave=('{this.Inscricao_Aeronave}');";
                            break;

                        case 2:
                            Console.Write("\nAlterar a Data da Última Venda: ");
                            Ultima_Venda = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Aeronave Set Ultima_Venda=('{this.Ultima_Venda}') Where Inscricao_Aeronave=('{this.Inscricao_Aeronave}');";
                            break;

                        case 3:
                            Console.Write("\nAlterar a Situação: [A] / [I] ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Companhia_Aerea Set Situacao=('{this.Situacao}') Where Inscricao_Aeronave=('{this.Inscricao_Aeronave}');";
                            break;
                    }

                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                    Banco = new ConexaoBanco();
                    Banco.UpdateDados(sql);
                }
                else
                {
                    Console.WriteLine("\nNÃO foi possível acionar a operação editar cadastro! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nCompanhia Aerea não encontrada! Aperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
        }
        #endregion

        #region Select Aeronave
        public void ConsultarAeronave()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Aeronave <<<");
            Console.Write("\nDigite a Inscrição: ");
            Inscricao_Aeronave = Console.ReadLine();
            while (Inscricao_Aeronave == "0")
            {
                Console.WriteLine("\nCNPJ inválido. Tente novamente");
                Console.Write("CPF: ");
                CNPJ = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja localizar aeronave no cadastro? Digite 1 - [SIM] 2 - [NÃo]: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_C" +
                    $"adastro, Situacao FROM Aeronave WHERE Inscricao_Aeronave=('{this.Inscricao_Aeronave}');";
                conn.LocalizarAeronave(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do passageiro! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Verifica CNPJ Existe
        private bool VerificaCNPJ()
        {
            do
            {
                Console.Write("\nDigite o CNPJ da Companhia Aerea : ");
                CNPJ = Console.ReadLine();


                Console.WriteLine("\n\tVerificação de CNPJ Bloqueado: ");
                if (conn.LocalizarBloqueados(CNPJ, "CNPJ", "Cadastro_Bloqueado"))
                {
                    Console.WriteLine("CNPJ Bloqueado! Não é possível efetuar venda. Tecle Enter para retornar ao menu!");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("CNPJ Apto!!!!");
                    if (conn.VerificaExiste(CNPJ, "CNPJ", "Companhia_Aerea") == false)
                    {
                        Console.WriteLine("O CNPJ não existe no cadastro de passageiros. Informe um CNPJ válido");
                    }
                }
            } while (conn.VerificaExiste(CNPJ, "CNPJ", "Companhia_Aerea") == false);

            Console.WriteLine($"\nCPF do passageiro encontrado: {this.CNPJ} continue sua compra...");

            return true;
        }
        #endregion

        #region Verifica O CNPJ se é válido
        public bool ValidaCNPJ(string vrCNPJ)

        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;

            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Cadastrar ID Aeronave
        private bool CadastraIdAeronave()
        {
            do
            {
                Console.Write("Informe o código de identificação da aeronave seguindo o padrão definido pela ANAC [XX-XXX] [Digite 0 para sair]: ");
                Inscricao_Aeronave = conn.TratamentoDado(Console.ReadLine().ToUpper().Trim().Replace("-", ""));

                if (Inscricao_Aeronave == "0")
                    return false;

                if (Inscricao_Aeronave.Length != 5)
                {
                    Console.WriteLine("Código de inscrição inválido!");
                }
                else
                {
                    if (conn.VerificaExiste(Inscricao_Aeronave, "Inscricao_Aeronave", "Aeronave"))
                    {
                        Console.WriteLine("Código de inscrição existente!");
                        Inscricao_Aeronave = "";
                    }
                }
            } while (Inscricao_Aeronave.Length != 5);

            return true;
        }
        #endregion

        //#region CadastroCNPJ
        //private bool CadastroCNPJ()
        //{
        //    do
        //    {
        //        Console.Write("Informe o CNPJ de qual a aeronave pertence [Digite 0 para sair]: ");
        //        CNPJ = conn.TratamentoDado(Console.ReadLine().Replace(".", "").Replace("-", "").Replace("/", ""));

        //        if (CNPJ == "0")
        //            return false;

        //        if (conn.VerificaExiste(CNPJ, "CNPJ", "Companhia_Aerea"))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("CNPJ NÃO ENCONTRADO");
        //            CNPJ = "";
        //        }
        //    } while (CNPJ.Length == 0);
        //    return false;
        //}
        //#endregion

    }
}
