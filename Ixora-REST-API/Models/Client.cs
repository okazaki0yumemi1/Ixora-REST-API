namespace Ixora_REST_API.Models
{
    public class Client
    {
        public Guid Id { get; set; } //ID клиента
        public FullName ClientName { get; private set; } //ФИО
        public string PhoneNumber { get; private set; } //Номер телефона

        public Client()
        {
            Id = new Guid();
        }
    }
}
