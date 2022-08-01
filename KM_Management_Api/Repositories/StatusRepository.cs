using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAll();

        Task<Status> GetById(int id);
        Task<int> Add(Status status);
        Task<int> Update(Status status);
        Task<int> Delete(int id);
    }
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        private readonly IConfiguration _configuration;
        public StatusRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<int> Add(Status status)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"INSERT INTO [Status]
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

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(status));
            }
        }

        public override async Task<int> Update(Status status)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"UPDATE [Status]
                                                   SET [Name] = @Name
                                                      ,[Description] = @Description
                                                      ,[IsStatus] = @IsStatus
                                                      ,[CreatedDate] = @CreatedDate
                                                      ,[ModifiedDate] = @ModifiedDate
                                                      ,[CreatedBy] = @CreatedBy
                                                      ,[ModifiedBy] = @ModifiedBy
                                                 WHERE [Id] = @Id");

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(status));
            }
        }

        public override async Task<int> Delete(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"DELETE FROM [Status] WHERE [id] = @Id");
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }



        private Object ParameterMapping(Status status)
        {
            return new
            {
                Id = status.Id,
                Name = status.Name,
                Description = status.Description,
                IsStatus = status.IsStatus,
                CreatedDate = status.CreatedDate,
                ModifiedDate = status.ModifiedDate,
                CreatedBy = status.CreatedBy,
                ModifiedBy = status.ModifiedBy
            };
        }
    }
}
