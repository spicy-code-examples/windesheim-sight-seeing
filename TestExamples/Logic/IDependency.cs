namespace Logic
{
    public interface IDependency
    {
        public Task ExecuteFollowUpActionAsync(string name);
    }
}