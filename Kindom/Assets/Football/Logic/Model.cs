using UnityEngine;
using Common.Utility;
using System.Collections.Generic;

namespace Football
{
	/// <summary>
	/// 模型
	/// </summary>
	public class Model : Component
	{
		/// <summary>
		/// 动作时间轴事件委托
		/// </summary>
		public delegate void AnimationTimeLineDelegate (string name, float time, bool EndAnimation);

		/// <summary>
		/// 时间轴事件回调参数
		/// </summary>
		public class TimeLineCallBack : Object
		{
			/// <summary>
			/// 动作名称
			/// </summary>
			public string Name;
			/// <summary>
			///  时间
			/// </summary>
			public float Time;
			/// <summary>
			/// 处理方法
			/// </summary>
			public AnimationTimeLineDelegate Handler;
			/// <summary>
			/// 是否是结束动作标识
			/// </summary>
			public bool EndAnimation;

		}
		
		public Model ()
		{
		}

		/// <summary>
		/// 注册时间轴事件
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="time">Time.</param>
		/// <param name="handler">Handler.</param>
		public void RegisterAnimationEventCallback(string name, AnimationTimeLineDelegate handler) {
			Animator animator = this.GetComponent<Animator> ();
			if (animator == null) {
				return;
			}

			RuntimeAnimatorController controller = animator.runtimeAnimatorController;
			for (int i = 0; i < controller.animationClips.Length; i++) {
				AnimationClip clip = controller.animationClips [i];
				if (clip.name != name) {
					continue;
				}

				TimeLineCallBack callback = new TimeLineCallBack ();
				callback.Handler = handler;
				callback.Name = name;
				callback.Time = clip.length;
				callback.EndAnimation = true;

				AnimationEvent animationEvent = new AnimationEvent ();
				animationEvent.time = clip.length;
				animationEvent.functionName = "OnTimeLineEvent";
				animationEvent.objectReferenceParameter = callback as Object;
				clip.AddEvent (animationEvent);
			}
		}

		/// <summary>
		/// 注册时间轴事件
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="time">Time.</param>
		/// <param name="handler">Handler.</param>
		public void RegisterAnimationEventCallback(string name, float time, AnimationTimeLineDelegate handler) {
			Animator animator = this.GetComponent<Animator> ();
			if (animator == null) {
				return;
			}

			RuntimeAnimatorController controller = animator.runtimeAnimatorController;
			for (int i = 0; i < controller.animationClips.Length; i++) {
				AnimationClip clip = controller.animationClips [i];
				if (clip.name != name) {
					continue;
				}

				TimeLineCallBack callback = new TimeLineCallBack ();
				callback.Handler = handler;
				callback.Name = name;
				callback.Time = time;
				callback.EndAnimation = false;

				AnimationEvent animationEvent = new AnimationEvent ();
				animationEvent.time = time;
				animationEvent.functionName = "OnTimeLineEvent";
				animationEvent.objectReferenceParameter = callback as Object;
				clip.AddEvent (animationEvent);
			}
		}

		/// <summary>
		/// 注销时间轴事件
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="time">Time.</param>
		public void UnregisterAnimationEventCallback(string name, float time) {
			Animator animator = this.GetComponent<Animator> ();
			if (animator == null) {
				return;
			}

			RuntimeAnimatorController controller = animator.runtimeAnimatorController;
			for (int i = 0; i < controller.animationClips.Length; i++) {
				AnimationClip clip = controller.animationClips [i];
				if (clip.name != name) {
					continue;
				}

				List<AnimationEvent> eventList = new List<AnimationEvent> ();
				for (int j = 0; j < clip.events.Length; j++) {
					if (clip.events [j].time != time) {
						eventList.Add (clip.events [i]);
					}
				}
				clip.events = eventList.ToArray();
			}
		}

		/// <summary>
		/// 时间轴回调事件
		/// </summary>
		/// <param name="obj">Object.</param>
		void OnTimeLineEvent(Object obj) {
			if (obj == null) {
				return;
			}
			TimeLineCallBack callback = obj as TimeLineCallBack;
			if (callback != null && callback.Handler != null) {
				callback.Handler (callback.Name, callback.Time, callback.EndAnimation);
			}
		}

		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="animationName">Animation name.</param>
		public void Play(string animationName) {
			Animation animation = this.GetComponent<Animation> ();
			if (animation == null) {
				return;
			}
			animation.Play (animationName);
		}
	}
}

