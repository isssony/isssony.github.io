namespace CityApp
{
    public class District
    {
        public string Name { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }

        public double Density
        {
            get { return Population / Area; }
        }
    }
}