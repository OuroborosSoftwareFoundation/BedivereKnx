namespace BedivereKnx
{
    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; }

        public IEnumerable<TreeNode<T>> TraverseDepthFirst()
        {
            var stack = new Stack<TreeNode<T>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current;

                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(current.Children[i]);
                }
            }
        }

        public IEnumerable<TreeNode<T>> TraverseBreadthFirst()
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
    }

    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Parent { get; private set; }
        public List<TreeNode<T>> Children { get; } = new List<TreeNode<T>>();

        public TreeNode(T data)
        {
            Data = data;
        }

        public void AddChild(TreeNode<T> child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public bool RemoveChild(TreeNode<T> child)
        {
            child.Parent = null;
            return Children.Remove(child);
        }
    }

}
