namespace Model.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModificationTimestamp
    {
        public DateTime ModifiedAtUtc { get; set; }
    }
}
