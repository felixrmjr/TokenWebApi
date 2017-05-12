using System.Linq;
using TokenINFRA.Entidades;

namespace TokenINFRA.Repositorio
{
    public class RegistrarUsuario : BaseRepository<Usuario>
    {
        public void Adicionar(Usuario registeruser)
        {
            Contexto.Usuarios.Add(registeruser);
            Contexto.SaveChanges();
        }

        public int ObterIdUsuarioLogado(Usuario usuario)
        {
            var retorno = (from tb1 in Contexto.Usuarios
                           where tb1.Nome == usuario.Nome &&
                                 tb1.Senha == usuario.Senha
                           select tb1.Id).FirstOrDefault();

            return retorno;
        }

        public bool ValidarUsuario(Usuario usuario)
        {
            var count = (from tb1 in Contexto.Usuarios
                         where tb1.Nome == usuario.Nome &&
                               tb1.Senha == usuario.Senha
                         select tb1).Count();

            return count > 0;
        }

        public bool ValidarNomeUsuario(Usuario usuario)
        {
            var count = (from tb1 in Contexto.Usuarios
                         where tb1.Nome == usuario.Nome
                         select tb1).Count();
            return count > 0;
        }
    }
}