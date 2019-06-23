using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocIT.Core.Repositories.Implementations
{
    public class BaseRepository<TModel, TQueryModel, Tid> : IBaseRepository<TModel,TQueryModel,Tid>
         where TModel : DbModel<Tid>
    {
        public BaseRepository(IMongoDatabase dbContext)
        {


            this.Collection = dbContext.GetCollection<TModel>(typeof(TModel).Name);
        }

        protected IMongoCollection<TModel> Collection { get; }

        public TModel CreateNew(TModel item)
        {
            item.DateCreated = DateTime.Now;
            Collection.InsertOne(item);
            return item;
        }

        public void Delete(TModel item) =>  Collection.DeleteOne(x => x.Id.Equals(item.Id));

        public TModel GetById(Tid id)
        {
            var items = Collection.Find(x => x.Id.Equals(id));
            return items.FirstOrDefault();
        }



        public void Update(TModel item) => Task.Run(()=> Collection.ReplaceOne(x => x.Id.Equals(item.Id), item));

        public IQueryable<TQueryModel> QueryAsync() => ProjectedSource;




        protected virtual IQueryable<TQueryModel> ProjectedSource { get; }

        public IQueryable<TModel> ObjectQuery => Collection.AsQueryable();
    }
}
