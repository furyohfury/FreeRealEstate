using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime;

    private void Update()
    {
        // Накапливаем разницу во времени между кадрами
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // FPS = 1 / время кадра
        float fps = 1.0f / deltaTime;

        // Обновляем текст (Mathf.Ceil — округление вверх)
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}
