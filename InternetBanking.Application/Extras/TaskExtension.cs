using System.Runtime.CompilerServices;

namespace InternetBanking.Application.Extras
{
    public static class TaskExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> AsTask<T>(this T t)
        => Task.FromResult(t);
    }
}