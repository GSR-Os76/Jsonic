namespace GSR.Jsonic
{
    // I hate the statefulness this pattern injects. But, it's more concise than writing two ToStrings for various things that're almost identical, and it's extensible.
    public enum JsonType
    {
        Array,
        Boolean,
        Null,
        Number,
        Object,
        String
    } // end enum
} // end namespace