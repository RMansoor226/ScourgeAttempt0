using UnityEngine;

public class CrosshairScaler : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    [SerializeField] private float minSize = 20f;
    [SerializeField] private float maxSize = 80f;

    // Update is called once per frame
    void Update()
    {
        float scaleFactor = Screen.height / 1080f;

        float size = 32f * scaleFactor;

        size = Mathf.Clamp(size, minSize, maxSize);

        crosshair.sizeDelta = new Vector2(size, size);
    }
}
