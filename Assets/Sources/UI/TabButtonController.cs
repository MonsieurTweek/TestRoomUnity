using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButtonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroupController tabGroup = null;

    public Image background = null;
    public TextMeshProUGUI title = null;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    private Color _defaultTitleColor = Color.white;

    private void Awake()
    {
        _defaultTitleColor = title.color;
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        title.color = _defaultTitleColor;

        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
