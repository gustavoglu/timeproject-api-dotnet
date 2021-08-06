namespace TimeProject.Domain.ObjectValues
{
    public class Tag
    {
        public Tag(string name, string color = null)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; set; }
        public string Color { get; set; }
    }
}
