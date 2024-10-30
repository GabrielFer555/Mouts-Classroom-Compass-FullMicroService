using MCC.Repositories;
using MCC.Data;

using Microsoft.EntityFrameworkCore;
using MCC.Models;

namespace MCC.Services
{
    public class ValidationService
    {
        private readonly ApplicationDbContext _context;

        public ValidationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public object ValidateData(Guid subjectId, Guid teacherId, Guid studentId)
        {
            var result = _context.ValidationDto
            .FromSqlRaw(@"
                SELECT s.name AS StudentName, t.name AS TeacherName, sb.subject_name AS SubjectName
                FROM student s
                JOIN subject sb ON sb.id = {0} AND sb.assigned_teacher = {1}
                JOIN teacher t ON t.id = sb.assigned_teacher
                WHERE s.id = {2}",
                subjectId, teacherId, studentId)
            .AsEnumerable()
            .FirstOrDefault();

            if (result == null)
            {
                throw new Exception("id does not exits");
            }
            return result;
        }
    }
}
