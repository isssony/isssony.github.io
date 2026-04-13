using System;

namespace ZoologicalLotto
{
    [Serializable]
    public class Animal
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; } // птицы, млекопитающие, рептилии, насекомые
        public string Habitat { get; set; } // среда обитания
        public string Description { get; set; } // описание для подсказки

        public Animal()
        {
            Name = string.Empty;
            ImagePath = string.Empty;
            Category = string.Empty;
            Habitat = string.Empty;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}