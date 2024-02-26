namespace ObjectOrientedDesign
{
    public interface IFactory
    {
        public IImport CreateFromString(string s);
    }
}
