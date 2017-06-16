using UnityEngine;
using System.Collections;

public class GroupItem : MonoBehaviour
{
	/// <summary>
	/// 所属分组
	/// </summary>
	private Group _Group;
	/// <summary>
	/// 当前项是否可用
	/// </summary>
	private bool _IsEnable;

	/// <summary>
	/// 所属分组
	/// </summary>
	/// <value>The group.</value>
	public Group Group {
		get { 
			return _Group;
		}
		set { 
			_Group = value;
		}
	}

	/// <summary>
	/// 当前项是否可用
	/// </summary>
	public bool IsEnable {
		get { 
			return _IsEnable;
		}
		protected set {
			_IsEnable = value;
		}
	}

	/// <summary>
	/// 当前项可用
	/// </summary>
	public virtual void Enable ()
	{
		IsEnable = true;
		if (Group != null) {
			Group.EnableItem (this);
		}
			
	}
	/// <summary>
	/// 当前项不可用
	/// </summary>
	public virtual void Disable()
	{
		IsEnable = false;	
		if (Group != null) {
			Group.DisableItem (this);
		}
	}
}

