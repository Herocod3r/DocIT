using System;
using System.Collections.Generic;



namespace DocIT.Core.Data.ViewModels
{
    public class ListViewModel<T>
    {
        public List<T> Result { get; set; }
        public Int64 Total { get; set; }
    }
}
