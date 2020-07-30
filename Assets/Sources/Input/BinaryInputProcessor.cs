using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
public class BinaryInputProcessor : InputProcessor<float>
{

#if UNITY_EDITOR
    static BinaryInputProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<BinaryInputProcessor>();
    }

    public override float Process(float value, InputControl control)
    {
        return value > 0.125f ? 1f :
            value < -0.125f ? -1f : 0f;
    }
}