using System;

namespace TokenINFRA.Entidades
{
    public class Usuario
    {
        //public Usuario()
        //{
        //    Empresas = new List<Empresa>();
        //}

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime CriadoEm { get; set; }

        //public ICollection<Empresa> Empresas { get; set; }
    }
}