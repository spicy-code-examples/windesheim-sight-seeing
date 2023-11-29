namespace Logic
{
    public class Item
    {
        public string Name { get; set; } = "DefaultName";
        public int ActionCount { get; set; } = 0;

        public void PerformAction()
        {
            ActionCount++;
        }
    }
}
