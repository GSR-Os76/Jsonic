namespace GSR.Jsonic.Formatting
{
    /// <summary>
    /// Formatting defining how to format a <see cref="JsonNumber"/>.
    /// </summary>
    public sealed class JsonNumberFormatting
    {
        /// <summary>
        /// Write to string exactly as how it was parsed. Overrides all the other settings.
        /// </summary>
        public bool Preserve { get; } = true;

        /// <summary>
        /// Should the number be written as containing an exponent denoting the power of 10 to multiply the significand by.
        /// 
        /// For instance:
        /// 9e0 = 9 * 10^1 = 9,
        /// 1.2e2 = 1.2 * 10^2 = 120
        /// </summary>
        public bool PlaceExponent { get; }

        /// <summary>
        /// Should the exponent be denoted with a capital 'E'. If false lowercase 'e' will be used.
        /// 
        /// Unless <see cref="DecimalPositioning"/> indicates otherwise, significand will contain no decimal point.
        /// </summary>
        public bool CapitalizeExponent { get; }

        /// <summary>
        /// Should a '+' prepend the exponent when the exponent is of positive sign.
        /// </summary>
        public bool ExplicitlySignExponent { get; }

        /// <summary>
        /// Should insignificant trailing 0s be allowed after the decimal? 
        /// 
        /// See also: <seealso cref="DecimalPositioning"/>.
        /// </summary>
        public bool AllowInsignificantDigits { get; }

        /// <summary>
        /// At what position should the decimal point be placed.
        /// 
        /// Ignored unless <see cref="PlaceExponent"/> or <see cref="AllowInsignificantDigits"/> are true.
        /// 
        /// <para>
        /// When <see cref="PlaceExponent"/> is true:
        /// 0 indicates no decimal.
        /// A negative value indicates the preferred number of digits that will be to the right of the decimal, positioning will be fulfilled as far as possible, any further significant digits will be placed on the left.
        /// A positive value indicates the preferred number of digits that will be to the left of the decimal, positioning will be fulfilled as far as possible, any further significant digits will be placed on the right.
        /// </para>
        /// 
        /// <para>
        /// When <see cref="AllowInsignificantDigits"/> is true:
        /// 
        /// Trailing 0's will be added after the decimal point in volume sufficient to reach a number of digits equal to the preferred number, as such is only meanigful with a negative value and insufficient significant digits.
        /// </para>
        /// </summary>
        public int DecimalPositioning { get; }




        /// <inheritdoc/>
        public JsonNumberFormatting(
            bool preserve = true,
            bool placeExponent = false,
            bool capitalizeExponent = false,
            bool explicitlySignExponent = false,
            bool allowInsignificantDigits = false,
            int decimalPositioning = 0)
        {
            Preserve = preserve;
            PlaceExponent = placeExponent;
            CapitalizeExponent = capitalizeExponent;
            ExplicitlySignExponent = explicitlySignExponent;
            AllowInsignificantDigits = allowInsignificantDigits;
            DecimalPositioning = decimalPositioning;
        } // end ctor
    } // end class
} // end namespace