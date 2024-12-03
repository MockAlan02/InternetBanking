using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class Admin: BasicUser
    {
        public override UserType UserType { get; set; } = UserType.Admin;
    }
}
