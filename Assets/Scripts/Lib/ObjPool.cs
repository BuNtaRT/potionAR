using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum TypeObj : byte
{
    Mushrooms
}

public class ObjPool : MonoBehaviour
{
    private GameObject _overSpawnCell;
    private GameObject _mainContainer;
    public static ObjPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Instance obj over 1");

        GlobalEventsManager.OnPause.AddListener(PauseSub);
    }

    private void PauseSub(PauseStatus status, bool enable) 
    {
        if (enable) 
        {
            if (enable)
            {
                _mainContainer.transform.position = new Vector3(0, -10, 0);
                _mainContainer.transform.localScale = Vector3.zero;
            }
            else
            {
                _mainContainer.transform.position = Vector3.zero;
                _mainContainer.transform.localScale = Vector3.one;

            }
        }
    }

    [Serializable]
    public struct ObjectsInfo 
    {
        public TypeObj    Type;
        public GameObject Prefab;
        public int        Count;
    }

    [SerializeField]
    private List<ObjectsInfo> _objectsInfo = new List<ObjectsInfo>();
    private Dictionary<TypeObj, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        _mainContainer = new GameObject();
        _mainContainer.name = "SpawnerContainer";

        _overSpawnCell = new GameObject();
        _overSpawnCell.name = "OverSpawnCell";
        _overSpawnCell.transform.SetParent(_mainContainer.transform);
        poolDictionary = new Dictionary<TypeObj, Queue<GameObject>>();

        foreach (ObjectsInfo temp in _objectsInfo)
        {
            GameObject cellPool = new GameObject();
            cellPool.transform.SetParent(_mainContainer.transform);
            cellPool.name = temp.Prefab.name;

            Queue<GameObject> tempQueue = new Queue<GameObject>();
            for (int i = 0; i < temp.Count; i++)
            {
                GameObject obj = Instantiate(temp.Prefab);
                obj.transform.SetParent(cellPool.transform);
                obj.SetActive(false);
                tempQueue.Enqueue(obj);
            }
            poolDictionary.Add(temp.Type, tempQueue);
        }
    }

    public Transform SpawnObj(TypeObj type, Vector3 position) 
    {
        if (poolDictionary[type].Count != 0)
        {
            GameObject temp = poolDictionary[type].Dequeue();
            temp.SetActive(true);
            temp.transform.position = position;
            return temp.transform;
        }
        else
        {
            var temp = _objectsInfo.FirstOrDefault(o => (o.Type == type)).Prefab;
            GameObject tempObj = Instantiate(temp);
            tempObj.transform.SetParent(_overSpawnCell.transform);
            tempObj.transform.position = position;
            return tempObj.transform;
        }
    }

    public void Destroy(TypeObj type, GameObject obj) 
    {
        obj.SetActive(false);
        poolDictionary[type].Enqueue(obj);
    }
}
