namespace InternetBanking.Presentation.Models
{
    public class AdminIndexViewModel
    {
        public int TotalTransactions { get; set; }
        public int DailyTransactions { get; set; }
        public int TotalPayments { get; set; }
        public int DailyPayments { get; set; }
        public int ActiveClients { get; set; }
        public int InactiveClients { get; set; }
        public int TotalProducts { get; set; }
    }
}