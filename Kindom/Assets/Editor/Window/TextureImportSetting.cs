using UnityEngine;
using UnityEditor;

/// <summary>
/// 使用说明： 选择需要批量设置的贴图
/// 单击Custom/Window/Texture Import Settings
/// 打开窗口后选择对应参数
/// 点击Set Texture ImportSettings
/// 稍等片刻--批量设置成功。
/// </summary>
public class TextureImportSetting : EditorWindow {
    
    /// <summary>
    /// 临时存储int[]
    /// </summary>
    private int[] IntArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
    //AnisoLevel
    private int AnisoLevel = 2;
    //Filter Mode
    private int FilterModeInt = 1;
    private string[] FilterModeString = new string[] { "Point", "Bilinear", "Trilinear" };
    //Wrap Mode
    private int WrapModeInt = 0;
    private string[] WrapModeString = new string[] { "Repeat", "Clamp" };
    //Texture Type
    private int TextureTypeInt = 0;
	private string[] TextureTypeString = new string[] { "Default", "Normal Map", "GUI(Editor and Legacy)", "Sprite(2D and UI)", "Cursor", "Cookie", "Lightmap", "Single Channel" };
    //Max Size
    private int MaxSizeInt = 5;
    private string[] MaxSizeString = new string[] { "32", "64", "128", "256", "512", "1024", "2048", "4096" };
    //Format
    private int FormatInt = 0;
    private string[] FormatString = new string[] { "Compressed", "16 bits", "true color" };

	//Texture Shape
	private int TextureShapeInt = 0;
	private string[] TextureShapeString = new string[] { "2D", "Cube" };

	// sRGB(Color Texture)
	private bool sRGBBool = false;
	private bool AlphaIsTransparency = true;

	// alpha source
	private int AlphaSourceInt = 1;
	private string[] AlphaSourceString = new string[]{"None", "Input Texture Alpha", "From Gray Scale"};
    
    /// <summary>
    /// 创建、显示窗体
    /// </summary>
    [@MenuItem("Custom/Window/Texture Import Settings")]
    private static void Init()
    {    
        TextureImportSetting window = (TextureImportSetting)EditorWindow.GetWindow(typeof(TextureImportSetting), true, "TextureImportSetting");
        window.Show();
    }
    
    /// <summary>
    /// 显示窗体里面的内容
    /// </summary>
    private void OnGUI()
    {
		//Texture Type
		TextureTypeInt = EditorGUILayout.IntPopup("Texture Type", TextureTypeInt, TextureTypeString, IntArray);
		TextureShapeInt = EditorGUILayout.IntPopup("Texture Shape", TextureShapeInt, TextureShapeString, IntArray);
		EditorGUILayout.Space ();

		GUILayout.BeginHorizontal();
		GUILayout.Label("sRGB(Color Texture)  ");
		sRGBBool = EditorGUILayout.Toggle(sRGBBool);
		GUILayout.EndHorizontal();

		AlphaSourceInt = EditorGUILayout.IntPopup("Alpha Source", AlphaSourceInt, AlphaSourceString, IntArray);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Alpha Is Transparency  ");
		AlphaIsTransparency = EditorGUILayout.Toggle(AlphaIsTransparency);
		GUILayout.EndHorizontal();

		EditorGUILayout.Space ();

		//Wrap Mode
		WrapModeInt = EditorGUILayout.IntPopup("Wrap Mode", WrapModeInt, WrapModeString, IntArray);
        //Filter Mode
        FilterModeInt = EditorGUILayout.IntPopup("Filter Mode", FilterModeInt, FilterModeString, IntArray);

		//AnisoLevel
		GUILayout.BeginHorizontal();
		GUILayout.Label("Aniso Level  ");
		AnisoLevel = EditorGUILayout.IntSlider(AnisoLevel, 0, 9);
		GUILayout.EndHorizontal();
       
		EditorGUILayout.Space ();
        //Max Size
        MaxSizeInt = EditorGUILayout.IntPopup("Max Size", MaxSizeInt, MaxSizeString, IntArray);
        //Format
        FormatInt = EditorGUILayout.IntPopup("Format", FormatInt, FormatString, IntArray);

        if (GUILayout.Button("Set Texture ImportSettings"))
            LoopSetTexture();
    }
    
    /// <summary>
    /// 获取贴图设置
    /// </summary>
    public TextureImporter GetTextureSettings(string path)
    {
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        //AnisoLevel
        textureImporter.anisoLevel = AnisoLevel;
        //Filter Mode
        switch (FilterModeInt)
        {
            case 0:
                textureImporter.filterMode = FilterMode.Point;
                break;
            case 1:
                textureImporter.filterMode = FilterMode.Bilinear;
                break;
            case 2:
                textureImporter.filterMode = FilterMode.Trilinear;
                break;
        }
        //Wrap Mode
        switch (WrapModeInt)
        {
            case 0:
                textureImporter.wrapMode = TextureWrapMode.Repeat;
                break;
            case 1:
                textureImporter.wrapMode = TextureWrapMode.Clamp;
                break;
        }
        //Texture Type
		switch (TextureTypeInt) {
		case 0:
			textureImporter.textureType = TextureImporterType.Default;
			break;
		case 1:
			textureImporter.textureType = TextureImporterType.NormalMap;
			break;
		case 2:
			textureImporter.textureType = TextureImporterType.GUI;
			break;
		case 3:
			textureImporter.textureType = TextureImporterType.Cubemap;
			break;
		case 4:
			textureImporter.textureType = TextureImporterType.Sprite;
			break;
		case 5:
			textureImporter.textureType = TextureImporterType.Cursor;
			break;
		case 6:
			textureImporter.textureType = TextureImporterType.Cookie;
			break;
		case 7:
			textureImporter.textureType = TextureImporterType.Lightmap;
			break;
		case 8:
			textureImporter.textureType = TextureImporterType.SingleChannel;
			break;
		}
        //Max Size 
        switch (MaxSizeInt)
        {
            case 0:
                textureImporter.maxTextureSize = 32;
                break;
            case 1:
                textureImporter.maxTextureSize = 64;
                break;
            case 2:
                textureImporter.maxTextureSize = 128;
                break;
            case 3:
                textureImporter.maxTextureSize = 256;
                break;
            case 4:
                textureImporter.maxTextureSize = 512;
                break;
            case 5:
                textureImporter.maxTextureSize = 1024;
                break;
            case 6:
                textureImporter.maxTextureSize = 2048;
                break;
            case 7:
                textureImporter.maxTextureSize = 4096;
                break;
        }
        //Format
        switch (FormatInt)
        {
            case 0:
                textureImporter.textureFormat = TextureImporterFormat.AutomaticCompressed;
                break;
            case 1:
                textureImporter.textureFormat = TextureImporterFormat.Automatic16bit;
                break;
            case 2:
                textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                break;
        }

		//Shape
		switch (TextureShapeInt)
		{
		case 0:
			textureImporter.textureShape = TextureImporterShape.Texture2D;
			break;
		case 1:
			textureImporter.textureShape = TextureImporterShape.TextureCube;
			break;
		}

		// alpha
		switch (AlphaSourceInt)
		{
		case 0:
			textureImporter.alphaSource = TextureImporterAlphaSource.None;
			break;
		case 1:
			textureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
			break;
		case 2:
			textureImporter.alphaSource = TextureImporterAlphaSource.FromGrayScale;
			break;
		}

		textureImporter.sRGBTexture = sRGBBool;
		textureImporter.alphaIsTransparency = AlphaIsTransparency;


        return textureImporter;
    }
    
    /// <summary>
    /// 循环设置选择的贴图
    /// </summary>
    private void LoopSetTexture()
    {
        Object[] textures = GetSelectedTextures();
        Selection.objects = new Object[0];
        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter texImporter = GetTextureSettings(path);
            TextureImporterSettings tis = new TextureImporterSettings();
            texImporter.ReadTextureSettings(tis);
            texImporter.SetTextureSettings(tis);
            AssetDatabase.ImportAsset(path);
        }
    }
    
    /// <summary>
    /// 获取选择的贴图
    /// </summary>
    /// <returns></returns>
    private Object[] GetSelectedTextures()
    {
        return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    }
} 
