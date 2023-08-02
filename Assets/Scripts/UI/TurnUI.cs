using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TurnUI : MonoBehaviour
{
    public List<Image> TestIconList;

    private List<IIcon> _deployment = new List<IIcon>();
    [SerializeField]
    private List<int> _deploymentNumber = new List<int>(); 

    private Vector2 _size;
    private int _interval;

    public List<IIcon> Deployment
    {
        set { _deployment = value;}
    }

    private void Start()
    {
        _size = GetComponent<RectTransform>().sizeDelta;
    }

    private void Update()
    {
        SetTest();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SetDeployment();
        }
    }
    public void SetTest() 
    {
        foreach (Image a in TestIconList)
        {
            _deployment.Add(a.GetComponent<IIcon>());
        }
    }
    public void SetDeployment()
    {
        _deploymentNumber.Clear();
        foreach (IIcon a in _deployment) 
        {
           if (!_deploymentNumber.Exists(x=>x== a.Order)) 
           {
                 if (a.Order != -1) 
                 {
                    _deploymentNumber.Add(a.Order);
                 }
           }
        }
        if (_deploymentNumber.Exists(x => x == -1))
        {
           if (!_deploymentNumber.FindIndex(element => element == -1).Equals(_deploymentNumber.Count - 1))
            {
                _deploymentNumber.Remove(_deploymentNumber.FindIndex(element => element == -1));
            }
        }
        _deploymentNumber.Sort();

        _interval = Mathf.RoundToInt((_size.x - 20) / _deploymentNumber.Count);
        foreach (IIcon a in _deployment)
        {
            a.SetPosition((a.Order == -1) ? new Vector3((_size.x - 20) / 2, -30 - 30 * NumberOfDuplicates(a), 0) : new Vector3(-(_size.x - 20) / 2 + _interval * _deploymentNumber.FindIndex(element => element == a.Order), -30 - 30 * NumberOfDuplicates(a), 0));
        }
    }

    public int NumberOfDuplicates(IIcon a)
    {
        int i = 0;
        foreach (IIcon b in _deployment)
        {
            if (b.Order == a.Order && b != a)
            {
                i++;
            }
            if(b.Order == a.Order && b == a)
            {
                break;
            }
        }
        return i;
    }

    public void ActivationChange()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
