using UnityEngine;
using System.Collections.Generic;

namespace Groups
{
	/// <summary>
	/// 分组
	/// </summary>
	public class Group
	{
		/// <summary>
		/// 所有成员项
		/// </summary>
		private List<GroupItem> _GroupItems;
		/// <summary>
		/// 当前生效项
		/// </summary>
		private GroupItem _current;

		public GroupItem Current {
			get { 
				return _current;
			}
		}

		public Group ()
		{
			_GroupItems = new List<GroupItem> ();
		}

		/// <summary>
		/// 添加项
		/// </summary>
		/// <param name="item">Item.</param>
		public void AddItem (GroupItem item)
		{
			if (item == null || _GroupItems.Contains (item)) {
				return;
			}
			item.Group = this;
			_GroupItems.Add (item);
		}

		/// <summary>
		/// 移除项
		/// </summary>
		/// <param name="item">Item.</param>
		public void RemoveItem (GroupItem item)
		{
			if (item == null || !_GroupItems.Contains (item)) {
				return;
			}

			item.Group = null;
			_GroupItems.Remove (item);
		}

		/// <summary>
		/// 使当前项可用
		/// </summary>
		/// <param name="item">Item.</param>
		public void Enable (GroupItem item)
		{
			if (item == null || !_GroupItems.Contains (item)) {
				return;
			}

			int len = _GroupItems.Count;
			for (int i = 0; i < len; i++) {
				if (_GroupItems [i] != item) {
					_GroupItems [i].Disable ();
				}
			}

			_current = item;
		}

		/// <summary>
		/// 使项不可用
		/// </summary>
		/// <param name="item">Item.</param>
		public void DisableItem (GroupItem item)
		{
			if (item == null || !_GroupItems.Contains (item)) {
				return;
			}

			if (_current == item) {
				_current = null;
			}
		}
	}

}