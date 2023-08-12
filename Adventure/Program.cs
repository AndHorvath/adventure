namespace Adventure
{
    internal class Program
    {
        static void Main()
        {
            string? name;

            do
            {
                Console.WriteLine("Enter your name, please.");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));

            Player player = new(name);
            Adventure adventure = new(player);

            adventure.Start();
            adventure.Play();
            adventure.Stop();
        }
    }
}