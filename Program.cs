namespace Debugging
{
    public class Program
    {
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
            CountDown(5);
        }
    }
}