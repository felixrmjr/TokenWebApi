using System;

namespace TokenINFRA.Entidades
{
    public class Token
    {
        public int Id   { get; set; }
        public string TokenStr  { get; set; }
        public DateTime Emissao  { get; set; }
        public DateTime Expiracao { get; set; }
        public DateTime CriadoEm { get; set; }

        //public virtual Empresa Empresa { get; set; }
    }
}
