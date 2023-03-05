namespace Ixora_REST_API.Models
{
    public class Client : Entity
    {
        public int Id { get; set; }
        //public FullName ClientName { get; set; } //ФИО
        public string ClientName { get; set; } //ФИО
        public string PhoneNumber { get; set; } //Phone number
        public Client()
        {

        }
        public Client(int id, string clientName, string phoneNumber)
        {
            Id = id;
            ClientName = clientName;
            PhoneNumber = phoneNumber;
        }
    }
}
