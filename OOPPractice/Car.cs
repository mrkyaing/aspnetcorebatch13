namespace OOPPractice
{
    public class Car
    {
        public string Color { get; set; }
        public string Model { get; set; }
        public string LicneceNo { get; set; }

        public void Drive(int gearno)=> Console.WriteLine("Drive with Gear no " + gearno);
        public void PlayHorn() => Console.WriteLine("T");
        public void PlayHorn(int count)
        {
            for(int i=1;i<=count; i++)
            {
                Console.WriteLine("T");
            }
        }
    }
}
