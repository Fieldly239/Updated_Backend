namespace KM_Management_Api.Models
{
    public class KMTopListView
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsStatus { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Remark { get; set; }
        public int FK_StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
