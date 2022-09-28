using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Passageiro
    {
        public String CPF { get; set; } //prop CHAVE com 11 dígitos
        public String Nome { get; set; } // < 50 digitos
        public DateTime DataNasc { get; set; }
        public char Sexo { get; set; } //M F N
        public DateTime Ultima_Compra { get; set; } //no cadastro, data atual
        public DateTime Data_Cadastro { get; set; }
        public char Situacao { get; set; } //A - Ativo I - Inativo
        public ConexaoBanco Banco { get; set; }


        public Passageiro()
        {
            Ultima_Compra = DateTime.Now; //data atual do sistema
            Data_Cadastro = DateTime.Now; //data atual do sistema
            Situacao = 'A';
        }

        #region Cadastrar Passageiro
        public void CadastrarPassageiro()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DE PASSAGEIRO <<<");

            ConexaoBanco conn = new();

            bool validacao = false;

            //CPF
            do
            {
                Console.Write("\nInforme seu CPF: ");
                CPF = conn.TratamentoDado(Console.ReadLine());
                if (CPF == "0")
                    return;
                if (!ValidaCPF(CPF))
                {
                    Console.WriteLine("Digite um CPF Válido!");
                }
            } while (!ValidaCPF(CPF));

            //Nome
            do
            {
                Console.Write("Informe seu nome completo (máximo de 50 caracteres): ");
                Nome = Console.ReadLine();
                if (Nome.Length == 0)
                {
                    Console.WriteLine("Nome obrigatório!");
                }
                if (Nome.Length > 50)
                {
                    Console.WriteLine("Informe um nome com menos de 50 caracteres!");
                }
            } while (Nome.Length > 50 || Nome.Length == 0);

            //Data de Nascimento
            do
            {
                Console.Write("Data de nascimento: ");

                try
                {
                    DataNasc = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {

                    Console.WriteLine("Formato de data inválido [dd/mm/aaaa]");
                    validacao = true;
                }
                if (DataNasc > DateTime.Now)
                {
                    Console.WriteLine("Data de aniversário não pode ser futura!");
                    validacao = true;
                }

            } while (validacao);

            //Sexo
            do
            {
                Console.Write("Sexo [F] Feminino [M] Masculino [N] Prefere não informar: ");
                Sexo = char.Parse(conn.TratamentoDado(Console.ReadLine()).ToUpper());
                if (Sexo == '0')
                    return;

                if (Sexo != 'M' && Sexo != 'N' && Sexo != 'F')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Sexo != 'M' && Sexo != 'N' && Sexo != 'F');

            Ultima_Compra = DateTime.Now;
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
            Console.Write("\nDigite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                string sql = $"INSERT INTO Passageiro (CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao) VALUES ('{this.CPF}' , '{this.Nome}' , '{this.DataNasc}' , '{this.Sexo}' , " +
                $"'{this.Ultima_Compra}' , '{this.Data_Cadastro}' , '{this.Situacao}');";
                Banco = new ConexaoBanco();
                Banco.InsertDados(sql);

                Console.WriteLine("Gravação realizada com sucesso! Pressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Não foi possível efetuar a gravação!");
            }
        }
        #endregion

        #region Select Passageiro Especifico
        public void LocalizarPassageiro()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Passageiro Especifico <<<");
            Console.Write("\nDigite o CPF: ");
            CPF = Console.ReadLine();
            while (ValidaCPF(this.CPF) == false || this.CPF.Length < 11)
            {
                Console.WriteLine("\nCPF inválido. Tente novamente");
                Console.Write("CPF: ");
                CPF = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja localizar passageiro no cadastro? Digite 1 - [SIM] 2 - [NÃo]: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao From Passageiro WHERE CPF=('{this.CPF}');";
                Banco = new ConexaoBanco();
                Banco.LocalizarDadosPassageiro(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do passageiro! Aperte ENTER para retornar ao Menu.");
            }
        }
        #endregion

        #region Selecionar Passageiro
        public void ConsultarListaPassageiro()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Passageiro(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim ou 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao FROM Passageiro";
                Banco = new ConexaoBanco();
                Banco.LocalizarDadosPassageiro(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dos passageiros! Aperte ENTER para retornar ao Menu.");
            }
        }
        #endregion

        #region Update Passageiro
        public void UpdatePassageiro()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n>>> Editar Dados do Passageiro <<<");
            Console.Write("\nInforme o CPF: ");
            CPF = Console.ReadLine();

            while (ValidaCPF(this.CPF) == false || this.CPF.Length < 11)
            {
                Console.WriteLine("\nCPF INVÁLIDO! Informe novamente.");
                Console.Write("CPF: ");
                CPF = Console.ReadLine();
            }

            sql = $"SELECT CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao FROM Passageiro WHERE CPF=('{this.CPF}');";
            Banco = new ConexaoBanco();

            if (!string.IsNullOrEmpty(Banco.LocalizarDadosPassageiro(sql)))
            {
                Console.WriteLine("\nDeseja efetuar a alteração? 1 - Sim ou 2 - Não");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1 - Nome");
                    Console.WriteLine("2 - Data de Nascimento");
                    Console.WriteLine("3 - Sexo (M/F/N)");
                    Console.WriteLine("4 - Situação");
                    Console.Write("\nDigite: ");
                    opc = int.Parse(Console.ReadLine());

                    while (opc < 1 || opc > 5)
                    {
                        Console.WriteLine("\nDigite uma opção válida:");
                        Console.Write("\nDigite: ");
                        opc = int.Parse(Console.ReadLine());
                    }

                    switch (opc)
                    {
                        case 1:
                            Console.Write("\nAlterar nome: ");
                            Nome = Console.ReadLine();
                            sql = $"Update Passageiro Set Nome=('{this.Nome}') Where CPF=('{this.CPF}');";
                            break;

                        case 2:
                            Console.Write("\nAlterar a data de nascimento: ");
                            DataNasc = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Data_Nascimento=('{this.DataNasc}') Where CPF=('{this.CPF}');";
                            break;

                        case 3:
                            Console.Write("\nAlterar o sexo (M/F/N): ");
                            Sexo = char.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Sexo=('{this.Sexo}') Where CPF=('{this.CPF}');";
                            break;

                        case 4:
                            Console.WriteLine("\nSituação Atual: ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Situacao=('{this.Situacao}') Where CPF=('{this.CPF}');";
                            break;
                    }

                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao Menu.");
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
                Console.WriteLine("\nPassageiro Não Encontrado! Aperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
        }
        #endregion


        #region Método Para Validar o CPF 
        private static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");

            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)

                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
        #endregion
    }
    
}