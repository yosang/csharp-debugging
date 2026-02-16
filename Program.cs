namespace Debugging
{
    public class Program
    {
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
            try
            {
                divideTwoNums(2, 0);
            }
            catch (DivideByZeroException err)
            {
                Console.WriteLine("Error: " + err.Message); // Error: Attempted to divide by zero.

                // throw err; // Re-throws the error
            }
            finally
            {
                Console.WriteLine("Exception handling done");
            }
        }
    }
}