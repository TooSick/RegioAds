namespace RegioAds.Domain.Models.Tree
{
    public class TreeNode<TKey, TValue>
    {
        private readonly IEqualityComparer<TValue> _comparer;


        public TKey Key { get; }

        public Dictionary<TKey, TreeNode<TKey, TValue>> Children { get; }

        public HashSet<TValue> Items { get; }


        public TreeNode(TKey key, IEqualityComparer<TValue> comparer)
        {
            Key = key;
            Children = new();
            Items = new(comparer);
            _comparer = comparer;
        }


        public TreeNode<TKey, TValue> GetOrAddChild(TKey key)
        {
            if (!Children.TryGetValue(key, out TreeNode<TKey, TValue> child))
            {
                child = new TreeNode<TKey, TValue>(key, _comparer);
                Children[key] = child;
            }

            return child;
        }
    }
}
