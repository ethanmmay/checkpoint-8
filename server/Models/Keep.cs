namespace server.Models
{
    public class Keep
    {
        public int id { get; set; }
        public string creatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string img { get; set; } = "https://place-hold.it/400x400";
        public int keeps { get; set; }
        public int views { get; set; }
        public Profile creator { get; set; }
    }

    public class KeepViewModel : Keep
    {
        public int VaultKeepId { get; set; }
    }
}