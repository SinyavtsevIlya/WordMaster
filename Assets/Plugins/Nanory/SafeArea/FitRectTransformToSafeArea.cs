using UniRx;
using UnityEngine;

public class FitRectTransformToSafeArea : MonoBehaviour
{
    private Rect lastPixelRect;
    
    private void Awake()
    {
        this.ObserveEveryValueChanged(_ => Screen.orientation)
            .Subscribe(_ => AdjustCorners())
            .AddTo(this);
    }
 
    private void AdjustCorners()
    {
        var safeAreaTransform = GetComponent<RectTransform>();
 
        var safeArea = Screen.safeArea;

        var canvas = GetComponentInParent<Canvas>();
        
        Rect pixelRect = canvas.pixelRect;
        if (pixelRect == lastPixelRect)
            return;
 
        lastPixelRect = pixelRect;
        Debug.Log($"{this}: Applying safe area with canvas size {pixelRect.size}", this);
 
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= pixelRect.width;
        anchorMin.y /= pixelRect.height;
        anchorMax.x /= pixelRect.width;
        anchorMax.y /= pixelRect.height;
 
        safeAreaTransform.anchorMin = anchorMin;
        safeAreaTransform.anchorMax = anchorMax;
    }
}
