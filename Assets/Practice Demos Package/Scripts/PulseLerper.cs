using UnityEngine;

public class PulseLerper : MonoBehaviour
{
    public float progress;
    public float durationMax;
    public float startSize;
    public float endSize;

    public AnimationCurve animationCurve;

    public SpriteRenderer spriteRenderer;
    public Color startColor, endColor;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Timer();

        PulseScaleLerp(startSize, endSize, animationCurve.Evaluate(progress / durationMax));
        PulseColorLerp(startColor, endColor, animationCurve.Evaluate(progress / durationMax));
    }
        private void Timer()
    {
        progress += Time.deltaTime;

        if(progress > durationMax)
        {
            progress = 0f;
        }
    }
    private void PulseScaleLerp(float start, float end, float duration)
    {
        transform.localScale = new Vector2(transform.localScale.x, Mathf.Lerp(start, end, duration));
    }
    private void PulseColorLerp(Color start, Color end, float duration)
    {
        spriteRenderer.material.color = Color.Lerp(start, end, duration);
    }
}
