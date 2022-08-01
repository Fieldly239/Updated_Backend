namespace KM_Management_Api.Models
{
    public class Application
    {
     public int Id { get; set; }
     public string Name { get; set; }
     public string Description { get; set; }
     public bool IsStatus { get; set; }
     public DateTime? CreatedDate { get; set; }
     public DateTime? ModifiedDate { get; set; }
     public string CreatedBy { get; set; }
     public string ModifiedBy { get; set; }
    }
}
