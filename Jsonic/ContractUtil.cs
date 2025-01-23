namespace GSR.Jsonic
{
    internal static class ContractUtil
    {
#if DEBUG
        public static T IsNotNull<T>(this T t) => t is not null ? t : throw new ArgumentNullException("Violation of contract, expected not null.");
        public static T IsNotNull<T>(this object? t) => t is not null ? (T)t : throw new ArgumentNullException("Violation of contract, expected not null.");
#endif
    } // end class
} // end namespace