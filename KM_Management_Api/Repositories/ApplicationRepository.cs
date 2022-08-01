using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> GetAll();
        Task<Application> GetById(int id);
        Task<int> Add(Application model);
        Task<int> Update(Application model);
        Task<int> Delete(int id);
    }
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<int> Add(Application model)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"INSERT INTO [Application]
                                                                   ([Name]
                                                                   ,[Description]
                                                                   ,[IsStatus]
                                                                   ,[CreatedDate]
                                                                   ,[ModifiedDate]
                                                                   ,[CreatedBy]
                                                                   ,[ModifiedBy])
                                                             VALUES
                                                                   (@Name
                                                                   ,@Description
                                                                   ,@IsStatus
                                                                   ,@CreatedDate
                                                                   ,@ModifiedDate
                                                                   ,@CreatedBy
                                                                   ,@ModifiedBy)");
                return await db.ExecuteAsync(sqlCommand, ParameterMapping(model));
            }
        }

        public override async Task<int> Update(Application model)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"UPDATE [Application]
                                                       SET [Name] = @Name
                                                          ,[Description] = @Description
                                                          ,[IsStatus] = @IsStatus
                                                          ,[CreatedDate] = @CreatedDate
                                                          ,[ModifiedDate] = @ModifiedDate
                                                          ,[CreatedBy] = @CreatedBy
                                                          ,[ModifiedBy] = @ModifiedBy
                                                 WHERE [Id] = @Id");
                return await db.ExecuteAsync(sqlCommand, ParameterMapping(model));
            }
        }

        public override async Task<int> Delete(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"DELETE FROM [Application] WHERE [id] = @Id");
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }

        private Object ParameterMapping(Application model)
        {
            return new
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsStatus = model.IsStatus,
                CreatedDate = model.CreatedDate,
                ModifiedDate = model.ModifiedDate,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy

            };
        }


    }
}
