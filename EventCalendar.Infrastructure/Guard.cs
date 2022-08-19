using System;

namespace EventCalendar.Infrastructure
{
    public static class Guard
    {
        public static void ThrowIfNull(object argumentValue, string message, string parameterName)
        {
            if (argumentValue == null) throw new ArgumentNullException(message, parameterName);
        }

        public static void ThrowIfNullOrEmpty(string argumentValue, string message, string parameterName)
        {
            if (string.IsNullOrEmpty(argumentValue)) throw new ArgumentNullException(message, parameterName);
        }

        public static void ThrowIfDateOutOfRange(DateTime argumentValue, string message, string parameterName)
        {
            if (argumentValue < DateTimeOffset.UtcNow) throw new ArgumentOutOfRangeException(message, parameterName);
        }

        public static void ThrowIfZeroOrNegative(int argumentValue, string message, string parameterName)
        {
            if (argumentValue <= 0) throw new ArgumentOutOfRangeException(message, parameterName);
        }
    }
}
