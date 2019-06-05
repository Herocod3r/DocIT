using System;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using DocIT.Core.Data.Models;


namespace DocIT.Core.Repositories
{
    public interface IBaseRepository<TModel,TQueryModel,Tid> where TModel:DbModel<Tid>
    {
        Task<TModel> GetByIdAsync(Tid id);
        Task<TModel> CreateNewAsync(TModel item);
        Task UpdateAsync(TModel item);
        Task DeleteAsync(TModel item);
        System.Linq.IQueryable<TQueryModel> QueryAsync();
    }
}
