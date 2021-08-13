namespace TimeProject.Application.ViewModels
{
    public class DataSelect
    {
        public DataSelect(string key, string value, bool select = false)
        {
            Key = key;
            Value = value;
            Select = select;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public bool Select { get; set; }
    }
}
