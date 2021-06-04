namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Model.Interfaces;

    public abstract class BaseModel : ICreationTimestamp
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAtUtc = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedAtUtc { get; set;}
    }
}
