using System;

namespace TokenINFRA.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Encarregado { get; set; }
        public DateTime CriadoEm { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        //public virtual Usuario Usuario { get; set; }

    }
}
