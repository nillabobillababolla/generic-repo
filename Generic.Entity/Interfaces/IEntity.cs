using System;

namespace Generic.Entity.Interfaces
{
    public interface IEntity : IModifiableEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        byte[] Version { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }

    public interface IEntityWithCopyMethods<T> : IEntity<T>
    {
        T ShallowCopy();

        T DeepCopy();
    }

}