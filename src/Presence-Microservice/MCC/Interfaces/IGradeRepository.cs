namespace MCC.Interfaces
{
    public interface IGradeRepository<T>
    {
        public abstract T Create(T t);
        public abstract T Update(T t);
        public abstract void Delete(Guid studentId, Guid subjectId);
        public abstract List<T> FindAll();
        public abstract T FindById(Guid studentId, Guid subjectId);
    }
}
