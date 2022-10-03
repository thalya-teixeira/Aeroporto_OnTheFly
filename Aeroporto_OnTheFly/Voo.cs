using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class Voo
    {
        public string ID_Voo { get; set; } //V0000 - Digito inicial V, seguidos de 4 dígitos numéricos.
        public string Inscricao_Aeronave { get; set; }
        public string Destino { get; set; }
        public DateTime Data_HoraVoo { get; set; }

        public DateTime Data_Cadastro { get; set; }
        public int AssentoOcup { get; set; }
        public char Situacao { get; set; } //Caractere (A – Ativo ou C – Cancelado)
        public Aeronave Aeronave { get; set; }

        public Voo()
        {

        }

        public Voo(string id_Voo, string inscricao_Aeronave, string destino, DateTime data_HoraVoo, DateTime data_Cadastro, char situação)
        {
            ID_Voo = id_Voo;
            Inscricao_Aeronave = inscricao_Aeronave;
            Destino = destino;
            Data_HoraVoo = data_HoraVoo;
            Data_Cadastro = data_Cadastro;
            Situacao = situação;
        }

        ConexaoBanco conn = new();

        #region CadastrarAeronave
        public void CadastrarVoo()
        {
            Console.Clear();
            Console.WriteLine(">>> CADASTRO DO VOO <<<");

            if (!CadastraIdVoo())
                return;

            Console.WriteLine($"O seu número de ID Voo: {this.ID_Voo}");

            //destino buscando  tabela disponível de IATAS
            Console.WriteLine("\n\t>>> Escolha o voo abaixo: <<<");
            String sql = $"SELECT Inscricao_Aeronave, CNPJ, Capacidade, Ultima_Venda, Data_Cadastro, Situacao From Aeronave WHERE Situacao = 'A'; ";
            conn.LocalizarAeronave(sql);
            Console.WriteLine("Inscrição da Aeronave selecionada: ");
            Inscricao_Aeronave = Console.ReadLine();

            //Escolher destino
            Console.WriteLine("\n\t>>> Escolha o Destino(s) Abaixo: <<<");
            sql = $"SELECT Siglas, Cidade From Iatas";
            conn.LocalizarIATAS(sql);
            Console.WriteLine("\nDigite IATA desejada : ");
            Destino = Console.ReadLine();

            Data_HoraVoo = DateTime.Now;

            Data_Cadastro = DateTime.Now;

            AssentoOcup = 0;

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
            Console.WriteLine("\nDeseja efetuar a gravação? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                sql = $"INSERT INTO Voo (ID_Voo, Inscricao_Aeronave, Destino, Data_HoraVoo, Data_Cadastro, AssentoOcup, Situacao) VALUES ('{this.ID_Voo}', " +
                     $"'{this.Inscricao_Aeronave}', '{this.Destino}', '{this.Data_HoraVoo}', '{this.Data_Cadastro}', '{this.AssentoOcup}', '{this.Situacao}');";

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

        #region Select Voo Especifico
        public void LocalizarVoo()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Localizar Voo Especifico <<<");
            Console.Write("\nDigite o ID do Voo: ");
            ID_Voo = Console.ReadLine(); //NAO TA LENDO

            while (ID_Voo == "0")
            {
                Console.WriteLine("\nID inválido. Tente novamente");
                Console.Write("ID Voo: ");
                ID_Voo = Console.ReadLine();
            }

            Console.WriteLine("\nDeseja Continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT ID_Voo, Inscricao_Aeronave, Destino, Data_HoraVoo, Data_Cadastro, AssentoOcup, Situacao FROM Voo WHERE ID_Voo=('{this.ID_Voo}');";
                conn.LocalizarVoo(sql);

                Console.WriteLine("\nAperte ENTER para Retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a localização do voo! Aperte ENTER para retornar ao menu.");
            }

        }
        #endregion

        #region Select Lista de Vôos
        public void ConsultarListaVoos()
        {
            Console.Clear();
            Console.WriteLine("\n\t>>> Lista de Voo(s) <<<");
            Console.WriteLine("\nDeseja continuar? Digite 1- Sim / 2-Não: ");
            Console.Write("Digite: ");
            int opc = int.Parse(Console.ReadLine());

            if (opc == 1)
            {
                String sql = $"SELECT ID_Voo, Inscricao_Aeronave, Destino, Data_HoraVoo, Data_Cadastro, AssentoOcup, Situacao FROM Voo WHERE Situacao = 'A';";
                conn.LocalizarVoo(sql);

                Console.WriteLine("\nAperte ENTER para retornar ao menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNÃO foi possível acionar a consulta dod vôos! Aperte ENTER para retornar ao menu.");
            }

        }
        #endregion

        #region Update Voo
        public void UpdateVoo()
        {
            int opc = 0;
            String sql = "";
            Console.Clear();
            Console.WriteLine("\n\t>>> Editar Dados do Voo <<<");
            Console.Write("\nDigite a ID do Voo: ");
            ID_Voo = Console.ReadLine();

            sql = $"SELECT ID_Voo, Inscricao_Aeronave, Destino, Data_HoraVoo, Data_Cadastro, AssentoOcup, Situacao FROM Voo WHERE ID_Voo=('{this.ID_Voo}');";

            if (!string.IsNullOrEmpty(conn.LocalizarVoo(sql)))
            {
                Console.WriteLine("\nDeseja Efetuar a Alteração? Digite 1- Sim / 2- Não: ");
                Console.Write("Digite: ");
                opc = int.Parse(Console.ReadLine());

                if (opc == 1)
                {
                    Console.WriteLine("\nSelecione a opção que deseja editar");
                    Console.WriteLine("1 - Iata");
                    Console.WriteLine("2 - Data/Hora Voo");
                    Console.WriteLine("3 - Assentos Ocupados ");
                    Console.WriteLine("4 - Situação: ");
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
                            Console.Write("\nAlterar o Iata: ");
                            Destino = Console.ReadLine();
                            sql = $"Update Voo Set Iatas=('{this.Destino}') Where IDVoo=('{this.ID_Voo}');";
                            break;
                        case 2:
                            Console.Write("\nAlterar a Data/Hora: ");
                            Data_HoraVoo = DateTime.Parse(Console.ReadLine());
                            sql = $"Update Voo Set Data_HoraVoo=('{this.Data_HoraVoo}') Where IDVoo=('{this.ID_Voo}');";
                            break;
                        case 3:
                            Console.WriteLine("\nAssentos Ocupados: ");
                            this.AssentoOcup = int.Parse(Console.ReadLine());
                            sql = $"Update Voo Set Assentos_Ocupados=('{this.AssentoOcup}') Where IDVoo=('{this.ID_Voo}');";
                            break;
                        case 4:
                            Console.WriteLine("\nSituação Atual: ");
                            this.Situacao = char.Parse(Console.ReadLine());
                            sql = $"Update Voo Set Situacao=('{this.Situacao}') Where IDVoo=('{this.ID_Voo}');";
                            break;

                    }
                    Console.WriteLine("\nCadastro alterado com sucesso!!!! Aperte ENTER para retornar ao Menu.");
                    Console.ReadKey();
                    conn.UpdateDados(sql);
                }

                else
                {
                    Console.WriteLine("\nNÃO foi possível acionar a operação editar cadastro! Aperte ENTER para retornar ao Menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nVoo Não Encontrado! Aperte ENTER para retornar ao Menu.");
                Console.ReadKey();
            }
        }
        #endregion

        #region Cadastrar ID Voo
        private bool CadastraIdVoo()
        {
            Console.WriteLine("O ID do seu Voo será gerado...\nPressione qualquer tecla para continuar ");
            ID_Voo = conn.TratamentoDado(Console.ReadLine().ToUpper().Trim().Replace("-", ""));

            while (true)
            {
                Random random = new Random();
                ID_Voo = "V" + random.Next(0001, 9999).ToString("0000");

                if (conn.VerificaExiste(ID_Voo, "ID_Voo", "Voo"))
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
