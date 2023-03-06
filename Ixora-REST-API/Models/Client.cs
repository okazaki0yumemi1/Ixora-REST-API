namespace Ixora_REST_API.Models
{
    public class Client : Entity
    {
        public int Id { get; set; }
        public string ClientName { get; set; } //ФИО
        public string PhoneNumber { get; set; } //Phone number
        public Client()
        {

        }
    }
}
