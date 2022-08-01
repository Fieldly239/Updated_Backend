using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<int> Add(Category model);
        Task<int> Update(Category model);
        Task<int> Delete(int id);
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<int> Add(Category model)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"INSERT INTO [Category]
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

        public override async Task<int> Update(Category model)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"UPDATE [Category]
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
                var sqlCommand = string.Format(@"DELETE FROM [Category] WHERE [id] = @Id");
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }

        private Object ParameterMapping(Category model)
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
