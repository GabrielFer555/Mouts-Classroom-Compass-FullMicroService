namespace MCC.Interfaces
{
    public interface IRepository<T>
    {
        public abstract T Create(T t);
        public abstract T Update(T t);
        public abstract void Delete(Guid id);
        public abstract List<T> FindAll();
        public abstract T FindById(Guid id);
    }
}
