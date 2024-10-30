using MCC.Data;
using MCC.Interfaces;
using MCC.Models;

namespace MCC.Repositories
{
    public class SubjectRepository : IRepository<Subject>
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Subject Create(Subject t)
        {
            _context.Subjects.Add(t);
            _context.SaveChanges();
            return t;
        }

        public void Delete(Guid id)
        {
            var subject = FindById(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                _context.SaveChanges();
            }
        }

        public List<Subject> FindAll()
        {
            var list = _context.Subjects.ToList();
            return list;
        }

        public Subject FindById(Guid id)
        {
            var subject = _context.Subjects.FirstOrDefault(g => g.id == id);
            if (subject == null)
            {
                throw new Exception("Subject not found");
            }
            return subject;
        }

        public Subject Update(Subject t)
        {
            _context.Subjects.Update(t);
            _context.SaveChanges();
            return t;
        }
    }
}
