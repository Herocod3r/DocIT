using System;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using DocIT.Core.Data.Models;


namespace DocIT.Core.Repositories
{
    public interface IBaseRepository<TModel,TQueryModel,Tid> where TModel:DbModel<Tid>
    {
        TModel GetById(Tid id);
        TModel CreateNew(TModel item);
        void Update(TModel item);
        void Delete(TModel item);
        System.Linq.IQueryable<TQueryModel> QueryAsync();
        System.Linq.IQueryable<TModel> ObjectQuery { get; }
    }
}
