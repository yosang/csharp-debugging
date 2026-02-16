using ExceptionHandlers;

namespace Debugging
{
    public class Program
    {
        // Only accepts numbers equal or greater to 1
        public static int multiPlyByFive(int a)
        {
            if (a < 1)
            {
                throw new NumTooLow("Number must be 1 or greater");
            }

            return a * 5;
        }
        public static int divideTwoNums(int a, int b)
        {
            return a / b;
        }
        public static void BroadCast(string message)
        {
            Console.WriteLine(message);
        }
        public static void CountDown(int n)
        {
            string? message = null;

            int i = n;
            while (i > 0)
            {
                message = $"Countdown: {i}";
                BroadCast(message);
                i--;
            }

            if (message != null)
            {
                BroadCast("Countdown finished");
            }
        }
        public static void Main()
        {
            // Debugging
            CountDown(5);

            // Exception handling

            // Using a custom exception
            try
            {
                int result = multiPlyByFive(0); // Uncomment for an erronous operation
                // int result = multiPlyByFive(2); // Uncomment for a successful operation

                Console.WriteLine(result);
            }
            catch (NumTooLow err)
            {
                Console.WriteLine("Custom error: " + err.Message);

                // Keeps the strack trace in tact, this shows the problem happend in line 12 and originates from the call in line 52.
                // throw;

                // Here we loose a lot of information
                // The stack trace history is gone, as this treats the exception as a brand new exception
                // The stack trace only points to line 67 now and everything before it is unknown.
                // throw err;
            }
            catch (Exception err) // We can add more catch blocks, in case something else unexpected happens
            {
                Console.WriteLine("Unexpected error: " + err.Message);
            }


            // Using a builint Exception class
            try
            {
                // int result = divideTwoNums(2, 0); // Uncomment for an erronous operation
                int result = divideTwoNums(4, 2); // Uncomment for a successful operation

                Console.WriteLine(result); // Never runs if we try to divide by 0
            }
            catch (DivideByZeroException err)
            {
                Console.WriteLine("Error: " + err.Message); // Error: Attempted to divide by zero.

                throw; // Re-throws the error
            }
            finally
            {
                Console.WriteLine("Exception handling done");
            }
        }
    }
}