using System;
using Clean.DataAccess.Entities.Users;

namespace Clean.DataAccess.Entities.Base
{
    public class BaseEntity
    {
        public int ActiveStatus { get; set; }
        public DateTime CreatedUtcTime { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedUtcTime { get; set; }
        public long UpdatedById { get; set; }

        //navigation properties
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
