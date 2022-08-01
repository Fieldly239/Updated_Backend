namespace KM_Management_Api.Models
{
    public class FileAttachment
    { 
        public int Id { get; set; }
        public string FK_KMId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string ?CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ?ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<IFormFile> FormFiles { get; set; }
    }
    public class FilesExampleMultiServicetag
    {
        public IFormFile FormFiles { get; set; }
    }
    public class FileModel
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    }
}
