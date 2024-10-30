using MCC.Models;
using MCC.Repositories;

namespace MCC.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _repository;

        public TeacherService(TeacherRepository repository)
        {
            _repository = repository;
        }

        public Teacher Create(Teacher teacher)
        {
            return _repository.Create(teacher);
        }

        public Teacher Get(Guid id)
        {
            var teacher = _repository.FindById(id);
            if (teacher == null)
            {
                throw new Exception("Teacher does not exits");
            }
            return teacher;
        }

        public void Update(Teacher teacher)
        {
            _repository.Update(teacher);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public List<Teacher> GetAll()
        {
            return (List<Teacher>)_repository.FindAll();
        }
    }
}
