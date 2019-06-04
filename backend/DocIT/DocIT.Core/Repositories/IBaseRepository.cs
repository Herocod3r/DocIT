using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace DocIT.Core.Repositories
{
    public interface IBaseRepository<Tmodel,Tid>
    {
        Task<Tmodel> GetByIdAsync(Tid id);
        Task<(List<Tmodel>, Int64)> ListAllAsync();
        Task<Tmodel> CreateNewAsync(Tmodel item);
        Task<Tmodel> UpdateAsync(Tmodel item);
        Task DeleteAsync(Tmodel item);

    }
}
