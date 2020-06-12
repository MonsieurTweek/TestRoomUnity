using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HomeButtonController : MonoBehaviour
{
    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(GameEvent.instance.HomeButtonPressed);
    }
}
