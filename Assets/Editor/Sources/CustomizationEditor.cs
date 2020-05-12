using UnityEngine;
using UnityEditor;

public class CustomizationEditor
{
    [MenuItem("Customization/Save")]
    public static void SaveCustomization()
    {
        Customization customization = ScriptableObject.CreateInstance<Customization>();

        if (CustomizationController.instance != null)
        {
            customization.isMale = CustomizationController.instance.playerCustomizationController.isMale;
            customization.Populate(CustomizationController.instance.playerCustomizationController.customizableParts);

            AssetDatabase.CreateAsset(customization, "Assets/Resources/Customization/_Customization.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = customization;

            SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Customization has been saved with success !"));
        }
        else
        {
            Debug.LogWarning("Can't find a customized player to save as preset");
        }
    }

    [MenuItem("Customization/Save", true)]
    public static bool CheckMenu()
    {
        return CustomizationController.instance != null;
    }
}