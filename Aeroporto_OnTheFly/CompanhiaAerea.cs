﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class CompanhiaAerea
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime Data_Abertura { get; set; }
        public DateTime Ultimo_Voo { get; set; }
        public char Situacao { get; set; }
        public ConexaoBanco Banco { get; set; }
        public CompanhiaAerea()
        {
            
        }

        #region Cadastrar Companhia
        public void CadastrarCompanhia()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DE COMPANHIA AEREA <<<");

            ConexaoBanco conn = new();

            bool validacao = false;

            //CNPJ
            do
            {
                Console.Write("\nInforme o CNPJ: ");
                CNPJ = conn.TratamentoDado(Console.ReadLine());
                if (CNPJ == "0")
                    return;
                if (!ValidaCNPJ(CNPJ))
                {
                    Console.WriteLine("Digite um CPF Válido!");
                }
            } while (!ValidaCNPJ(CNPJ));

            //Nome
            do
            {
                Console.Write("Informe a razão social (máximo de 50 caracteres): ");
                RazaoSocial = Console.ReadLine();
                if (RazaoSocial.Length == 0)
                {
                    Console.WriteLine("Nome obrigatório!");
                }
                if (RazaoSocial.Length > 50)
                {
                    Console.WriteLine("Informe um nome com menos de 50 caracteres!");
                }
            } while (RazaoSocial.Length > 50 || RazaoSocial.Length == 0);

            //Data de abertura
            do
            {
                Console.Write("Data de abertura: ");

                try
                {
                    Data_Abertura = DateTime.Parse(Console.ReadLine());
                    validacao = false;
                }
                catch (Exception)
                {

                    Console.WriteLine("Formato de data inválido [dd/mm/aaaa]");
                    validacao = true;
                }
                if (Data_Abertura > DateTime.Now.AddMonths(-6))
                {
                    Console.WriteLine("O tempo de abertura é insufiente para finalizar o cadastro!");
                    validacao = true;
                }

            } while (validacao);

            Ultimo_Voo = DateTime.Parse(Console.ReadLine());

            //Situação
            do
            {
                Console.Write("Situação A - Ativo ou I - Inativo: ");
                Situacao = char.Parse(Console.ReadLine());

                if (Situacao != 'I' && Situacao != 'A')
                {
                    Console.WriteLine("Digite um opção válida!!!");
                }
            } while (Situacao != 'I' && Situacao != 'A');

            //gravar no banco
            Console.WriteLine("");
            Console.Write("\nDeseja efetuar a gravação? 1 - SIM OU 2 - NÃO : ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                //CNPJ, RazaoSocial, Data_Abertura, Ultimo_Voo, Situacao
                string sql = $"INSERT INTO Companhia_Aerea (CNPJ, RazaoSocial, Data_Abertura, Ultimo_Voo, Situacao) VALUES ('{this.CNPJ}' , '{this.RazaoSocial}' , '{this.Data_Abertura.ToString("MM/dd/yyyy")}' , " +
                $"'{this.Ultimo_Voo.ToString("MM/dd/yyyy")}' , '{this.Situacao}');";
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

        #region Select Companhia Especifica
        public void LocalizarCompanhia()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Companhia Aerea Especifica <<<");
            Console.Write("\nDigite o CNPJ: ");
            CNPJ = Console.ReadLine();
            while (ValidaCNPJ(this.CNPJ) == false || this.CNPJ.Length < 14)
            {
                Console.WriteLine("\nCPF inválido. Tente novamente");
                Console.Write("CPF: ");
                CNPJ = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja localizar companhia no cadastro? Digite 1 - [SIM] 2 - [NÃo]: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT CNPJ, RazaoSocial, Data_Abertura, Ultimo_Voo, Situacao From Companhia_Aerea WHERE CPF=('{this.CNPJ}');";
                Banco = new ConexaoBanco();
                Banco.LocalizarDadosCompanhia(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do passageiro! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Selecionar Lista Companhia
        public void ConsultarListaCompanhia()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Companhia Aerea(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim ou 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT CNPJ, RazaoSocial, Data_Abertura, Ultimo_Voo, Situacao From Companhia_Aerea";
                Banco = new ConexaoBanco();
                Banco.LocalizarDadosPassageiro(sql);
                Console.WriteLine("\nAperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dos companhias! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Update Comapanhia Aerea
        public void UpdateCompanhia()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n>>> Editar Dados do Passageiro <<<");
            Console.Write("\nInforme o CNPJ: ");
            CNPJ = Console.ReadLine();

            while (ValidaCNPJ(this.CNPJ) == false || this.CNPJ.Length < 14)
            {
                Console.WriteLine("\nCNPJ INVÁLIDO! Informe novamente.");
                Console.Write("CPF: ");
                CNPJ = Console.ReadLine();
            }

            sql = $"SELECT CNPJ, RazaoSocial, Data_Abertura, Ultimo_Voo, Situacao From Companhia_Aerea WHERE CPF=('{this.CNPJ}');";
            Banco = new ConexaoBanco();

            if (!string.IsNullOrEmpty(Banco.LocalizarDadosPassageiro(sql)))
            {
                Console.WriteLine("\nDeseja efetuar a alteração? 1 - Sim ou 2 - Não");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1 - Razão Social");
                    Console.WriteLine("2 - Data de Abertura");
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
                            Console.Write("\nAlterar nome: ");
                            RazaoSocial = Console.ReadLine();
                            sql = $"Update Passageiro Set Nome=('{this.RazaoSocial}') Where CPF=('{this.CNPJ}');";
                            break;

                        case 2:
                            Console.Write("\nAlterar a data de nascimento: ");
                            Data_Abertura = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Data_Nascimento=('{this.Data_Abertura}') Where CPF=('{this.CNPJ}');";
                            break;

                        case 3:
                            Console.Write("\nAlterar o sexo (M/F/N): ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Passageiro Set Sexo=('{this.Situacao}') Where CPF=('{this.CNPJ}');";
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
    }
}