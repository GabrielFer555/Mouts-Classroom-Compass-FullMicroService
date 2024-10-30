using MCC.Data;
using MCC.Interfaces;
using MCC.Models;

namespace MCC.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Teacher Create(Teacher t)
        {
            _context.Teachers.Add(t);
            _context.SaveChanges();
            return t;
        }

        public void Delete(Guid id)
        {
            var teacher = FindById(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
        }

        public List<Teacher> FindAll()
        {
            var list = _context.Teachers.ToList();
            return list;
        }

        public Teacher FindById(Guid id)
        {
            var teacher = _context.Teachers.FirstOrDefault(g => g.id == id);
            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }
            return teacher;
        }

        public Teacher Update(Teacher t)
        {
            _context.Teachers.Update(t);
            _context.SaveChanges();
            return t;
        }
    }
}
