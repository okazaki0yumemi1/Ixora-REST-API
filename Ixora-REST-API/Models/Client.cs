namespace Ixora_REST_API.Models
{
    public class Client
    {
        public Guid Id { get; set; } //ID клиента
        public FullName ClientName { get; set; } //ФИО
        public string PhoneNumber { get; set; } //Номер телефона

        public Client()
        {
            Id = new Guid();
        }
        public Client(Guid id, FullName clientName, string phoneNumber)
        {
            Id = id;
            ClientName = clientName;
            PhoneNumber = phoneNumber;
        }
    }
}
