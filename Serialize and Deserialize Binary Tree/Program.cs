using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace Serialize_and_Deserialize_Binary_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);
            root.right.left = new TreeNode(4);
            root.right.right = new TreeNode(5);
            InOrder(root);
            var data = serialize(root);
            TreeNode node = deserialize(data);
            InOrder(node);
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        static void InOrder(TreeNode root)
        {
            if (root == null) return ;
            Console.WriteLine(root.val);
            InOrder(root.left);
            InOrder(root.right);
        }

        // Encodes a tree to a single string.
        static string serialize(TreeNode root)
        {
            if (root == null) return "";
            Queue<TreeNode> treeNodes = new Queue<TreeNode>();
            StringBuilder builder = new StringBuilder();
            treeNodes.Enqueue(root);
            while(treeNodes.Count > 0)
            {
                TreeNode node = treeNodes.Dequeue();
                if(node == null)
                {
                    builder.Append("n ");
                    continue;
                }
                builder.Append(node.val + " ");
                treeNodes.Enqueue(node.left);
                treeNodes.Enqueue(node.right);
            }
            return builder.ToString();
        }

        // Decodes your encoded data to tree.
        static TreeNode deserialize(string data)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            var nodes = data.Split(" ");
            TreeNode root = new TreeNode(Convert.ToInt32(nodes[0]));
            queue.Enqueue(root);
            for(int i = 1; i < nodes.Length; i++)
            {
                if(queue.Count > 0)
                {
                    TreeNode parent = queue.Dequeue();
                    if (nodes[i] != "n")
                    {
                        TreeNode left = new TreeNode(Convert.ToInt32(nodes[i]));
                        parent.left = left;
                        queue.Enqueue(left);
                    }
                    if (nodes[++i] != "n")
                    {
                        TreeNode right = new TreeNode(Convert.ToInt32(nodes[i]));
                        parent.right = right;
                        queue.Enqueue(right);
                    }
                }
            }
            return root;
        }
    }
}
