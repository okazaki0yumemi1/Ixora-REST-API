namespace Ixora_REST_API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public FullName ClientName { get; set; } //ФИО
        public string PhoneNumber { get; set; } //Phone number
        public Client()
        {

        }
        public Client(int id, FullName clientName, string phoneNumber)
        {
            Id = id;
            ClientName = clientName;
            PhoneNumber = phoneNumber;
        }
    }
}
