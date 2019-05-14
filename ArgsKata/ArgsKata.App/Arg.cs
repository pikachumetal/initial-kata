namespace ArgsKata.App
{
    public class Arg
    {
        public string Key { get; private set; }
        public object Value { get; private set; }

        public Arg(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
