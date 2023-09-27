using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


    [Serializable]
    public class TreeData {
        public T GetValue<T>() {
            if (this is ITreeData<T> tdata) {
                return tdata.value;
            }

            return default;
        }
    }

    public interface ITreeData<T> {
        public T value { get; }
    }

    public class GameActionNode : TreeData {
        public Pawn Instigator;
        public List<Pawn> Victims;
        public UniTask Animation;
        public Action<Pawn, List<Pawn>> Payload;
        public List<GameActionNode> Reactions = new List<GameActionNode>();

        public GameActionNode(Pawn instigator, List<Pawn> victims, UniTask animation, Action<Pawn, List<Pawn>> payload) {
            Instigator = instigator;
            Victims = victims;
            Animation = animation;
            Payload = payload;
        }
    }

    public class Foo {
        private GameActionNode _node;

        void Test() {}
    }

    /*
    public class UITreeData : TreeData {
        public TreeNode<UITreeData> Node;
        public NodeView NodeView;

        public UITreeData(NodeView gameObject, Transform parent, TreeNode<UITreeData> node) {
            Node = node;
            NodeView = GameObject.Instantiate(gameObject, parent);
            NodeView.TreeData = this;
        }
    }
    */
