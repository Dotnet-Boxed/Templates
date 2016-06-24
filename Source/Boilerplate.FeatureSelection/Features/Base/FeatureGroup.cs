namespace Boilerplate.FeatureSelection.Features
{
    public class FeatureGroup : IFeatureGroup
    {
        private readonly string name;
        private readonly int order;

        public FeatureGroup(string name, int order)
        {
            this.name = name;
            this.order = order;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Order
        {
            get { return this.order; }
        }
    }
}
