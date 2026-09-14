using Unity.VisualScripting;
using UnityEngine;

public class TimeLerper : MonoBehaviour
{
    //Task A
    public float progress;
    public float duration;
    public int direction;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        direction = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        TimeScaleLerp();
    }
    private void TimeScaleLerp()
    {
        progress += Time.deltaTime * direction;

        if (progress > duration)
        {
            direction = -1;
        }
        else if (progress < 0)
        {
            direction = 1;
        }

        transform.localScale = new Vector2(transform.localScale.x, Mathf.Lerp(transform.localScale.y, progress, duration));
    
        spriteRenderer.color = Color.Lerp(Color.green, Color.red, duration);
    }
}
