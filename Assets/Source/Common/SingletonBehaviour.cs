using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T LocateSingleton (bool createIfNotFound = true)
    {
        T singletonObject;
        var singletonTypeName = typeof(T).Name;

        var objectsOfSingletonType = FindObjectsOfType<T>();
        if (objectsOfSingletonType.Length == 0 && !createIfNotFound)
        {
            Debug.LogError($"Wasn't able to locate {singletonTypeName} singleton object.");
            return null;
        }
        if (objectsOfSingletonType.Length > 1)
            Debug.LogWarning($"More than one instance of {singletonTypeName} singleton objects found.");

        if (objectsOfSingletonType.Length == 0 && createIfNotFound)
        {
            var singletonGameObject = new GameObject(singletonTypeName);
            singletonObject = singletonGameObject.AddComponent<T>();
        }
        else singletonObject = objectsOfSingletonType[0];

        return singletonObject;
    }
}
