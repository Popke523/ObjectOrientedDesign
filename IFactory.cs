namespace ObjectOrientedDesign
{
    public interface IFactory
    {
        public FlightSystemObject CreateFromString(string s);
    }
}
