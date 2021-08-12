using System.Collections.Generic;

namespace TimeProject.Infra.Identity.Rules
{
    public class Rule
    {
        public Rule(string name, List<string> types)
        {
            Name = name;
            Types = types;
        }

        public string Name { get; set; }
        public List<string> Types { get; set; }
    }
}
