namespace GSR.Jsonic
{
    internal static class ContractUtil
    {
#if ASSERT
        internal static T IsNotNull<T>(this T t) => t is not null ? t : throw new ArgumentNullException("Violation of contract, expected not null.");
#endif
    } // end class
} // end namespace