using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Passagem_Voo
    {
        public string ID_Passagem { get; set; }
        public string ID_Voo { get; set; }
        public DateTime Data_Ultima_Compra { get; set; }
        public float Valor { get; set; }
        public char Situacao { get; set; }

        public Passagem_Voo()
        {

        }

        ConexaoBanco conn = new();

        #region Cadastrar Passagem
        public void CadastrarPassagem()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DE PASSAGEM <<<");

            if (!GeraIDVenda())
                return;
            Console.WriteLine($"O seu número de ID da passagem: {this.ID_Passagem}");

            //buscando tabela de voos
            Console.WriteLine("\n\t>>> Escolha o voo abaixo: <<<");
            String sql = $"SELECT ID_Voo, Inscricao_Aeronave, Destino, Data_HoraVoo, Data_Cadastro, AssentoOcup, Situacao FROM Voo WHERE Situacao = 'A'; ";
            conn.LocalizarVoo(sql);
            Console.WriteLine("ID do voo selecionado: ");
            ID_Voo = Console.ReadLine();

            Data_Ultima_Compra = DateTime.Now;

            Valor = 1000;

            //Situação
            Console.Write("Situação [L] - Livre / [R] - Reservada / [P] - Paga: ");
            Situacao = char.Parse(Console.ReadLine().ToUpper().Trim());
            while ((this.Situacao.CompareTo('L') != 0) && (this.Situacao.CompareTo('R') != 0) && (this.Situacao.CompareTo('P') != 0))
            {
                Console.WriteLine("\nOpção invalida, digite novamente");
                Console.Write("\nSituação [L] - Livre / [R] - Reservada / [P] - Paga:: ");
                Situacao = char.Parse(Console.ReadLine().ToUpper().Trim());
            }

            //gravar no banco
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                sql = $"INSERT INTO Passagem_Voo (ID_Passagem, ID_Voo, Data_Ultima_Compra, Valor, Situacao) VALUES ('{this.ID_Passagem}', " +
                     $"'{this.ID_Voo}', '{this.Data_Ultima_Compra}', '{this.Valor}', '{this.Situacao}');";

                conn.InsertDados(sql);

                Console.WriteLine("\nGravação efetuada com sucesso! Aperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nGravação não efetuada! Aperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
        }
        #endregion

        #region Select Passagem Especifico
        public void LocalizarPassagem()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Passagem Especifica <<<");
            Console.Write("\nDigite o ID da passagem: ");
            ID_Passagem = Console.ReadLine();

            while (ID_Passagem == "0")
            {
                Console.WriteLine("\nID de passagem inválida. Tente novamente");
                Console.Write("ID Passagem: ");
                ID_Passagem = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT ID_Passagem, ID_Voo, Data_Ultima_Compra, Valor, Situacao FROM Passagem_Voo WHERE ID_Passagem=('{this.ID_Passagem}');";
                conn.LocalizarPassagem(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do voo! Aperte ENTER para retornar ao menu.");
            }
        }
        #endregion

        #region Select Lista de Passagem
        public void ConsultarListaPassagens()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Passagem(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT ID_Passagem, ID_Voo, Data_Ultima_Compra, Valor, Situacao FROM Passagem_Voo;";
                conn.LocalizarPassagem(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dod vôos! Aperte ENTER para retornar ao menu.");
            }

        }
        #endregion

        #region Update Passagem
        public void UpdatePassagem()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados da Passagem <<<");
            Console.Write("\nDigite a ID da passagem: ");
            ID_Passagem = Console.ReadLine();

            sql = $"SELECT ID_Passagem, ID_Voo, Data_Ultima_Compra, Valor, Situacao FROM Passagem_Voo WHERE ID_Passagem=('{this.ID_Passagem}');";

            if (!string.IsNullOrEmpty(conn.LocalizarPassagem(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1 - Data Última compra");
                    Console.WriteLine("2 - Valor ");
                    Console.WriteLine("3 - Situação: ");
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
                            Console.Write("\nAlterar Data da Última compra: ");
                            Data_Ultima_Compra = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Passagem_Voo Set Data_Ultima_Compra=('{this.Data_Ultima_Compra}') Where ID_Passagem=('{this.ID_Passagem}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar valor: ");
                            Valor = float.Parse(Console.ReadLine());
                            sql = $"Update Passagem_Voo Set Valor=('{this.Valor}') Where ID_Passagem=('{this.ID_Passagem}');";
                            break;
                        case 3:
                            Console.WriteLine("\nSituação Atual: ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Passagem_Voo Set Situacao=('{Situacao}') Where ID_Passagem=('{this.ID_Passagem}');";
                            break;

                    }
                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                    conn.UpdateDados(sql);
                }

                else
                {
                    Console.WriteLine("\nNÃO foi possível acionar a operação editar cadastro! Aperte ENTER para retornar ao menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nVoo Não Encontrado! Aperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
        }
        #endregion

        #region Gera ID Passagem
        private bool GeraIDVenda()
        {
            Console.WriteLine("O ID da sua passagem será gerado...\nPressione qualquer tecla para continuar ");
            ID_Voo = conn.TratamentoDado(Console.ReadLine().ToUpper().Trim().Replace("-", ""));

            while (true)
            {
                Random random = new Random();
                ID_Passagem = "PA" + random.Next(0001, 9999).ToString("0000");

                if (conn.VerificaExiste(ID_Passagem, "ID_Passagem", "Passagem_Voo"))
                {
                    Console.WriteLine("Voo Já cadastrado!!!");
                }
                else
                {
                    break;
                }
            }
            return true;
        }
        #endregion
    }
}
