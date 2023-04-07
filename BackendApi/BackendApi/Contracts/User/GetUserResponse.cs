namespace BackendApi.Contracts.User
{
    public class GetUserResponse
    {
        public int UserNumber { get; set; }
        public string Nickname { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Namee { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime Birthdate { get; set; }
    }
}
