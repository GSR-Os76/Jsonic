namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonArray"/> or <see cref="JsonObject"/> in commong.
    /// </summary>
    public struct JsonCollectionFormatting
    {
        /// <summary>
        /// Number of newlines to place in an empty collection.
        /// </summary>
        public int EmptyCollectionNewLining { get; } = 0;

        /// <summary>
        /// Should there be a newline before the first element of the collection
        /// </summary>
        public bool NewLineProceedingFirstElement { get; } = true;

        /// <summary>
        /// Should there be a newline between each element of the collection
        /// </summary>
        public bool NewLineBetweenElements { get; } = true;
        
        /// <summary>
        /// Should there be a newline following the last element of the collection
        /// </summary>
        public bool NewLineSucceedingLastElement { get; } = true;

        /// <summary>
        /// Whitespace to add after every single newline for indentation, will be added once per collection body to indicate depth.
        /// </summary>
        public string Indentation { get; } = "    ";




        /// <inheritdoc/>
        public JsonCollectionFormatting(int emptyCollectionNewlineCount, bool newLineProceedingFirstElement, bool newLineBetweenElements, bool newLineSucceedingLastElement, string indentation) 
        {
#if DEBUG
            if (emptyCollectionNewlineCount > 0)
                throw new ArgumentOutOfRangeException(nameof(emptyCollectionNewlineCount));

            if (!JsonUtil.ANCHORED_WHITESPACE_REGEX.IsMatch(indentation))
                throw new ArgumentException();
#endif
            EmptyCollectionNewLining = emptyCollectionNewlineCount;
            NewLineProceedingFirstElement = newLineProceedingFirstElement;
            NewLineBetweenElements = newLineBetweenElements;
            NewLineSucceedingLastElement = newLineSucceedingLastElement;
        } // end ctor

    } // end struct
} // end namespace