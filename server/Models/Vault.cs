namespace server.Models
{
    public class Vault
    {

        public int id { get; set; }
        public string creatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isPrivate { get; set; }
        public Profile creator { get; set; }
    }

    public class VaultViewModel : Vault
    {
        public int VaultKeepId { get; set; }
    }
}