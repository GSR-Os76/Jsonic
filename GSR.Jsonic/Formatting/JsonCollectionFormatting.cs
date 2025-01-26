namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonArray"/> or <see cref="JsonObject"/> in commong.
    /// </summary>
    public abstract class JsonCollectionFormatting
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
        /// <summary>
        /// Whitespace to add after every single element separating comma that's not followed by a newline.
        /// </summary>
        public string PostCommaSpacing { get; } = " ";



        /// <inheritdoc/>
        public JsonCollectionFormatting(
            int emptyCollectionNewlineCount,
            bool newLineProceedingFirstElement,
            bool newLineBetweenElements,
            bool newLineSucceedingLastElement,
            string indentation,
            string postCommaSpacing)
        {
#if DEBUG
            if (emptyCollectionNewlineCount < 0)
                throw new ArgumentOutOfRangeException(nameof(emptyCollectionNewlineCount));

            if (!Util.ANCHORED_WHITESPACE_REGEX.IsMatch(indentation))
                throw new ArgumentException();

            if (!Util.ANCHORED_WHITESPACE_REGEX.IsMatch(postCommaSpacing))
                throw new ArgumentException();
#endif
            EmptyCollectionNewLining = emptyCollectionNewlineCount;
            NewLineProceedingFirstElement = newLineProceedingFirstElement;
            NewLineBetweenElements = newLineBetweenElements;
            NewLineSucceedingLastElement = newLineSucceedingLastElement;
            Indentation = indentation;
            PostCommaSpacing = postCommaSpacing;
        } // end ctor

    } // end struct
} // end namespace