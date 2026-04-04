using System.Collections.Generic;

namespace CityApp
{
    public class City
    {
        public string Name { get; set; }
        public List<District> Districts { get; set; } = new List<District>();
    }
}