using UnityEngine;
using TMPro;

public class DynamicText : MonoBehaviour
{
    [SerializeField] int textId;
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        switch(textId)
        {
            case 0:
                text.text = $"With {PlayerMovement.damage} damage(s).";
                break;
        }
    }
}
