using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
/// Automatically saves active scene and all the assets on entering play mode.
/// </summary>
[InitializeOnLoad]
public class AutoSave
{
    static AutoSave ()
    {
        EditorApplication.playmodeStateChanged = () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                var activeScene = SceneManager.GetActiveScene();
                EditorSceneManager.SaveScene(activeScene);
                AssetDatabase.SaveAssets();
            }
        };
    }
}
