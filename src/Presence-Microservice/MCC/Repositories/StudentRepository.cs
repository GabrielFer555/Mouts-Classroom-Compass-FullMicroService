using MCC.Data;
using MCC.Interfaces;
using MCC.Models;

namespace MCC.Repositories
{
    public class StudentRepository: IRepository<Student>
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Student Create(Student t)
        {
            _context.Students.Add(t);
            _context.SaveChanges();
            return t;
        }

        public void Delete(Guid id)
        {
            var student = FindById(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public List<Student> FindAll()
        {
            List<Student> list = [.. _context.Students];
            return list;
        }

        public Student FindById(Guid id)
        {
            var student = _context.Students.FirstOrDefault(g => g.id == id);
            if (student == null){
                throw new Exception("Student not found");
            }
            return student;
        }

        public Student Update(Student t)
        {
            _context.Students.Update(t);
            _context.SaveChanges();
            return t;
        }
    }
}
