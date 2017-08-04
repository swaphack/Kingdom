using UnityEngine;
using Football.AI;
using Football.Actions;

namespace Football
{
	/// <summary>
	/// 实体
	/// </summary>
	public class Entity : MonoBehaviour
	{
		/// <summary>
		/// 编号
		/// </summary>
		private int _ID;
		/// <summary>
		/// 动作表演者
		/// </summary>
		private Performer _Performer;
		/// <summary>
		/// 智能体
		/// </summary>
		private Agent _Agent;

		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The ID.</value>
		public int ID {
			get { 
				return _ID;
			}
			set { 
				_ID = value;	
			}
		}

		/// <summary>
		/// 动作表演者
		/// </summary>
		/// <value>The performer.</value>
		public Performer Performer {
			get { 
				return _Performer;
			}
		}

		/// <summary>
		/// 智能体
		/// </summary>
		/// <value>The agent.</value>
		public Agent Agent {
			get { 
				return _Agent;
			} 
		}

		/// <summary>
		/// 父节点
		/// </summary>
		/// <value>The parent.</value>
		public Component Parent {
			get { 
				return transform.parent;
			}
		}

		public Entity ()
		{
			_Performer = new Performer ();
			_Performer.Entity = this;
		}

		void Update() {
			RunAction ();
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		private void RunAction() {
			_Performer.RunAction (Time.deltaTime);
		}

		/// <summary>
		/// 添加智能体
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void AddAgent<T>() where T : Agent 
		{
			T t = this.gameObject.AddComponent<T> ();
			t.SetEntity (this);
			_Agent = t;
		}

		/// <summary>
		/// 添加到父节点
		/// </summary>
		/// <param name="parent">Parent.</param>
		public void AddToParent(Component parent)
		{
			if (parent == null) {
				return;
			}

			transform.SetParent (parent.transform);
		}

		/// <summary>
		/// 从父节点移除
		/// </summary>
		public void RemoveFromParent() 
		{
			transform.SetParent (null);		
			GameObject.DestroyImmediate (gameObject);
		}

		/// <summary>
		/// 添加子节点
		/// </summary>
		/// <param name="t">T.</param>
		public void AddChild(Component t)
		{
			if (t == null) {
				return;
			}

			t.transform.SetParent (this.transform);
		}

		/// <summary>
		/// 移除子节点
		/// </summary>
		/// <param name="t">T.</param>
		public void RemoveChild(Component t) {
			if (t == null) {
				return;
			}

			Object.DestroyImmediate (t.gameObject);
		}

		/// <summary>
		/// 根据编号查找子节点
		/// </summary>
		/// <returns>The child by I.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T FindChildByID<T>(int id) where T : Component {
			string strID = id.ToString ();
			int count = this.transform.childCount;
			for (int i = 0; i < count; i++) {
				Transform transform = this.transform.GetChild (i);
				Entity childEntity = transform.GetComponent<Entity> ();
				if (childEntity != null && childEntity.ID == id) {
					return childEntity.GetComponent<T> ();
				}
			}

			return null;
		}

		/// <summary>
		/// 根据名称查找子节点
		/// </summary>
		/// <returns>The child by name.</returns>
		/// <param name="name">Name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T FindChildByName<T>(string name) where T : Component {
			Transform child = this.transform.Find (name);
			if (child == null) {
				return default(T);
			}
			return child.GetComponent<T> ();
		}
	}
}

