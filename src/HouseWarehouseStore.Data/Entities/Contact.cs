namespace HouseWarehouseStore.Data.Entities
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public long Mobile { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
    }
}