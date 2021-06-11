namespace Model
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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
