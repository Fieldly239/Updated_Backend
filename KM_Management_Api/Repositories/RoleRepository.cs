using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll();

        Task<Role> GetById(int id);
        Task<int> Add(Role role);
        Task<int> Update(Role role);
        Task<int> Delete(int id);
    }
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly IConfiguration _configuration;
        public RoleRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<int> Add(Role role)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"INSERT INTO [Role]
                                                                   ([KeyName]
                                                                   ,[Name]
                                                                   ,[Description]
                                                                   ,[IsStatus])
                                                             VALUES
                                                                   (@KeyName
                                                                   ,@Name
                                                                   ,@Description
                                                                   ,@IsStatus)");

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(role));
            }
        }

        public override async Task<int> Update(Role role)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"UPDATE [Role]
                                                   SET [KeyName] = @KeyName
                                                      ,[Name] = @Name
                                                      ,[Description] = @Description
                                                      ,[IsStatus] = @IsStatus
                                                 WHERE [Id] = @Id");

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(role));
            }
        }

        public override async Task<int> Delete(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"DELETE FROM [Role] WHERE [id] = @Id");
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }



        private Object ParameterMapping(Role role)
        {
            return new
            {
                Id = role.Id,
                KeyName = role.KeyName,
                Name = role.Name,
                Description = role.Description,
                IsStatus = role.IsStatus
            };
        }
    }
}
