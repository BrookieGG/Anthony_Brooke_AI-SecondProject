using Unity.VisualScripting;
using UnityEngine;

public class RoomTile : MonoBehaviour
{
    public float dirtiness = 100f;
    public float maxdirtiness = 100f;
    public Renderer tileRenderer;
    public Color cleanColor = Color.white;
    public Color dirtyColor = new Color(0.45f,0.25f,0.1f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        tileRenderer = GetComponent<Renderer>();

    }
    public void AddDirt(float amount)
    {
        dirtiness = Mathf.Clamp(dirtiness + amount, 0f, maxdirtiness);
        UpdateVisual();
    }
    public void CleanDirt(float amount)
    {
        dirtiness = Mathf.Clamp(dirtiness - amount, 0f, maxdirtiness);
        UpdateVisual();
    }
    public void SetDirtiness(float value)
    {
        dirtiness = Mathf.Clamp(value, 0f, maxdirtiness);
        UpdateVisual();
    }
    public void UpdateVisual()
    {
        if (tileRenderer != null)
        {
            float t = dirtiness / maxdirtiness;
            Color currentColor = Color.Lerp(cleanColor, dirtyColor, t);
            tileRenderer.material.color = currentColor;
        }
    }
}
