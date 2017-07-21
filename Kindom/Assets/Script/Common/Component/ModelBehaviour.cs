using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using Common.Utility;

/// <summary>
/// 模型组件
/// </summary>
[RequireComponent(typeof(HighlightableObject))]
public class ModelBehaviour : MonoBehaviour, ITouchModel,IInitialization
{
	/// <summary>
	/// 外发光颜色
	/// </summary>
	public Color OuterGlowColor = Color.red;
	/// <summary>
	/// 高光对象
	/// </summary>
	private HighlightableObject _HighlightableObject;
	/// <summary>
	/// 渲染
	/// </summary>
	private Renderer _Renderer;

	/// <summary>
	/// 是否可点击
	/// </summary>
	protected bool _EnableTouch;

	/// <summary>
	/// 是否点击过
	/// </summary>
	private bool _IsTouched;

	/// <summary>
	/// 是否点击过
	/// </summary>
	/// <value><c>true</c> if this instance is touched; otherwise, <c>false</c>.</value>
	public bool IsTouched {
		get { 
			return _IsTouched;
		} 
		set { 
			_IsTouched = value;
		}
	}

	/// <summary>
	/// 是否可触摸
	/// </summary>
	/// <value><c>true</c> if touch enable; otherwise, <c>false</c>.</value>
	public virtual bool EnableTouch {
		get { 
			return _EnableTouch;
		}
		set { 
			if (value == false) {
				TouchListener.Instance.RemoveDispatch (this.gameObject);
			} else {
				TouchListener.Instance.AddDispatch (this.gameObject, this.OnTouchMe);
			}
			_EnableTouch = value;

			Collider collider = GetComponent<Collider> ();
			if (collider != null) {
				collider.enabled = value;
			}
		}
	}

	/// <summary>
	/// 点击处理
	/// </summary>
	/// <param name="hitInfo">Hit info.</param>
	private void OnTouchMe(Vector3 hitInfo)
	{
		OnTouchModel (hitInfo);
	}

	/// <summary>
	/// 点击事件
	/// </summary>
	public virtual bool OnTouchModel(Vector3 hitInfo) { return false; }

	public Renderer Renderer {
		get { 
			return _Renderer;
		}
	}

	/// <summary>
	/// 材质
	/// </summary>
	/// <value>The material.</value>
	public Material Material {
		get { 
			if (_Renderer == null) {
				return null;
			}
			return _Renderer.material;
		}
		set {
			if (_Renderer == null) {
				return;
			}
			_Renderer.material = value;
		}
	}

	/// <summary>
	/// 颜色
	/// </summary>
	/// <value>The color.</value>
	public Color color {
		get { 
			if (Material == null) {
				return Color.white;
			}
			return Material.color;
		}
		set { 
			if (Material == null) {
				return;
			}
			Material.color = value;
		}
	}

	/// <summary>
	/// 透明度
	/// </summary>
	/// <value>The alpha.</value>
	public float Alpha {
		get { 
			if (Material == null) {
				return 0;
			}
			return Material.color.a;
		}
		set { 
			if (Material == null) {
				return;
			}
			Color color = Material.color;
			color.a = value;
			Material.color = color;
		}
	}

	/// <summary>
	/// 主纹理
	/// </summary>
	/// <value>The main texture.</value>
	public Texture MainTexture {
		get { 
			if (Material == null) {
				return null;
			}
			return Material.mainTexture;
		}
		set { 
			if (Material == null) {
				return;
			}
			Material.mainTexture = value;
		}
	}

	/// <summary>
	/// 主纹理的偏移位置
	/// </summary>
	/// <value>The main texture offset.</value>
	public Vector2 mainTextureOffset {
		get { 
			if (Material == null) {
				return Vector2.zero;
			}
			return Material.mainTextureOffset;
		}
		set { 
			if (Material == null) {
				return;
			}
			Material.mainTextureOffset = value;
		}
	}
	/// <summary>
	/// 主纹理的缩放比例
	/// </summary>
	/// <value>The main texture scale.</value>
	public Vector2 mainTextureScale {
		get { 
			if (Material == null) {
				return Vector2.zero;
			}
			return Material.mainTextureScale;
		}
		set { 
			if (Material == null) {
				return;
			}
			Material.mainTextureScale = value;
		}
	}

	/// <summary>
	/// 是否接收阴影
	/// </summary>
	/// <value><c>true</c> if receive shadows; otherwise, <c>false</c>.</value>
	public bool ReceiveShadows {
		get { 
			return _Renderer.receiveShadows;
		}
		set { 
			_Renderer.receiveShadows = value;
		}
	}

	/// <summary>
	/// 阴影产生方式
	/// </summary>
	/// <value>The shadow casting mode.</value>
	public ShadowCastingMode ShadowCastingMode {
		get {
			return _Renderer.shadowCastingMode;
		}
		set { 
			_Renderer.shadowCastingMode = value;
		}
	}

	/// <summary>
	/// 初始化
	/// </summary>
	public virtual void Initialize() {
		_HighlightableObject = GetComponent<HighlightableObject> ();
		_Renderer = GetComponent<Renderer> ();
	}

	/// <summary>
	/// 播放高光
	/// </summary>
	public void PlayHighlight() {
		if (_HighlightableObject != null) {
			_HighlightableObject.ConstantOn (OuterGlowColor);
		}
	}

	/// <summary>
	/// 取消高光
	/// </summary>
	public void CancelHighlight() {
		if (_HighlightableObject != null) {
			_HighlightableObject.ConstantOff ();
		}
	}

	/// <summary>
	/// 替换材质
	/// </summary>
	/// <param name="mat">Mat.</param>
	public void ReplaceMaterial(Material mat) {
		if (mat == null || _Renderer == null) {
			return;
		}
		_Renderer.material = mat;
	}

	/// <summary>
	/// 替换材质纹理
	/// </summary>
	/// <param name="texture">Texture.</param>
	public void ReplaceTexture(Texture texture)	{
		if (texture == null || _Renderer == null) {
			return;
		}
		if (_Renderer.material != null) {
			_Renderer.material.mainTexture = texture;
		}
	}

}

