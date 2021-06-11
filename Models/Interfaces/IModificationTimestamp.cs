namespace Model.Interfaces
{
    using System;

    public interface IModificationTimestamp
    {
        public DateTime ModifiedAtUtc { get; set; }
    }
}
