namespace RegioAds.Domain.Models.Tree
{
    public class PrefixTree<TValue>
    {
        private readonly TreeNode<string, TValue> _root;
        private readonly IEqualityComparer<TValue> _comparer;


        public PrefixTree(IEqualityComparer<TValue> comparer)
        {
            _root = new TreeNode<string, TValue>(string.Empty, comparer);
            _comparer = comparer;
        }


        public void AddNode(string path, TValue item)
        {
            var current = _root;
            var pathParts = path.Split('/', StringSplitOptions.RemoveEmptyEntries).Distinct();

            foreach (var pathPart in pathParts)
                current = current.GetOrAddChild(pathPart);

            current.Items.Add(item);
        }

        public List<TValue> FindNodes(string path)
        {
            var current = _root;
            var pathParts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var result = new List<TValue>();

            foreach (var pathPart in pathParts)
            {
                if (!current.Children.TryGetValue(pathPart, out var child))
                    break;

                current = child;
                result.AddRange(current.Items);
            }

            return result.Distinct(_comparer).ToList();
        }
    }
}
