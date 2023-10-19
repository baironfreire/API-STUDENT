using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Services.Contract;

namespace API.Services.Implementation
{
    
    public class StudentService: IStudentService
    {
        private StudentsContext _dbContext;
        public StudentService(
            StudentsContext dbContext
        )
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> Delete(Student model)
        {
            try
            {
                this._dbContext.Students.Remove( model );
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> Get(int id)
        {
            try
            {
                Student? student = new Student();
                student = await this._dbContext.Students.Where(st=>st.StudentId == id).FirstOrDefaultAsync();
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Student>> List()
        {
            try
            {
                List<Student> students = new List<Student>();
                students= await _dbContext.Students.ToListAsync();
                return students;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> Save(Student model)
        {
            try
            {
                this._dbContext.Students.Add(model);
                await this._dbContext.SaveChangesAsync();
                return model;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Student model)
        {
            try
            {
                this._dbContext.Students.Update(model);
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> Details(int id)
        {
            try
            {
                Student student = await this._dbContext.Students
                    .Include(s => s.Qualifications)
                    .FirstOrDefaultAsync(m => m.StudentId == id);
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
