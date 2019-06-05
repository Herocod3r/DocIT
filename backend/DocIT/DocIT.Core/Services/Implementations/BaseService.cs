using System;
namespace DocIT.Core.Services.Implementations
{
    public class BaseService
    {
        public BaseService()
        {
        }

        protected T1 Map<T1,T2>(T2 obj)
        {
            return AutoMapper.Mapper.Map<T1>(obj);
        }

    }
}
