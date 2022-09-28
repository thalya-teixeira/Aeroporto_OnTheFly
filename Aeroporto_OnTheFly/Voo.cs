using System;
using System.Collections.Generic;
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
        public string Situação { get; set; } //Caractere (A – Ativo ou C – Cancelado)
        public Aeronave Aeronave { get; set; }

        public Voo()
        {

        }

        public Voo(string id_Voo, string inscricao_Aeronave, string destino, DateTime data_HoraVoo, DateTime data_Cadastro, string situação)
        {
            ID_Voo = id_Voo;
            Inscricao_Aeronave = inscricao_Aeronave;
            Destino = destino;
            Data_HoraVoo = data_HoraVoo;
            Data_Cadastro = data_Cadastro;
            Situação = situação;
        }


    }
}
