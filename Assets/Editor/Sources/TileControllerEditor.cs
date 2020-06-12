using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(TileController))]
public class TileControllerEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        TileController targetTileController = (TileController)target;

        targetTileController.icon = (Image)EditorGUILayout.ObjectField("Icon", targetTileController.icon, typeof(Image), true);
        targetTileController.iconAnimationTime = EditorGUILayout.FloatField("Icon Animation Time", targetTileController.iconAnimationTime);
        targetTileController.iconAnimationIn = (LeanTweenType)EditorGUILayout.EnumPopup("Icon Animation In", targetTileController.iconAnimationIn);
        targetTileController.iconAnimationOut = (LeanTweenType)EditorGUILayout.EnumPopup("Icon Animation Out", targetTileController.iconAnimationOut);

        EditorGUILayout.Space();

        // Show default inspector property editor
        DrawDefaultInspector();
    }
}