using Microsoft.VisualBasic;

namespace AAD_CRUD.Models
{
    public class Funcionario
    {
        public int NIF { get; set; } 
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime DataNasc { get; set;}
        public DateTime DataAdmissao { get; set; }

        public string Morada { get; set; }

        public string Email { get; set; }

        public int CPCP { get; set; }
        public string Localidade { get; set; }

    }
}
