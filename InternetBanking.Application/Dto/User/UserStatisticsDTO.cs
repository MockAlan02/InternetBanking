namespace InternetBanking.Application.Dto.User
{
    public class UserStatisticsDTO
    {
        public required int NumberOfActiveCustomer { get; set; }
        public required int NumberOfUnactiveCustomer { get; set; }
    }
}