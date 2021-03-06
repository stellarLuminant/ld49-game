using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(CanvasScaler))]
public class CanvasPixelScaling : MonoBehaviour
{
	public RectTransform BasePixelCanvas;

	CanvasScaler scaler;
	PixelPerfectCamera pixelCamera;

	[Header("Debug")]
	public bool isScaling;
	public TextMeshProUGUI textMeshProUGUI;

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		Pain();
	}

	void Pain()
    {
		Assert.IsNotNull(BasePixelCanvas, "BasePixelCanvas wasn't set!");

		pixelCamera = FindObjectOfType<PixelPerfectCamera>();
		Assert.IsNotNull(pixelCamera, "PixelPerfectCamera wasn't set!");

		scaler = GetComponent<CanvasScaler>();
		Assert.IsNotNull(scaler, "CanvasScaler wasn't set!");

		if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ConstantPixelSize)
		{
			Debug.LogWarning("scaler.uiScaleMode didn't equal ConstantPixelSize! Forcing to ConstantPixelSize.");
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
		}
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Pain();
	}


	// Update is called once per frame
	void Update()
	{
		var shouldScalePixelPerfectly = Application.isPlaying;
		
		#if UNITY_EDITOR
		// Editor specific code to be able to get pixelCamera.runInEditMode
		shouldScalePixelPerfectly = shouldScalePixelPerfectly 
			|| (pixelCamera && pixelCamera.runInEditMode);
		#endif

		if (shouldScalePixelPerfectly && pixelCamera)
		{
			Scale();
			isScaling = true;
		} else
        {
			isScaling = false;
        }

		if (textMeshProUGUI)
        {
			if (shouldScalePixelPerfectly)
				textMeshProUGUI.SetText($"Pixel Camera Debugging\n" +
					$"isScaling: {isScaling}\n" +
					$"pixelCamera exists: {pixelCamera != null}\n" +
					$"ratio: {pixelCamera.pixelRatio}\n");
			else
				textMeshProUGUI.SetText($"Pixel Camera Debugging\n" +
					$"bleh");
        }
	}

	public void Scale()
    {
		// Sets the correct scale factor
		scaler.scaleFactor = pixelCamera.pixelRatio;

		// Sets the BasePixelCanvas size to match the pixel perfect camera's size, multiplied by ratio
		// Note that in order for this to work, the rect needs to be in the center.
		var rect = BasePixelCanvas.rect;
		rect.width = pixelCamera.refResolutionX * pixelCamera.pixelRatio;
		rect.height = pixelCamera.refResolutionY * pixelCamera.pixelRatio;
	}
}
