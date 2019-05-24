namespace OnlineQueuing.DTO
{
    public class AdminParserDTO
    {
        public Admin[] Admins { get; set; }
        
        public class Admin
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }

    }
}
