using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshProUGUI))]
public class GetParentName : MonoBehaviour
{
    private TextMeshProUGUI _textMesh = null;

    private void OnEnable()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();

        _textMesh.text = transform.parent.name;
    }
}
