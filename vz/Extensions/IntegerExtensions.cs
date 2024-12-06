namespace vz.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Adds two integers together.
        /// </summary>
        /// <param name="number"> The number to add to. </param>
        /// <param name="toAdd"> The number to be added. </param>
        /// <returns> The sum of the two integers. </returns>
        /// <exception cref="OverflowException"> Thrown if the result overflows an int. </exception>
        public static int Add(this int number, int toAdd)
        {
            if (toAdd == 0) return number; // Early return if we're adding zero

            try
            {
                return checked(number + toAdd); // Use 'checked' to throw OverflowException if overflow occurs
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("The sum of the numbers caused an overflow.", ex);
            }
        }

        /// <summary>
        /// Divides one integer by another.
        /// </summary>
        /// <param name="number"> The number to be divided. </param>
        /// <param name="divisor"> The number to divide by. </param>
        /// <returns> The result of the division. </returns>
        /// <exception cref="DivideByZeroException"> Thrown when attempting to divide by zero. </exception>
        /// <exception cref="OverflowException"> Thrown if the result of the division overflows an int. </exception>
        public static int Divide(this int number, int divisor)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            try
            {
                return checked(number / divisor); // Use 'checked' for integer division overflow
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("The division resulted in an overflow.", ex);
            }
        }

        /// <summary>
        /// Multiplies two integers.
        /// </summary>
        /// <param name="number"> The number to multiply. </param>
        /// <param name="multiplier"> The number by which to multiply. </param>
        /// <returns> The product of the two integers. </returns>
        /// <exception cref="OverflowException"> Thrown if the product overflows an int. </exception>
        public static int Multiply(this int number, int multiplier)
        {
            if (multiplier == 1) return number; // Early return if multiplying by 1
            if (multiplier == 0 || number == 0) return 0; // Early return if result is obviously zero

            try
            {
                return checked(number * multiplier); // Use 'checked' to throw OverflowException if overflow occurs
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("The multiplication caused an overflow.", ex);
            }
        }

        /// <summary>
        /// Subtracts one integer from another.
        /// </summary>
        /// <param name="number"> The number to subtract from. </param>
        /// <param name="toSubtract"> The number to subtract. </param>
        /// <returns> The result of the subtraction. </returns>
        /// <exception cref="OverflowException"> Thrown if the result underflows an int. </exception>
        public static int Subtract(this int number, int toSubtract)
        {
            if (toSubtract == 0) return number; // Early return if we're subtracting zero

            try
            {
                return checked(number - toSubtract); // Use 'checked' to throw OverflowException if underflow occurs
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("The subtraction resulted in an underflow.", ex);
            }
        }
    }
}