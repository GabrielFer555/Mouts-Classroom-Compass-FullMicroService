using MCC.Data;
using MCC.Interfaces;
using MCC.Models;

namespace MCC.Repositories
{
    public class GradeRepository : IRepository<Grade>
    {
        private readonly ApplicationDbContext _context;

        public GradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Grade Create(Grade t)
        {
            _context.Grades.Add(t);
            _context.SaveChanges();
            return t;
        }

        public void Delete(Guid id)
        {
            var grade = FindById(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                _context.SaveChanges();
            }
        }

        public List<Grade> FindAll()
        {
            var list = _context.Grades.ToList();
            return list;
        }

        public Grade FindById(Guid id)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.id == id);

            if (grade == null)
                throw new Exception("Grade not found");
            
            return grade;
        }

        public Grade Update(Grade t)
        {
            _context.Grades.Update(t);
            _context.SaveChanges();
            return t;
        }

        public List<Grade> FindByStudentAndSubjectId(Guid StudentId, Guid SubjectId)
        {
            var grades = _context.Grades
                                 .Where(g => g.student_id == StudentId && g.subject_id == SubjectId)
                                 .ToList();

            if (!grades.Any())
                throw new Exception("No grades found");
            

            return grades;
        }

    }
}
