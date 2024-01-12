using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Search.Persistence.Contracts
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        Task SaveAsync(TEntity document);

        Task UpdateAsync(TEntity document, Func<TEntity, string> getId);
    }
}
