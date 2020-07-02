using TMPro;
using UnityEngine;

public class EnemyUIController : MonoBehaviour
{
    [Header("References")]
    public Transform introLayout = null;
    public TextMeshProUGUI introTitle = null;
    public TextMeshProUGUI gaugeTitle = null;

    [Header("Properties")]
    public LeanTweenType introAnimation = LeanTweenType.linear;

    private Vector3 _introOriginalPosition = Vector3.zero;

    private void Awake()
    {
        _introOriginalPosition = introLayout.transform.position;
    }

    private void Start()
    {
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        gaugeTitle.text = data.name;
        introTitle.text = data.name;

        // Position is reset to be out of screen
        introLayout.transform.position = new Vector3(-Screen.width, _introOriginalPosition.y, 0f);

        LeanTween.moveX(introLayout.gameObject, Screen.width * 0.5f, 1f).setEase(introAnimation).setDelay(0.1f);

        introLayout.gameObject.SetActive(true);
    }

    private void OnIntroEnded()
    {
        introLayout.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }
    }
}
