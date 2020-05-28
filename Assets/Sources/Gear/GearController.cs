using UnityEngine;

public class GearController : MonoBehaviour
{
    public CharacterFSM owner { private set; get; }

    public void Attach(CharacterFSM character)
    {
        owner = character;
    }
}