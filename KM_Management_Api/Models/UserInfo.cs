namespace KM_Management_Api.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string FK_RoleId { get; set; }
        public bool IsStatus { get; set; }
        public string TitleId { get; set; }
        public string TitleName_Th { get; set; }
        public string TitleName_En { get; set; }
        public string Name_Th { get; set; }
        public string Name_En { get; set; }
        public string SurName_Th { get; set; }
        public string SurName_En { get; set; }
        public string Sex { get; set; }
        public string Position_Id { get; set; }
        public string PositionDesc_Th { get; set; }
        public string PositionDesc_En { get; set; }
        public string Dim_Branch { get; set; }
        public string Branch_ID { get; set; }
        public string Branch { get; set; }
        public string Branch_Th { get; set; }
        public string Branch_Desc { get; set; }
        public string Area { get; set; }
        public string GrmGroup { get; set; }
        public string Department_Id { get; set; }
        public string Department_TH { get; set; }
        public string Department_EN { get; set; }
        public string Faction_Id { get; set; }
        public string Faction_Th { get; set; }
        public string Faction_EN { get; set; }
        public string Empst { get; set; }
        public string User_Email { get; set; }
        public string User_Uid { get; set; }
        public string PP_Emplid { get; set; }
        public string Afs_Branch_Code { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public string Approver { get; set; }
        public string Approver_Name { get; set; }
        public string B_Manager_Id { get; set; }
        public string B_Name { get; set; }
        public string Employee_Type { get; set; }
        public string Phone { get; set; }
        public string Extension { get; set; }
        public string Pref_First_Name { get; set; }
        public bool IsCanBehaft { get; set; }
        public bool IsAcceptTerms { get; set; }
    }
}
