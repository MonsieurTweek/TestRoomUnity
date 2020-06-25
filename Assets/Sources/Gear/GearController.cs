using UnityEngine;

public class GearController : MonoBehaviour
{
    public bool preserveScale = false;

    public CharacterFSM owner { private set; get; }

    public virtual void Attach(CharacterFSM character)
    {
        owner = character;
    }
}