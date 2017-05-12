using System;
using System.Linq.Expressions;
using TokenINFRA.Contexto;

namespace TokenINFRA.Repositorio
{
    public class BaseRepository<T> where T : class
    {
        public TokenContexto Contexto { get; }

        public BaseRepository()
        {
            Contexto = new TokenContexto();
        }

        public void Alterar(T obj, params Expression<Func<T, object>>[] propertiesToUpdate)
        {
            Contexto.Set<T>().Attach(obj);

            foreach (var p in propertiesToUpdate)
            {
                Contexto.Entry(obj).Property(p).IsModified = true;
            }
        }

        public void Salvar()
        {
            Contexto.SaveChanges();
        }
    }
}
