namespace InternetBanking.Application.Dto.Customer
{
    public class CustomerCountDTO
    {
        public int ActiveCustomersCount { get; set; }
        public int UnactiveCustomersCount { get; set; }
        public int TotalCustomersCount => UnactiveCustomersCount + ActiveCustomersCount;
    }
}