using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 噪声波
/// </summary>
public class NoiseWave
{
	/// <summary>
	/// 振幅
	/// </summary>
	private float _Amplitude;
	/// <summary>
	/// 频率
	/// </summary>
	private float _Frequency;
	/// <summary>
	/// 组合波
	/// </summary>
	private List<Wave> _Waves;

	/// <summary>
	/// 振幅
	/// </summary>
	public float Amplitude {
		get { 
			return _Amplitude;
		}
		set { 
			_Amplitude = value;
		}
	}

	/// <summary>
	/// 频率
	/// </summary>
	public float Frequency {
		get { 
			return _Frequency;
		}
		set { 
			_Frequency = value;
		}
	}

	/// <summary>
	/// 波长
	/// </summary>
	/// <value>The length of the wave.</value>
	public float WaveLength {
		get { 
			if (_Frequency <= 0) {
				return 0;
			}
			return 1 / _Frequency;
		}
	}

	public NoiseWave()
	{
		_Waves = new List<Wave> ();
	}

	/// <summary>
	/// 添加波
	/// </summary>
	/// <param name="wave">Wave.</param>
	public void AddWave(Wave wave)
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
	public void RemoveWave(Wave wave)
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
	public void RemoveAllWaves() 
	{
		_Waves.Clear ();
	}

	/// <summary>
	/// 创建噪声波
	/// </summary>
	/// <param name="amplitude">Amplitude.</param>
	/// <param name="frequency">Frequency.</param>
	public static NoiseWave Create(float amplitude, float frequency)
	{
		int waveCount = (int)frequency;
		int heightCount = waveCount + 1;

		// y轴偏移
		float[] positionYs = new float[heightCount];
		for (int i = 0; i < heightCount; i++) {
			positionYs[i] = Random.Range (-amplitude, amplitude);
		}

		NoiseWave noiseWave = new NoiseWave ();
		noiseWave.Amplitude = amplitude;
		noiseWave.Frequency = frequency;

		for (int i = 0; i < waveCount; i++) {
			float a = Mathf.Abs (positionYs [i] - positionYs [i + 1]) * 0.5f;
			//float k = Mathf.Min (positionYs [i], positionYs [i + 1]) + a;
			Wave.WaveStatus status = positionYs [i + 1] > positionYs [i] ? Wave.WaveStatus.Rise : Wave.WaveStatus.Fall;
			Wave wave = Wave.Create (a, 1 / frequency, status);

			noiseWave.AddWave (wave);
		}

		return noiseWave;
	}
}

