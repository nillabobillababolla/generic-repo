namespace Generic.Entity.Interfaces
{
    public interface ICopyableEntity<T> : IEntity<T>
    {
        T ShallowCopy();

        T DeepCopy();
    }
}
