using MCC.Models;
using MCC.Repositories;

namespace MCC.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public Student Create(Student student)
        {
            return _repository.Create(student);
        }

        public Student Get(Guid id)
        {
            var student = _repository.FindById(id);
            if (student == null)
            {
                throw new Exception("Student does not exits");
            }
            return student;
        }

        public void Update(Student student)
        {
            _repository.Update(student);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public List<Student> GetAll()
        {
            return (List<Student>)_repository.FindAll();
        }

    }
}
