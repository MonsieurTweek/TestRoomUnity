using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
public class PlayAnimationInEditor : MonoBehaviour
{
    public string state = string.Empty;
    public string layer = string.Empty;
    [Range(0.0f, 1.0f)]
    public float normalizedTime = 0f;

    private Animator _animator = null;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play()
    {
        _animator.applyRootMotion = false;

        _animator.Play(state, _animator.GetLayerIndex(layer), normalizedTime);
        _animator.Update(Time.deltaTime);
    }

    public void UpdateTime(float offset)
    {
        normalizedTime += offset;

        Play();
    }

    public void ResetAnimator()
    {
        _animator.Play("Idle", _animator.GetLayerIndex("Base Layer"), 0f);

        _animator.Update(Time.deltaTime);
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(PlayAnimationInEditor))]
public class PlayAnimationInEditorInspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        // Show default inspector property editor
        DrawDefaultInspector();

        PlayAnimationInEditor playAnimationInEditor = (PlayAnimationInEditor)target;

        UnityEditor.EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Previous Frame"))
            playAnimationInEditor.UpdateTime(-0.01f);

        if (GUILayout.Button("Play"))
            playAnimationInEditor.Play();

        if (GUILayout.Button("Next Frame"))
            playAnimationInEditor.UpdateTime(0.01f);

        UnityEditor.EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Reset"))
            playAnimationInEditor.ResetAnimator();
    }
}
#endif