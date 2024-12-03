namespace InternetBanking.Application.Dto.Transaction
{
    public class TransactionStatisticsDTO
    {
        public required int NumberOfTransactionsInCurrentDay { get; set; }
        public required int TotalNumberOfTransactions { get; set; }
        public required int NumberOfPaymentsInCurrentDay { get; set; }
        public required int TotalNumberOfPayments { get; set; }
    }
}