using System;

namespace Generic.Entity.Interfaces
{
    public interface IFlaggedEntity<T> : ICopyableEntity<T>
    {
        bool? IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
        string DeletedBy { get; set; }
    }
}
