using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public void Init(){}

    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(Sprite))
        {
        }
        
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        if (prefab.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(prefab, parent).gameObject;
        
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        
        Object.Destroy(go);
    }
}
