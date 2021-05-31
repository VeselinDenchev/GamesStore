namespace GamesStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class BaseModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAtUtc = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        public DateTime CreatedAtUtc { get; protected set; }
    }
}
