using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RoomPreview : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Color validColor = new(0f, 1f, 0f, 0.5f);
    [SerializeField] private Color invalidColor = new(1f, 0f, 0f, 0.5f);

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        sr.sortingOrder = 9999;
    }

    public void SetSprite(Sprite sprite)
    {
        if (!sr) sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }

    public void SetValid(bool isValid)
    {
        if (!sr) return;
        sr.color = isValid ? validColor : invalidColor;
    }
}
