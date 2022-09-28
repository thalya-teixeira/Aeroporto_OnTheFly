using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Aeronave
    {
        public string Inscricao_Aeronave { get; set; } //PP, PT, PR, PS, BR [XX-XXXX]
        public string CNPJ { get; set; }
        public int Capacidade { get; set; }
        public DateTime Ultima_Venda { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public string Situacao { get; set; }
        public CompanhiaAerea Companhia { get; set; }


        public Aeronave()
        {

        }

        public Aeronave(string inscricao_Aero, string cnpj, int capacidade, DateTime ultima_Venda, DateTime data_Cadastro, string situacao)
        {
            Inscricao_Aeronave = inscricao_Aero;
            CNPJ = cnpj;
            Capacidade = capacidade;
            Ultima_Venda = ultima_Venda;
            Data_Cadastro = data_Cadastro;
            Situacao = situacao;
        }

        #region Cadastrar Aeronave



        #endregion



        /*
                #region sufixo aeronave
                public String SufixoAeronave()
                {
                    string sufixo;
                    bool aux;
                    do
                    {
                        Console.Write("Informe as 3 últimas letras da inscrição da aeronave: ");
                        sufixo = Console.ReadLine();
                        aux = VerificarSufixo(sufixo);
                        if (!aux) Console.WriteLine("SUFIXO INVÁLIDO");
                    } while (sufixo.Length != 3 || !aux);
                    return sufixo.ToUpper();
                }
                public bool VerificarSufixo(String sufixo)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        char aux = sufixo[i];
                        if (Char.IsLetter(aux)) ;
                        else return false;
                    }
                    return true;
                }
                public String SelecionarPrefixo()
                {
                    int prefixo;
                    do
                    {
                        Console.WriteLine("Informe o prefixo da aeronave\n1 - PP\n2 - PT\n3 - PR\n4 - PS\n5 - BR\n0 - Sair");
                        int.TryParse(Console.ReadLine(), out prefixo);
                        switch (prefixo)
                        {
                            case 1:
                                return "PP" + {this.inscricao};
                                break;
                            case 2:
                                return "PT";
                                break;
                            case 3:
                                return "PR";
                                break;
                            case 4:
                                return "PS";
                                break;
                            case 5:
                                return "BR";
                                break;
                            default:
                                Console.WriteLine("PREFIXO INVÁLIDO");
                                break;
                        }
                    } while (prefixo != 0);
                    return "0";
                }
                #endregion
        */
        #region CadastrarAeronave
        public void CadastrarAeronave()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DE AERONAVE <<<");

            /*
            do
            {
                string prefixo = SelecionarPrefixo();

                if(prefixo == "0")
                {
                    Console.WriteLine("Cadastro cancelado! \nPressione Enter para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            } while (true);
            */


        }
        #endregion



    }
}
