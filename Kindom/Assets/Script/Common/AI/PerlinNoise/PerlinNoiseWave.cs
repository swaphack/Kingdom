using UnityEngine;
using System.Collections.Generic;

public class PerlinNoiseWave
{
	/// <summary>
	/// 组合波
	/// </summary>
	private List<NoiseWave> _Waves;

	public PerlinNoiseWave ()
	{
		_Waves = new List<NoiseWave> ();
	}

	/// <summary>
	/// 添加波
	/// </summary>
	/// <param name="wave">Wave.</param>
	public void AddNoiseWave(NoiseWave wave)
	{
		if (wave == null) {
			return;
		}

		_Waves.Add (wave);
	}

	/// <summary>
	/// 移除波
	/// </summary>
	/// <param name="wave">Wave.</param>
	public void RemoveNoiseWave(NoiseWave wave)
	{
		if (wave == null) {
			return;
		}

		if (_Waves.Contains (wave)) {
			_Waves.Remove (wave);
		}
	}

	/// <summary>
	/// 移除所有波
	/// </summary>
	public void RemoveAllNoiseWaves() 
	{
		_Waves.Clear ();
	}

	/// <summary>
	/// 获取噪声
	/// </summary>
	/// <returns>The noise.</returns>
	/// <param name="position">Position.</param>
	public float GetNoise(float position)
	{
		int count = _Waves.Count;
		float total = 0;
		for (int i = 0; i < count; i++) {
			NoiseWave wave = _Waves [i];
			total += NoiseHelper.InterpolatedNoise1D (position * wave.Frequency) * wave.Amplitude;
		}
		return total;
	}

	/// <summary>
	/// 获取噪声
	/// </summary>
	/// <returns>The noise.</returns>
	/// <param name="position">Position.</param>
	public float GetNoise(Vector2 position)
	{
		int count = _Waves.Count;
		float total = 0;
		for (int i = 0; i < count; i++) {
			NoiseWave wave = _Waves [i];
			total += NoiseHelper.InterpolatedNoise2D (position.x * wave.Frequency, position.y * wave.Frequency) * wave.Amplitude;
		}
		return total; 
	}

	/// <summary>
	/// 获取噪声
	/// </summary>
	/// <returns>The noise.</returns>
	/// <param name="position">Position.</param>
	public float GetNoise(Vector3 position)
	{
		int count = _Waves.Count;
		float total = 0;
		for (int i = 0; i < count; i++) {
			NoiseWave wave = _Waves [i];
			total += NoiseHelper.InterpolatedNoise3D (position.x * wave.Frequency, position.y * wave.Frequency, position.z * wave.Frequency) * wave.Amplitude;
		}
		return total;
	}

	/// <summary>
	/// 使用叠加波纹创建柏林噪声
	/// </summary>
	/// <param name="waveParams">Wave parameters.</param>
	public PerlinNoiseWave Create(List<KeyValuePair<float, float>> waveParams)
	{
		if (waveParams == null || waveParams.Count == 0) {
			return null;
		}

		PerlinNoiseWave perlinWave = new PerlinNoiseWave ();

		int count = waveParams.Count;
		for (int i = 0; i < count; i++) {
			NoiseWave wave = NoiseWave.Create (waveParams [i].Key, waveParams [i].Value);
			if (wave != null) {
				perlinWave.AddNoiseWave (wave);
			}
		}

		return perlinWave;
	}

	/// <summary>
	/// 使用持久度创建柏林噪声
	/// </summary>
	/// <param name="persistence">Persistence.</param>
	/// <param name="octaves">Count.</param>
	public PerlinNoiseWave Create(float persistence, int octaves)
	{
		if (persistence <= 0 || octaves <= 0) {
			return null;
		}
		List<KeyValuePair<float, float>> waveParams = new List<KeyValuePair<float, float>> ();

		for (int i = 0; i < octaves; i++) {
			float amplitude = Mathf.Pow(persistence, i);
			float frequency = Mathf.Pow (2, i);
			waveParams.Add (new KeyValuePair<float, float> (amplitude, frequency));
		}

		return Create(waveParams);
	}
}

