using UnityEngine;
using System.Collections;

/// <summary>
/// 天气系统
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class Weather : MonoBehaviour
{
	/// <summary>
	/// 粒子系统
	/// </summary>
	private ParticleSystem _ParticleSystem;
	/// <summary>
	/// 形状模块
	/// </summary>
	private ParticleSystem.ShapeModule _ShapModule;

	/// <summary>
	/// 方形长度
	/// </summary>
	public float SquareLength;
	/// <summary>
	/// 方形宽度
	/// </summary>
	public float SquareWidth;
	/// <summary>
	/// 下落高度
	/// </summary>
	public float SkyHeight;

	public ParticleSystem ParticleSystem {
		get { 
			return _ParticleSystem;
		}
	}

	/// <summary>
	/// 初始化天气
	/// </summary>
	public virtual void InitWeather() {
		this.transform.rotation = Quaternion.Euler (new Vector3 (90, 0, 0));
			
		_ParticleSystem = this.GetComponent<ParticleSystem>	();
		_ShapModule = _ParticleSystem.shape;

		// 形状
		_ShapModule.shapeType = ParticleSystemShapeType.Box;

		// 重力
		ParticleSystem.MainModule main = _ParticleSystem.main;
		main.gravityModifierMultiplier = 1;


		this.SetSkySize (SquareLength, SquareWidth);
		this.SetSkyHeight (SkyHeight);
	}

	/// <summary>
	/// 设置天空大小
	/// </summary>
	/// <param name="length">Length.</param>
	/// <param name="width">Width.</param>
	public void SetSkySize(float length, float width) {
		ParticleSystem.ShapeModule shape = _ParticleSystem.shape;
		Vector3 box = shape.box;
		box.x = length;
		box.y = width;
		shape.box = box;
	}

	/// <summary>
	/// 设置天空高度
	/// </summary>
	/// <param name="height">Height.</param>
	public void SetSkyHeight(float height) {
		Vector3 pos = this.transform.position;
		pos.y = height;
		this.transform.position = pos;
	}

	/// <summary>
	/// 材质
	/// </summary>
	/// <value>The shap material.</value>
	public Material ShapMaterial {
		get { 
			return _ShapModule.meshRenderer.material;
		}
		set { 
			_ShapModule.meshRenderer.material = value;
		}
	}

	/// <summary>
	/// 替换纹理
	/// </summary>
	/// <param name="texture">Texture.</param>
	public void ReplaceTexture(Texture texture)
	{
		Material mat = ShapMaterial;
		if (mat == null) {
			return;
		}

		mat.mainTexture = texture;
	}
}

