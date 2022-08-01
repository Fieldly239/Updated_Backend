using Dapper;
using KM_Management_Api.Models;
using System.Data.SqlClient;

namespace KM_Management_Api.Repositories
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<UserInfo>> GetAll();

        Task<UserInfo> GetById(int id);
        Task<int> Add(UserInfo userInfo);
        Task<int> Update(UserInfo userInfo);
        Task<int> Delete(int id);
    }
    public class UserInfoRepository : GenericRepository<UserInfo>, IUserInfoRepository
    {
        private readonly IConfiguration _configuration;
        public UserInfoRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override async Task<int> Add(UserInfo userInfo)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"INSERT INTO [UserInfo]
                                                                   ([EmpId]
                                                                   ,[FK_RoleId]
                                                                   ,[IsStatus]
                                                                   ,[TitleId]
                                                                   ,[TitleName_Th]
                                                                   ,[TitleName_En]
                                                                   ,[Name_Th]
                                                                   ,[Name_En]
                                                                   ,[SurName_Th]
                                                                   ,[SurName_En]
                                                                   ,[Sex]
                                                                   ,[Position_Id]
                                                                   ,[PositionDesc_Th]
                                                                   ,[PositionDesc_En]
                                                                   ,[Dim_Branch]
                                                                   ,[Branch_ID]
                                                                   ,[Branch]
                                                                   ,[Branch_Th]
                                                                   ,[Branch_Desc]
                                                                   ,[Area]
                                                                   ,[GrmGroup]
                                                                   ,[Department_Id]
                                                                   ,[Department_TH]
                                                                   ,[Department_EN]
                                                                   ,[Faction_Id]
                                                                   ,[Faction_Th]
                                                                   ,[Faction_EN]
                                                                   ,[Empst]
                                                                   ,[User_Email]
                                                                   ,[User_Uid]
                                                                   ,[PP_Emplid]
                                                                   ,[Afs_Branch_Code]
                                                                   ,[Startdate]
                                                                   ,[Enddate]
                                                                   ,[Approver]
                                                                   ,[Approver_Name]
                                                                   ,[B_Manager_Id]
                                                                   ,[B_Name]
                                                                   ,[Employee_Type]
                                                                   ,[Phone]
                                                                   ,[Extension]
                                                                   ,[Pref_First_Name]
                                                                   ,[IsCanBehaft]
                                                                   ,[IsAcceptTerms])
                                                             VALUES
                                                                   (@EmpId
                                                                   ,@FK_RoleId
                                                                   ,@IsStatus
                                                                   ,@TitleId
                                                                   ,@TitleName_Th
                                                                   ,@TitleName_En
                                                                   ,@Name_Th
                                                                   ,@Name_En
                                                                   ,@SurName_Th
                                                                   ,@SurName_En
                                                                   ,@Sex
                                                                   ,@Position_Id
                                                                   ,@PositionDesc_Th
                                                                   ,@PositionDesc_En
                                                                   ,@Dim_Branch
                                                                   ,@Branch_ID
                                                                   ,@Branch
                                                                   ,@Branch_Th
                                                                   ,@Branch_Desc
                                                                   ,@Area
                                                                   ,@GrmGroup
                                                                   ,@Department_Id
                                                                   ,@Department_TH
                                                                   ,@Department_EN
                                                                   ,@Faction_Id
                                                                   ,@Faction_Th
                                                                   ,@Faction_EN
                                                                   ,@Empst
                                                                   ,@User_Email
                                                                   ,@User_Uid
                                                                   ,@PP_Emplid
                                                                   ,@Afs_Branch_Code
                                                                   ,@Startdate
                                                                   ,@Enddate
                                                                   ,@Approver
                                                                   ,@Approver_Name
                                                                   ,@B_Manager_Id
                                                                   ,@B_Name
                                                                   ,@Employee_Type
                                                                   ,@Phone
                                                                   ,@Extension
                                                                   ,@Pref_First_Name
                                                                   ,@IsCanBehaft
                                                                   ,@IsAcceptTerms)");

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(userInfo));
            }
        }

        public override async Task<int> Update(UserInfo userInfo)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"UPDATE [UserInfo]
                                                           SET [EmpId] = @EmpId
                                                              ,[FK_RoleId] = @FK_RoleId
                                                              ,[IsStatus] = @IsStatus
                                                              ,[TitleId] = @TitleId
                                                              ,[TitleName_Th] = @TitleName_Th
                                                              ,[TitleName_En] = @TitleName_En
                                                              ,[Name_Th] = @Name_Th
                                                              ,[Name_En] = @Name_En
                                                              ,[SurName_Th] = @SurName_Th
                                                              ,[SurName_En] = @SurName_En
                                                              ,[Sex] = @Sex
                                                              ,[Position_Id] = @Position_Id
                                                              ,[PositionDesc_Th] = @PositionDesc_Th
                                                              ,[PositionDesc_En] = @PositionDesc_En
                                                              ,[Dim_Branch] = @Dim_Branch
                                                              ,[Branch_ID] = @Branch_ID
                                                              ,[Branch] = @Branch
                                                              ,[Branch_Th] = @Branch_Th
                                                              ,[Branch_Desc] = @Branch_Desc
                                                              ,[Area] = @Area
                                                              ,[GrmGroup] = @GrmGroup
                                                              ,[Department_Id] = @Department_Id
                                                              ,[Department_TH] = @Department_TH
                                                              ,[Department_EN] = @Department_EN
                                                              ,[Faction_Id] = @Faction_Id
                                                              ,[Faction_Th] = @Faction_Th
                                                              ,[Faction_EN] = @Faction_EN
                                                              ,[Empst] = @Empst
                                                              ,[User_Email] = @User_Email
                                                              ,[User_Uid] = @User_Uid
                                                              ,[PP_Emplid] = @PP_Emplid
                                                              ,[Afs_Branch_Code] = @Afs_Branch_Code
                                                              ,[Startdate] = @Startdate
                                                              ,[Enddate] = @Enddate
                                                              ,[Approver] = @Approver
                                                              ,[Approver_Name] = @Approver_Name
                                                              ,[B_Manager_Id] = @B_Manager_Id
                                                              ,[B_Name] = @B_Name
                                                              ,[Employee_Type] = @Employee_Type
                                                              ,[Phone] = @Phone
                                                              ,[Extension] = @Extension
                                                              ,[Pref_First_Name] = @Pref_First_Name
                                                              ,[IsCanBehaft] = @IsCanBehaft
                                                              ,[IsAcceptTerms] = @IsAcceptTerms
                                                 WHERE [Id] = @Id");

                return await db.ExecuteAsync(sqlCommand, ParameterMapping(userInfo));
            }
        }

        public override async Task<int> Delete(int id)
        {
            using (var db = new SqlConnection(connectionStrings))
            {
                var sqlCommand = string.Format(@"DELETE FROM [UserInfo] WHERE [id] = @Id");
                return await db.ExecuteAsync(sqlCommand, new { Id = id });
            }
        }



        private Object ParameterMapping(UserInfo userInfo)
        {
            return new
            {
                Id = userInfo.Id,
                EmpId = userInfo.EmpId,
                FK_RoleId = userInfo.FK_RoleId,
                IsStatus = userInfo.IsStatus,
                TitleId = userInfo.TitleId,
                TitleName_Th = userInfo.TitleName_Th,
                TitleName_En = userInfo.TitleName_En,
                Name_Th = userInfo.Name_Th,
                Name_En = userInfo.Name_En,
                SurName_Th = userInfo.SurName_Th,
                SurName_En = userInfo.SurName_En,
                Sex = userInfo.Sex,
                Position_Id = userInfo.Position_Id,
                PositionDesc_Th = userInfo.PositionDesc_Th,
                PositionDesc_En = userInfo.PositionDesc_En,
                Dim_Branch = userInfo.Dim_Branch,
                Branch_ID = userInfo.Branch_ID,
                Branch = userInfo.Branch,
                Branch_Th = userInfo.Branch_Th,
                Branch_Desc = userInfo.Branch_Desc,
                Area = userInfo.Area,
                GrmGroup = userInfo.GrmGroup,
                Department_Id = userInfo.Department_Id,
                Department_TH = userInfo.Department_TH,
                Department_EN = userInfo.Department_EN,
                Faction_Id = userInfo.Faction_Id,
                Faction_Th = userInfo.Faction_Th,
                Faction_EN = userInfo.Faction_EN,
                Empst = userInfo.Empst,
                User_Email = userInfo.User_Email,
                User_Uid = userInfo.User_Uid,
                PP_Emplid = userInfo.PP_Emplid,
                Afs_Branch_Code = userInfo.Afs_Branch_Code,
                Startdate = userInfo.Startdate,
                Enddate = userInfo.Enddate,
                Approver = userInfo.Approver,
                Approver_Name = userInfo.Approver_Name,
                B_Manager_Id = userInfo.B_Manager_Id,
                B_Name = userInfo.B_Name,
                Employee_Type = userInfo.Employee_Type,
                Phone = userInfo.Phone,
                Extension = userInfo.Extension,
                Pref_First_Name = userInfo.Pref_First_Name,
                IsCanBehaft = userInfo.IsCanBehaft,
                IsAcceptTerms = userInfo.IsAcceptTerms
            };
        }
    }
}
