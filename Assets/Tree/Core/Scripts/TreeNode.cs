using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Core.Scripts {
    // From https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
    public class TreeNode<TValue> {
        private readonly List<TreeNode<TValue>> _children = new();

        public TreeNode(TValue value) {
            Value = value;
        }

        public TreeNode<TValue> this[int i] => _children[i];

        public TreeNode<TValue> Parent { get; private set; }

        public TValue Value { get; }

        public ReadOnlyCollection<TreeNode<TValue>> Children => _children.AsReadOnly();

        public TreeNode<TValue> AddChild(TValue value) {
            var node = new TreeNode<TValue>(value) {Parent = this};
            _children.Add(node);
            return node;
        }

        public TreeNode<TValue>[] AddChildren(params TValue[] values) {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<TValue> node) {
            return _children.Remove(node);
        }

        public void Traverse(Action<TValue> headAction) {
            headAction(Value);
            foreach (var child in _children)
                child.Traverse(headAction);
        }

        public void Traverse(Action<TValue> headAction, Action<TValue> childAction, bool depthFirst = true) {
            headAction(Value);
            foreach (var child in _children)
            {

                if (depthFirst) childAction?.Invoke(child.Value);
                child.Traverse(headAction);
                if (!depthFirst) childAction?.Invoke(child.Value);
            }
        }

        public IEnumerable<TValue> Flatten() {
            return new[] {Value}.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}