namespace RegioAds.Domain.Models.Tree
{
    public class TreeNode<TKey, TValue> where TKey : notnull
    {
        public TKey Key { get; }

        public Dictionary<TKey, TreeNode<TKey, TValue>> Children { get; } = new();

        public List<TValue> Items { get; } = new();


        public TreeNode(TKey key)
        {
            Key = key;
        }


        public TreeNode<TKey, TValue> GetOrAddChild(TKey key)
        {
            if (!Children.TryGetValue(key, out TreeNode<TKey, TValue> child))
            {
                child = new TreeNode<TKey, TValue>(key);
                Children[key] = child;
            }

            return child;
        }
    }
}
