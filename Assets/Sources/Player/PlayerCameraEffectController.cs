using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerCameraEffectController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public PostProcessVolume postProcess = null;
    public PlayerCameraShakeController shakeController = null;

    [Header("Properties")]
    public float duration = 0.5f;

    private Vignette _vignette = null;
    private float _vignetteIntensity = 0f;

    private void Awake()
    {
        postProcess.profile.TryGetSettings<Vignette>(out _vignette);        

        if (_vignette != null)
        {
            _vignetteIntensity = _vignette.intensity.value;
            _vignette.intensity.value = 0f;
        }
    }

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnHit;
    }

    private void OnHit(uint uniqueId, CharacterTypeEnum type, int health, int damage)
    {
        if (player.data.uniqueId == uniqueId && _vignette != null && player.stateMove.isDashing == false)
        {
            shakeController.Shake(duration);
            LeanTween.value(0f, _vignetteIntensity, duration).setEase(LeanTweenType.easeShake).setOnUpdate(UpdateVignetteIntensity);
        }
    }

    private void UpdateVignetteIntensity(float intensity)
    {
        _vignette.intensity.value = intensity;
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onHit -= OnHit;
        }
    }
}