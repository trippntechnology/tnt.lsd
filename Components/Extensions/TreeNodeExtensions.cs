using System.Windows.Forms;

namespace LSDComponents
{
	public static class TreeNodeExtensions
	{
		public static bool IsParent(this TreeNode parent, TreeNode node)
		{
			bool rtnValue = false;

			if (parent.Nodes.Count > 0)
			{
				// Check if node is immediate child of parent
				rtnValue = parent.Nodes.Contains(node);

				// If node is not immediate child check if node is further down
				for (int index = 0; index < parent.Nodes.Count && !rtnValue; index++)
				{
					rtnValue = IsParent(parent.Nodes[index], node);
				}
			}

			return rtnValue;
		}

		public static TreeNode GetFirstParent(this TreeNode node)
		{
			TreeNode firstParent = node;

			while (firstParent.Parent != null)
			{
				firstParent = firstParent.Parent;
			}

			return firstParent;
		}
	}
}
