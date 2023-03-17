using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyShape : MonoBehaviour
{
	List<GameObject> createdObjects;

    GameObject root = null;
    public GameObject Root
    {
        get
        {
            if (root == null) return gameObject;
            else return root;
        }
    }

	protected T CreateSymbol<T>(string name, Vector3 localPosition = new Vector3(), Quaternion localRotation = new Quaternion(), Transform parent = null) where T : MyShape
	{
		if (parent == null)
		{
			parent = transform; // default: add as child game object
		}
		GameObject newObj = new GameObject(name);
		newObj.transform.parent = parent;
		newObj.transform.localPosition = localPosition;
		newObj.transform.localRotation = localRotation;
		newObj.transform.localScale = new Vector3(1, 1, 1);
		AddGenerated(newObj);

		T component = newObj.AddComponent<T>();
		component.root = Root;
		return component;
	}

	protected GameObject AddGenerated(GameObject newObject)
	{
		if (createdObjects == null)
		{
			createdObjects = new List<GameObject>();
		}
		createdObjects.Add(newObject);
		return newObject;
	}

	protected GameObject SpawnPrefab(GameObject prefab, Vector3 localPosition = new Vector3(), Quaternion localRotation = new Quaternion(), Transform parent = null)
	{
		if (parent == null)
		{
			parent = transform; // default: add as child game object
		}
		GameObject copy = Instantiate(prefab, parent);
		copy.transform.localPosition = localPosition;
		copy.transform.localRotation = localRotation;
		copy.transform.localScale = new Vector3(1, 1, 1);
		AddGenerated(copy);
		return copy;
	}

    [ContextMenu("DeleteGenerated")]	
	public void DeleteGenerated()
	{
		if (createdObjects == null)
			return;
		foreach (GameObject gen in createdObjects)
		{
			if (gen == null)
				continue;
			// Delete recursively: (needed for when it's not a child of this game object)
			MyShape shapeComp = gen.GetComponent<MyShape>();
			if (shapeComp != null)
				shapeComp.DeleteGenerated();

			DestroyImmediate(gen);
		}
		createdObjects.Clear();
	}

	protected abstract void Execute();


}
