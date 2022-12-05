using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]
    private float     _tempFactor = 1;
    [SerializeField]
    private int       _updateSteep = 5;
    private int       _updateCount = 0;
    private Transform _transform;

    private Dictionary<CardObj,Transform> _modificationObj = new Dictionary<CardObj, Transform>();

    private void Awake() => _transform = transform;

    public void AddObj(CardObj obj) 
    {
        if (!_modificationObj.ContainsKey(obj)) 
            _modificationObj.Add(obj,obj.transform);
    }

    public bool RemoveObj(CardObj obj) 
    {
        if (_modificationObj.ContainsKey(obj))
            return _modificationObj.Remove(obj);
        return false;
    }

    private void FixedUpdate()
    {
        _updateCount++;
        if (_updateSteep <= _updateCount)
        {
            _updateCount = 0;
            if (_modificationObj.Count >= 1)
            {
                foreach (var item in _modificationObj)
                {
                    var distance = (_transform.position - item.Value.position).sqrMagnitude;
                    if (distance <= 1)
                        distance = 1;
                    item.Key.Temperature += (int)(_tempFactor / distance);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.ITEM))
        {
            CardObj card = other.GetComponent<CardObj>();
            if(card.GetItem() != null)
                AddObj(card);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.ITEM))
            RemoveObj(other.GetComponent<CardObj>());
    }
}
