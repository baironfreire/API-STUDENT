using API.Models;

namespace API.Services.Contract
{
    public interface IStudentService: IGenericService<Student>
    {
        public Task<Student> Details(int id);
    }
}
