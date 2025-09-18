using RegioAds.Domain.Models;
using RegioAds.Domain.Models.Tree;

namespace RegioAds.Tests.Domain
{
    public class TreeNodeTests
    {
        private readonly TreeNode<string, string> _node;

        public TreeNodeTests()
        {
            _node = new TreeNode<string, string>("root", EqualityComparer<string>.Default);
        }

        [Fact]
        public void Constructor_SetsProperties()
        {
            Assert.Equal("root", _node.Key);
            Assert.NotNull(_node.Children);
            Assert.NotNull(_node.Items);
            Assert.Empty(_node.Children);
            Assert.Empty(_node.Items);
        }

        [Fact]
        public void GetOrAddChild_NewChild_CreatesAndReturnsChild()
        {
            var child = _node.GetOrAddChild("child");

            Assert.NotNull(child);
            Assert.Equal("child", child.Key);
            Assert.Single(_node.Children);
            Assert.Contains("child", _node.Children.Keys);
        }

        [Fact]
        public void GetOrAddChild_ExistingChild_ReturnsExistingChild()
        {
            var firstChild = _node.GetOrAddChild("child");

            var secondChild = _node.GetOrAddChild("child");

            Assert.Same(firstChild, secondChild);
            Assert.Single(_node.Children);
        }
    }

    public class PrefixTreeTests
    {
        private readonly PrefixTree<string> _tree;

        public PrefixTreeTests()
        {
            _tree = new PrefixTree<string>(EqualityComparer<string>.Default);
        }

        [Fact]
        public void AddNode_SingleLevelPath_AddsItem()
        {
            _tree.AddNode("location1", "platform1");

            var result = _tree.FindNodes("location1");
            Assert.Single(result);
            Assert.Contains("platform1", result);
        }

        [Fact]
        public void AddNode_DuplicateItems_StoresOnlyUniqueItems()
        {
            _tree.AddNode("location1", "platform1");
            _tree.AddNode("location1", "platform1");

            var result = _tree.FindNodes("location1");
            Assert.Single(result);
        }

        [Fact]
        public void FindNodes_PartialPath_ReturnsAllMatchingItems()
        {
            _tree.AddNode("country/city1", "platform1");
            _tree.AddNode("country/city2", "platform2");
            _tree.AddNode("country/city1/district1", "platform3");

            var result = _tree.FindNodes("country/city1/district1");

            Assert.Equal(2, result.Count);
            Assert.Contains("platform1", result);
            Assert.Contains("platform3", result);
        }

        [Fact]
        public void FindNodes_NonExistentPath_ReturnsEmptyList()
        {
            _tree.AddNode("location1", "platform1");

            var result = _tree.FindNodes("nonexistent");

            Assert.Empty(result);
        }

        [Fact]
        public void FindNodes_EmptyPath_ReturnsEmptyList()
        {
            var result = _tree.FindNodes("");

            Assert.Empty(result);
        }
    }

    public class AdTreeTests
    {
        private readonly AdTree _tree;

        public AdTreeTests()
        {
            _tree = new AdTree();
        }

        [Fact]
        public void Constructor_CreatesInstance()
        {
            Assert.NotNull(_tree);
        }

        [Fact]
        public void AddNode_WithAdPlatform_AddsSuccessfully()
        {
            var platform = new AdPlatform("TestPlatform");

            _tree.AddNode("location1", platform);

            var result = _tree.FindNodes("location1");
            Assert.Single(result);
            Assert.Equal("TestPlatform", result[0].Name);
        }

        [Fact]
        public void FindNodes_ReturnsDistinctPlatforms()
        {
            var platform1 = new AdPlatform("Platform1");
            var platform2 = new AdPlatform("Platform1");

            _tree.AddNode("location1", platform1);
            _tree.AddNode("location1", platform2);

            var result = _tree.FindNodes("location1");

            Assert.Single(result);
        }
    }
}
