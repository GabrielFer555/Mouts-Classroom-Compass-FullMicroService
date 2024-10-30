using MCC.Data;
using MCC.Models;
using MCC.Repositories;

namespace MCC.Services
{
    public class GradeService
    {
        private readonly GradeRepository _repository;

        public GradeService(GradeRepository repository)
        {
            _repository = repository;
        }

        public Grade Create(Grade grade)
        {
            return _repository.Create(grade);
        }

        public Grade Get(Guid id)
        {
            var grade = _repository.FindById(id);
            if (grade == null)
            {
                throw new Exception("Grade does not exits");
            }
            return grade;
        }

        public void Update(Grade grade)
        {
            _repository.Update(grade);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public List<Grade> GetAll()
        {
            return (List<Grade>)_repository.FindAll();
        }


        public List<Grade> GetGradeByStudentAndSubjectId(Guid studentId, Guid subjectId)
        {
            return _repository.FindByStudentAndSubjectId(studentId, subjectId);

        }
    }
}
