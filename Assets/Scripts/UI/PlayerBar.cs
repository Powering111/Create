using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour, Bar
{
    [SerializeField] Image fg;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void set_rate(float value)
    {
        fg.fillAmount = value;
    }
}
