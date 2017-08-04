using System.Collections.Generic;

namespace Football.AI
{
	/// <summary>
	/// 规则节点
	/// </summary>
	internal class RuleNode
	{
		/// <summary>
		/// 条件
		/// </summary>
		private int _Condition;
		/// <summary>
		/// 结果
		/// </summary>
		private int _Result;
		/// <summary>
		/// 子节点
		/// </summary>
		private Dictionary<int, RuleNode> _Children;

		/// <summary>
		/// 条件
		/// </summary>
		/// <value>The condition.</value>
		public int Condition {
			get { 
				return _Condition;
			}
			set { 
				_Result = value;
			}
		}
		/// <summary>
		/// 结果
		/// </summary>
		/// <value>The result.</value>
		public int Result {
			get { 
				return _Result;
			}
			set { 
				_Result = value;
			}
		}

		public RuleNode() {
			_Condition = FSMRule.INVALID_CONDITION;
			_Result = FSMRule.INVALID_RESULT;
			_Children = new Dictionary<int, RuleNode>();				
		}

		/// <summary>
		/// 添加子节点
		/// </summary>
		/// <param name="node">Node.</param>
		public void AddChild (RuleNode node) {
			if (node == null) {
				return;
			}
			_Children[node.Condition] = node;
		}

		/// <summary>
		/// 移除子节点
		/// </summary>
		/// <param name="node">Node.</param>
		public void RemoveChild (RuleNode node) {
			if (node == null) {
				return;
			}
			if (_Children.ContainsKey (node.Condition)) {
				_Children.Remove (node.Condition);
			}
		}

		/// <summary>
		/// 移除所有子节点
		/// </summary>
		public void RemoveAllChildren() {
			_Children.Clear ();
		}

		/// <summary>
		/// 匹配子节点
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="condition">Condition.</param>
		public RuleNode FindChild(int condition) {
			if (_Children.ContainsKey (condition)) {
				return _Children [condition];
			} else {
				return null;
			}
		}
	}

	/// <summary>
	/// 条件树
	/// </summary>
	public class FSMRule
	{
		/// <summary>
		/// 跟节点
		/// </summary>
		private RuleNode _Root;

		/// <summary>
		/// 不可用条件
		/// </summary>
		public const int INVALID_CONDITION = -1;
		/// <summary>
		/// 不可用结果
		/// </summary>
		public const int INVALID_RESULT = -1;

		public FSMRule() {
			_Root = new RuleNode();
		}

		/// <summary>
		/// 添加规则
		/// </summary>
		/// <param name="conditions">Conditions.</param>
		/// <param name="result">Result.</param>
		public void AddRule(int[] conditions, int result) {
			if (conditions == null || conditions.Length == 0) {
				return;
			}
			RuleNode lastNode = _Root;
			for (int i = 0; i < conditions.Length; i++) {
				RuleNode tempNode = lastNode.FindChild (conditions [i]);
				if (tempNode == null) {
					tempNode = new RuleNode ();
					tempNode.Condition = conditions [i];
					lastNode.AddChild (tempNode);
				}
				lastNode = tempNode;
			}

			lastNode.Result = result;
		}

		/// <summary>
		/// 移除规则
		/// 
		/// 先找到规则链，存放在栈
		/// 再从最后一个开始移除，一直到第一个
		/// </summary>
		/// <param name="conditions">Conditions.</param>
		public void RemoveRule(int[] conditions) {
			if (conditions == null || conditions.Length == 0) {
				return;
			}

			Stack<RuleNode> ruleStack = new Stack<RuleNode> ();

			RuleNode lastNode = _Root;
			ruleStack.Push (lastNode);
			int len = conditions.Length - 1;
			for (int i = 0; i < len; i++) {
				lastNode = lastNode.FindChild (conditions [i]);
				if (lastNode == null) {
					return;
				}
				ruleStack.Push (lastNode);
			}

			while (ruleStack.Count > 0) {
				RuleNode node = ruleStack.Pop ();
				RuleNode child = node.FindChild (conditions [len]);
				node.RemoveChild (child);
				len--;
			}
		}

		/// <summary>
		/// 匹配规则
		/// -1 表示未设置
		/// </summary>
		/// <param name="conditions">Conditions.</param>
		public int Match(int[] conditions) {
			if (conditions == null || conditions.Length == 0) {
				return INVALID_RESULT;
			}

			RuleNode lastNode = _Root;
			for (int i = 0; i < conditions.Length; i++) {
				lastNode = lastNode.FindChild (conditions [i]);
				if (lastNode == null) {
					return INVALID_RESULT;
				}
			}

			return lastNode.Result;
		}
	}
}

