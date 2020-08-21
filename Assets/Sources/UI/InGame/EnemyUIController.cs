using TMPro;
using UnityEngine;

public class EnemyUIController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI gaugeTitle = null;
    
    private void Start()
    {
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        gaugeTitle.text = data.name;
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
        }
    }
}