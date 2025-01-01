namespace AgricultureManager.Module.Api.Interfaces
{
    public interface IMenuItem
    {
        string Name { get; }
        string Icon { get; }
        string Url { get; }
        IEnumerable<IMenuItem> Children { get; }
    }
}
