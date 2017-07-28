using Football.AI;
using Football.Actions;
using UnityEngine;

namespace Football
{
	/// <summary>
	/// 球
	/// </summary>
	public class Ball : Entity
	{
		public Ball ()
		{
		}

		/// <summary>
		/// 添加球的动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void AddAction(Action action) {
			if (action == null) {
				return;
			}
			this.Performer.AddAction (action);
		}

		void Start() {
			AddAgent<Agent> ();

			this.AddAction(
				new RepeateForever(
					new Sequence(
						new Parabola(new Vector3(20,10,0), 0),
						new Parabola(new Vector3(-20,10,0), 0)
					)
				)
			);
		}
	}
}

