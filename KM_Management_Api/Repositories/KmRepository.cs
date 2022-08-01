using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface IKMRepository
    {
        Task<IEnumerable<KM>> GetAll();
        Task<IEnumerable<KM>> GetByApplication(int id);

        Task<IEnumerable<KM>> GetByCategory(int id);

        Task<IEnumerable<KM>> GetByStatus(int id);

        Task<IEnumerable<KMTopListView>> GetTopKnowLedge();



        Task<KM> GetById(int id);
        Task<int> Add(KM model);
        Task<int> Update(KM model);
        Task<int> Delete(int id);
      
    }

    public class KMRepository : GenericRepository<KM>, IKMRepository
    {
        public KMRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<KM>> GetByApplication(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"SELECT * FROM [KM] WHERE [FK_ApplicationId] = @FK_ApplicationId");
                var resp = await db.QueryAsync<KM>(sqlCommand, new { FK_ApplicationId = id });
                return resp.ToList();
            }
        }

        public async Task<IEnumerable<KM>> GetByCategory(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"SELECT * FROM [KM] WHERE [FK_CategoryId] = @FK_CategoryId");
                var resp = await db.QueryAsync<KM>(sqlCommand, new { FK_CategoryId = id });
                return resp.ToList();
            }
        }

        public async Task<IEnumerable<KM>> GetByStatus(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"SELECT * FROM [KM] WHERE [FK_StatusId] = @FK_StatusId");
                var resp = await db.QueryAsync<KM>(sqlCommand, new { FK_StatusId = id });
                return resp.ToList();
            }
        }



        public override async Task<int> Add(KM model)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"INSERT INTO [KM]
                                                           ([KeyName]
                                                           ,[Title]
                                                           ,[Description]
                                                           ,[IsStatus]
                                                           ,[IsFavorite]
                                                           ,[ApproveDate]
                                                           ,[Remark]
                                                           ,[FK_StatusId]
                                                           ,[FK_ApplicationId]
                                                           ,[FK_CategoryId]
                                                           ,[CreatedDate]
                                                           ,[ModifiedDate]
                                                           ,[CreatedBy]
                                                           ,[ModifiedBy])
                                                     VALUES
                                                           (@KeyName
                                                           ,@Title
                                                           ,@Description
                                                           ,@IsStatus
                                                           ,@IsFavorite
                                                           ,@ApproveDate
                                                           ,@Remark
                                                           ,@FK_StatusId
                                                           ,@FK_ApplicationId
                                                           ,@FK_CategoryId
                                                           ,@CreatedDate
                                                           ,@ModifiedDate
                                                           ,@CreatedBy
                                                           ,@ModifiedBy)");

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(model));
            }
        }

        public override async Task<int> Update(KM model)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"UPDATE [KM]
                                                   SET [KeyName] = @KeyName
                                                      ,[Title] = @Title
                                                      ,[Description] = @Description
                                                      ,[IsStatus] = @IsStatus
                                                      ,[IsFavorite] = @IsFavorite
                                                      ,[ApproveDate] = @ApproveDate
                                                      ,[Remark] = @Remark
                                                      ,[FK_StatusId] = @FK_StatusId
                                                      ,[FK_ApplicationId] = @FK_ApplicationId
                                                      ,[FK_CategoryId] = @FK_CategoryId
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
                var sqlCommand = string.Format(@"DELETE FROM [KM] WHERE [id] = @Id");
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }

       

        private Object ParameterMapping(KM model)
        {
            return new
            {
                Id = model.Id,
                KeyName = model.KeyName,
                Title = model.Title,
                Description = model.Description,
                IsStatus = model.IsStatus,
                IsFavorite = model.IsFavorite,
                ApproveDate = model.ApproveDate,
                Remark = model.Remark,
                FK_StatusId = model.FK_StatusId,
                FK_ApplicationId = model.FK_ApplicationId,
                FK_CategoryId = model.FK_CategoryId,
                CreatedDate = model.CreatedDate,
                ModifiedDate = model.ModifiedDate,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy

            };
        }

        public async Task<IEnumerable<KMTopListView>> GetTopKnowLedge()
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"SELECT   k.[Id]
                                                         ,k.[KeyName]
                                                         ,k.[Title]
                                                         ,k.[Description]
                                                         ,k.[IsStatus]
                                                         ,k.[IsFavorite]
                                                         ,k.[ApproveDate]
                                                         ,k.[Remark]
                                                         ,k.[FK_StatusId]
                                                         ,k.[FK_ApplicationId] AS ApplicationId
                                                         ,k.[FK_CategoryId] AS CategoryId
                                                         ,k.[CreatedDate]
                                                         ,k.[ModifiedDate]
                                                         ,k.[CreatedBy]
                                                         ,k.[ModifiedBy]
                                                         ,a.[Name] AS ApplicationName
                                                         ,c.[Name] AS CategoryName
                                                         FROM [KM] as k
                                                         LEFT JOIN [Application] AS a ON a.id = k.FK_ApplicationId
                                                         LEFT JOIN [CATEGORY] AS c ON c.id = k.FK_CategoryId ");
                var resp = await db.QueryAsync<KMTopListView>(sqlCommand, new { });
                return resp.ToList();
            }
        }
    }
}
