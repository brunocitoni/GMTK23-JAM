using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class UtilityFunctions
{
    public static string printList<T>(List<T> list)
    {
        string toprint = " ";

        foreach (T item in list)
        {
            toprint += item + ",";
        }

        return toprint;
    }

    // to find an inactive game object by name
    public static GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    public static string RemoveFileExtension(ReadOnlySpan<char> path)
    {
        var lastPeriod = path.LastIndexOf('.');
        return (lastPeriod < 0 ? path : path[..lastPeriod]).ToString();
    }

    /*public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
        return assets;
    }*/

    public static int GenerateRandomIntSeed(string seed)
    {
        return seed.GetHashCode() * Application.identifier.GetHashCode();
    }

    public static void MyDestroy(GameObject go)
    {
        go.name = "Morituri"; // needs to update the name otherwise confusion due to the fact that they don't get destroyed immediately and I am looking for them by name
        UnityEngine.Object.Destroy(go);
    }

    public static GameObject FindParentWithName(GameObject childObject, string name)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.name == name)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    public static void DestroyAllChildren(GameObject go)
    {
            // Iterate through all child objects
            foreach (Transform child in go.transform)
            {
                // Destroy the child GameObject
                MyDestroy(child.gameObject);
            }
    }
}
