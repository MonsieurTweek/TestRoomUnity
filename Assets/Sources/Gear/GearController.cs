using UnityEngine;

public class GearController : MonoBehaviour
{
    public bool preserveScale = false;
    public VisualEffectController fx = null;

    public CharacterFSM owner { private set; get; }

    protected virtual void Awake()
    {
        if (fx != null)
        {
            fx.gameObject.SetActive(false);
        }
    }

    public virtual void Attach(CharacterFSM character)
    {
        owner = character;
    }

    public void PlayFx()
    {
        if (fx != null)
        {
            GameObject gameObject = GamePoolManager.instance.UseFromPool(fx.name);

            gameObject.transform.position = fx.transform.position;
            gameObject.transform.rotation = fx.transform.rotation;

            gameObject.GetComponent<VisualEffectController>().Play();
        }
    }
}