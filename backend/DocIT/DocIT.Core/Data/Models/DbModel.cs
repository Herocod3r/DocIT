using System;
namespace DocIT.Core.Data.Models
{
    public abstract class DbModel<Tid>
    {
        public Tid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
