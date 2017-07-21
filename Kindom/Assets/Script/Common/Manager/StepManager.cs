using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Utility;

/// <summary>
/// 步骤管理
/// </summary>
public class StepManager : SingletonBehaviour<StepManager>
{
	private List<IStep> _Steps;

	private List<IStep> _RemovedSteps;

	public StepManager()
	{
		_Steps = new List<IStep> ();
		_RemovedSteps = new List<IStep> ();
	}

	/// <summary>
	/// 添加步骤
	/// </summary>
	/// <param name="step">Step.</param>
	public void AddStep(IStep step)
	{
		if (step == null) {
			return;
		}

		if (_Steps.Contains (step)) {
			return;
		}

		_Steps.Add (step);
	}

	/// <summary>
	/// 移除步骤
	/// </summary>
	/// <param name="step">Step.</param>
	public void RemoveStep(IStep step) {
		if (step == null) {
			return;
		}

		if (_RemovedSteps.Contains (step)) {
			return;
		}

		_RemovedSteps.Add (step);
	}

	void Update()
	{
		for (int i = 0; i < _RemovedSteps.Count; i++) {
			_Steps.Remove (_RemovedSteps [i]);
		}
		_RemovedSteps.Clear ();

		int stepCount = _Steps.Count;
		for (int i = 0; i < stepCount; i++) {
			_Steps [i].DoEvent ();
			if (_Steps [i].Finish) {
				RemoveStep (_Steps[i]);
			}
		}
	}
}

