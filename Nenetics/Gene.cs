namespace Nenetics
{
    /// <summary>
    /// Contains a single value, true or false.  Can be treated as a 1 or 0
    /// </summary>
    public class Gene 
    {
        public bool Value { get; set; }

        public Gene(bool value)
        {
            Value = value;
        }

        public static Gene GetRandomGene()
        {
            return RandomNumberSource.GetNext(1) == 1 ? new Gene(true) : new Gene(false);
        }

        public override string ToString()
        {
            return Value ? "1" : "0";
        }
    }
}
