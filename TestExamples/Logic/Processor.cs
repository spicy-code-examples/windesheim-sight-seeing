namespace Logic
{
    public class Processor(IDependency dependency)
    {
        public IDependency Dependency { get; set; } = dependency;
        public Item Item { get; set; } = new();

        public void DoAction()
        {
            Item.PerformAction();
            Dependency.ExecuteFollowUpActionAsync(Item.Name);
        }
    }
}
