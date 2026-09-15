using Unity.VisualScripting;
using UnityEngine;

public class TimeLerper : MonoBehaviour
{
    [Header ("Task A")]    
    public float progress;
    public float durationMax;
    public float startSize;
    public float endSize;

    [Header ("Task B")]    
    public SpriteRenderer spriteRenderer;
    public Color startColor, endColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Timer();

        TimeScaleLerp(startSize, endSize, progress / durationMax);
        TimeColorLerp(startColor, endColor, progress / durationMax);
    }
    private void Timer()
    {
        progress += Time.deltaTime;

        if(progress > durationMax)
        {
            progress = 0f;
        }
    }
    private void TimeScaleLerp(float start, float end, float duration)
    {
        transform.localScale = new Vector2(transform.localScale.x, Mathf.Lerp(start, end, duration));
    }
    private void TimeColorLerp(Color start, Color end, float duration)
    {
        spriteRenderer.material.color = Color.Lerp(start, end, duration);
    }
    private void PulseScaleLerp(float start, float end, float duration)
    {
        
    }
}
