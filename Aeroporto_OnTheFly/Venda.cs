using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Venda
    {
        public string ID_Venda { get; set; }
        public string CPF { get; set; }
        public DateTime Data_Venda { get; set; }
        public float Valor_Total { get; set; }
        public string ID_Passagem { get; set; }
        public ConexaoBanco Banco { get; set; }
        public Venda()
        {

        }

        ConexaoBanco conn = new();

        #region Cadastrar Venda
        public void CadastrarVenda()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DA VENDA <<<");

            if (!GeraIDVenda())
                return;

            Console.WriteLine($"\nO número de ID da sua venda: {this.ID_Venda}\nPressione ENTER para continuar...");
            Console.ReadKey();

            Console.WriteLine("\n\t>>> Escolha o Passageiro: <<<");
            Console.WriteLine("Pressione ENTER para visualizar a lista de passageiros ATIVOS");
            Console.ReadKey();
            String sql = $"SELECT CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao From Passageiro WHERE Situacao = 'A';";
            conn.LocalizarDadosPassageiro(sql);

            if (!VerificaCPF())
                return;

            Data_Venda = DateTime.Now;

            Console.Write("Informe o valor unitário: ");
            float ValorUnitario = float.Parse(Console.ReadLine());

            int quantidade;
            do
            {
                Console.Write("\nDigite a quantidade de passagem que deseja comprar: [máx 4 passagens]");
                quantidade = int.Parse(Console.ReadLine());

                if (quantidade > 4 || quantidade <= 0)
                {
                    Console.WriteLine("Impossivel comprar mais que 4 passagens!");
                }
            } while (quantidade > 4 || quantidade <= 0);

            Console.WriteLine("\n\t>>> Escolha a Passagem: <<<");
            sql = $"SELECT ID_Passagem, ID_Voo, Data_Ultima_Compra, Valor, Situacao From Passagem_Voo WHERE Situacao = 'L';";
            conn.LocalizarPassagem(sql);
            if (!VerificaIDPassagem())
                return;

            Valor_Total = ValorUnitario * quantidade;

            for (int i = 1; i <= quantidade; i++)
            {
                CadastroItemVenda();
            }
        }
        #endregion

        #region Cadastrar Item Venda
        public void CadastroItemVenda()
        {

            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());
            if (opc == 1)
            {
                String sql = $"INSERT INTO Venda (ID_Venda, CPF, Data_Venda, Valor_Total, ID_Passagem) VALUES ('{this.ID_Venda}' , " +
                     $"'{this.CPF}', '{this.Data_Venda}', '{this.Valor_Total}', '{this.ID_Passagem}');";
                conn.InsertDados(sql);
                Console.WriteLine("\nGravação efetuada com sucesso! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nGravação não efetuada! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
            Console.WriteLine("Deseja  atualizar a situação da(s) Passagem(s) selecionada(s)? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            opc = int.Parse(Console.ReadLine());
            if (opc == 1)
            {
                Passagem_Voo editPass = new();
                editPass.EditarSituacaoPassagem();
            }
            else
            {
                Console.WriteLine("\n Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
        }
        #endregion

        #region Select Venda Especifica
        public void LocalizarVenda()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Venda Especifica <<<");
            Console.Write("\nDigite o ID da venda: ");
            ID_Venda = Console.ReadLine();

            while (ID_Venda == "0")
            {
                Console.WriteLine("\nID da venda inválido. Tente novamente");
                Console.Write("ID Passagem: ");
                ID_Venda = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT ID_Venda, CPF, Data_Venda, Valor_Total, ID_Passagem FROM Venda WHERE ID_Venda=('{this.ID_Venda}');";
                conn.LocalizarVenda(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do voo! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Select Lista de Venda
        public void ConsultarListaVenda()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Passagem(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT ID_Venda, CPF, Data_Venda, Valor_Total, ID_Passagem FROM Venda ;";
                conn.LocalizarVenda(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dod vôos! Aperte ENTER para retornar ao menu.");
            }

        }
        #endregion

        #region Update Venda
        public void UpdateVenda()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados da Venda <<<");
            Console.Write("\nDigite a ID da Venda: ");
            ID_Venda = Console.ReadLine();

            sql = $"SELECT ID_Venda, CPF, Data_Venda, Valor_Total, ID_Passagem FROM Venda WHERE ID_Venda=('{this.ID_Venda}');";

            if (!string.IsNullOrEmpty(conn.LocalizarVenda(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1 - Data da venda");
                    Console.WriteLine("2 - Valor Total ");
                    Console.Write("\nDigite: ");
                    opc = int.Parse(Console.ReadLine());
                    while (opc < 1 || opc > 2)
                    {
                        Console.WriteLine("\nDigite uma opção válida:");
                        Console.Write("\nDigite: ");
                        opc = int.Parse(Console.ReadLine());

                    }

                    switch (opc)
                    {
                        case 1:
                            Console.Write("\nAlterar Data da Venda: ");
                            Data_Venda = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Venda SET Data_Venda=('{this.Data_Venda}') WHERE ID_Venda=('{this.ID_Venda}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar valor total: ");
                            Valor_Total = float.Parse(Console.ReadLine());
                            sql = $"Update Venda SET Valor_Total=('{this.Valor_Total}') WHERE ID_Venda=('{this.ID_Venda}');";
                            break;
                    }
                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                    conn.UpdateDados(sql);
                }
                else
                {
                    Console.WriteLine("\nNÃO foi possível adicionar a operação editar no cadastro! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nVenda não encontrada! Aperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
        }
        #endregion

        #region Verifica CPF Existe
        private bool VerificaCPF()
        {
            do
            {
                Console.Write("\nDigite o CPF do Passageiro : ");
                CPF = Console.ReadLine();


                Console.WriteLine("\n\tVerificação de CPF Restrito: ");
                if (conn.LocalizarBloqueados(CPF, "CPF", "Cadastro_Restrito"))
                {
                    Console.WriteLine("CPF Restrito! Não é possível efetuar venda. Tecle Enter para retornar ao menu!");
                    Console.ReadKey();
                    return false;
                }
                else
                {
                    Console.WriteLine("CPF Apto!!!!");
                    if (conn.VerificaExiste(CPF, "CPF", "Passageiro") == false)
                    {
                        Console.WriteLine("O CPF não existe no cadastro de passageiros. Informe um CPF válido");
                    }
                }
            } while (conn.VerificaExiste(CPF, "CPF", "Passageiro") == false);

            Console.WriteLine($"\nCPF do passageiro encontrado: {this.CPF} continue sua compra...");

            return true;
        }
        #endregion

        #region Verifica ID Passagem
        private bool VerificaIDPassagem()
        {
            do
            {
                Console.Write("\nDigite o ID da passagem: ");
                ID_Passagem = Console.ReadLine();

                if (conn.VerificaExiste(ID_Passagem, "ID_Passagem", "Passagem_Voo") == false)
                {
                    Console.WriteLine("O ID da passagem não existe no cadastro passagens. Informe um ID válido");
                }

            } while (conn.VerificaExiste(ID_Passagem, "ID_Passagem", "Passagem_Voo") == false);

            Console.WriteLine($"\nID da passagem encontrado: {this.ID_Passagem} continue sua compra");

            return true;
        }
        #endregion

        #region Gera ID Venda
        public bool GeraIDVenda()
        {
            for (int i = 1; i <= 999; i++)
            {
                if (!conn.VerificaExiste(i.ToString(), "ID_Venda", "Venda"))
                {
                    ID_Venda = i.ToString();
                    break;
                }
            }
            return true;
        }
        #endregion
    }
}
