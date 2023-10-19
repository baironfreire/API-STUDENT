using API.Models;
using API.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementation
{
    public class QualificationService: IQualificationService
    {
        private StudentsContext _dbContext;
        public QualificationService(
             StudentsContext dbContext
        )
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> Delete(Qualification model)
        {
            try
            {
                this._dbContext.Qualifications.Remove(model);
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Qualification> Get(int id)
        {
            try
            {
                Qualification? qualification = new Qualification();
                qualification = await this._dbContext.Qualifications.Where(st => st.QualificationsId == id).FirstOrDefaultAsync();
                return qualification;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Qualification>> List()
        {
            try
            {
                List<Qualification> qualifications = new List<Qualification>(); ;
                qualifications = await _dbContext.Qualifications.ToListAsync();
                return qualifications;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Qualification> Save(Qualification model)
        {
            try
            {
                this._dbContext.Qualifications.Add(model);
                await this._dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Qualification model)
        {
            try
            {
                this._dbContext.Qualifications.Update(model);
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
