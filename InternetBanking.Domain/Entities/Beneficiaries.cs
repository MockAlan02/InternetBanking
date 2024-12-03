using InternetBanking.Domain.Common;

namespace InternetBanking.Domain.Entities
{
    public class Beneficiaries : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DentifierAccount { get; set; } = string.Empty;
    }
}
