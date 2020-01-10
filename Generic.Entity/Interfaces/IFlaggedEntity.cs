using System;

namespace Generic.Entity.Interfaces
{
    public interface IFlaggedEntity<T> : ICopyableEntity<T>
    {
        DateTime? DeletedDate { get; set; }
        string DeletedBy { get; set; }
    }
}
