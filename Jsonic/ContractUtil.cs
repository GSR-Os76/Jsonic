namespace GSR.Jsonic
{
    internal static class ContractUtil
    {
        public static T IsNotNull<T>(this T t) => t is not null ? t : throw new ArgumentNullException("Violation of contract, expected not null.");
        public static T IsNotNull<T>(this object? t) => t is not null ? (T)t : throw new ArgumentNullException("Violation of contract, expected not null.");

    } // end class
} // end namespace