using System;

/// <summary>
/// 波 y = A * sin(w * x + q) + k
/// 周期 T
/// 频率 f
/// 角速度 w
/// 振幅 A
/// 初始x方向偏移位置 q
/// y方向偏移位置 k
/// 
/// T = 1 / f
/// w = 2 * PI / T = 2 * PI * f
/// </summary>
public class Wave
{
	/// <summary>
	/// 波状态
	/// </summary>
	public enum WaveStatus
	{
		/// <summary>
		/// 上升
		/// </summary>
		Rise,
		/// <summary>
		/// 下降
		/// </summary>
		Fall,
	}
	/// <summary>
	/// 振幅
	/// </summary>
	private float _Amplitude;
	/// <summary>
	/// 频率
	/// </summary>
	private float _Frequency;
	/// <summary>
	/// 波状态
	/// </summary>
	private WaveStatus _Status;
	/// <summary>
	/// 振幅
	/// </summary>
	/// <value>The amplitude.</value>
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
	/// <value>The frequency.</value>
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

	/// <summary>
	/// 波状态，上升还是下降
	/// </summary>
	/// <value>The status.</value>
	public WaveStatus Status {
		get { 
			return _Status;
		}
		set { 
			_Status = value;
		}
	}

	private Wave ()
	{
				
	}

	/// <summary>
	/// 创建一个波
	/// </summary>
	/// <param name="amplitude">Amplitude.</param>
	/// <param name="frequency">Frequency.</param>
	/// <param name="status">Status.</param>
	public static Wave Create(float amplitude, float frequency, WaveStatus status)
	{
		Wave wave = new Wave ();
		wave.Amplitude = amplitude;
		wave.Frequency = frequency;
		wave.Status = status;
		return wave;
	}
}

