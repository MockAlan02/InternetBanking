using InternetBanking.Application.Enums;

namespace InternetBanking.Application.Extras.ResultObject
{
    public record AppError(ErrorType Type, string Message);

    public static class AppErrorExt
    {
        public static AppError Because(this ErrorType type, string message)
        => new(type, message);
    }

    public static class AppErrors
    {
        public static AppError AccountDoesntExist { get; }
               = ErrorType.NotFound.Because("No existe una cuenta de ahorros con ese identificador");
    }
}