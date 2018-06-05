using UnityEngine;
using UnityEditor;

public class DeletePlayerPrefs : ScriptableObject
{
    [MenuItem("Custom/Clear all Player Preferences")]
    static void deleteAllExample()
    {
        if (EditorUtility.DisplayDialog("Delete all editor preferences.",
                "Are you sure you want to delete all the editor preferences? " +
                "This action cannot be undone.", "Yes", "No"))
        {
            Debug.Log("yes");
            PlayerPrefs.DeleteAll();
        }
    }
}
