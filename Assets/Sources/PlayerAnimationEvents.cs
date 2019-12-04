using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationEvents : MonoBehaviour
{

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // [TODO] To move to a specific PlayerAnimationEvents script
    void FootR()
    {
        // Do something
    }
    // [TODO] To move to a specific PlayerAnimationEvents script
    void FootL()
    {
        // Do something
    }
    // [TODO] To move to a specific PlayerAnimationEvents script
    void Land()
    {
        // Do something
    }
    // [TODO] To move to a specific PlayerAnimationEvents script
    void HasLanded()
    {
        playerController.CanJump = true;
    }
}
