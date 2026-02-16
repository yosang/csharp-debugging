namespace Debugging
{
    public class Program
    {
        public static void CountDown(int n)
        {
            string? message = null;

            int i = n;
            while (i > 0)
            {
                message = $"Countdown: {i}";
                Console.WriteLine(message);
                i--;
            }

            if (message != null)
            {
                Console.WriteLine("Countdown finished");
            }
        }
        public static void Main()
        {
            CountDown(5);
        }
    }
}