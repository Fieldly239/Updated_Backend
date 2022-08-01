namespace KM_Management_Api.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsStatus { get; set; }
    }
}
