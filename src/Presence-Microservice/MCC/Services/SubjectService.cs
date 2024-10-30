using MCC.Models;
using MCC.Repositories;

namespace MCC.Services
{
    public class SubjectService
    {
        private readonly SubjectRepository _repository;

        public SubjectService(SubjectRepository repository)
        {
            _repository = repository;
        }

        public Subject Create(Subject subject)
        {
            return _repository.Create(subject);
        }

        public Subject Get(Guid id)
        {
            var subject = _repository.FindById(id);
            if (subject == null)
            {
                throw new Exception("Subject does not exits");
            }
            return subject;
        }

        public void Update(Subject subject)
        {
            _repository.Update(subject);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public List<Subject> GetAll()
        {
            return (List<Subject>)_repository.FindAll();
        }
    }
}
