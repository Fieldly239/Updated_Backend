using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface IFileAttachmentRepository
    {
        Task<IEnumerable<FileAttachment>> GetAll();
        Task<IEnumerable<FileAttachment>> GetAllByKMId(string kmid);
        Task<FileAttachment> GetById(int id);
        Task<int> Add(FileAttachment model);
        Task<int> Update(FileAttachment model);
        Task<int> Delete(int id);
    }
    public class FileAttachmentRepository : GenericRepository<FileAttachment>, IFileAttachmentRepository
    {
        public FileAttachmentRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<FileAttachment>> GetAllByKMId(string kmid)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"SELECT * FROM [FileAttachment] WHERE FK_KMId = @FK_KMId");
                var resp = await db.QueryAsync<FileAttachment>(sqlCommand, new 
                { 
                    FK_KMId = kmid
                });
                return resp.ToList();
            }
        }
        public override async Task<int> Add(FileAttachment model)
        {
            var sqlCommand = string.Format(@"INSERT INTO [FileAttachment]
                                                       ([FK_KMId]
                                                       ,[FileName]
                                                       ,[FileExtension]
                                                       ,[CreatedBy]
                                                       ,[CreatedDate]
                                                       ,[ModifiedBy]
                                                       ,[ModifiedDate])
                                                 VALUES
                                                       (@FK_KMId
                                                       ,@FileName
                                                       ,@FileExtension
                                                       ,@CreatedBy
                                                       ,@CreatedDate
                                                       ,@ModifiedBy
                                                       ,@ModifiedDate)");
            using (var db = new SqlConnection(connectionStrings))
            {
                return await db.ExecuteAsync(sqlCommand, MappingParameter(model));
            }
        }
        public override async Task<int> Update(FileAttachment model)
        {
            var sqlCommand = string.Format(@"UPDATE [FileAttachment]
                                               SET [FK_KMId] =  @FK_KMId
                                                  ,[FileName] = @FileName
                                                  ,[FileExtension] = @FileExtension
                                                  ,[CreatedBy] = @CreatedBy
                                                  ,[CreatedDate] = @CreatedDate
                                                  ,[ModifiedBy] = @ModifiedBy
                                                  ,[ModifiedDate] = @ModifiedDate
                                             WHERE Id = @Id");
            using (var db = new SqlConnection(connectionStrings))
            {
                return await db.ExecuteAsync(sqlCommand, MappingParameter(model));
            }
        }
        public override async Task<int> Delete(int id)
        {
            var sqlCommand = string.Format(@"DELETE FROM [FileAttachment] WHERE Id = @Id");
            using (SqlConnection db = new SqlConnection(connectionStrings))
            {
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }
        private object MappingParameter(FileAttachment model)
        {
            return new
            {
                Id = model.Id,
                FK_KMId = model.FK_KMId,
                FileName = model.FileName,
                FileExtension = model.FileExtension,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
            };
        }
    }
}
