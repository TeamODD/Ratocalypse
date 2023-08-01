using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TurnUI : MonoBehaviour
{
    public List<Image> TestIconList;

    private List<Icon> _deployment = new List<Icon>();
    private List<int> _deploymentNumber = new List<int>(); 

    private Vector2 _size;
    private int _interval;

    public List<Icon> Deployment
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
        SetDeployment();
    }
    public void SetTest() 
    {
        foreach (Image a in TestIconList)
        {
            _deployment.Add(a.GetComponent<Icon>());
        }
    }
    public void SetDeployment()
    {
        _deploymentNumber.Clear();
        foreach (Icon a in _deployment) 
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
        foreach (Icon a in _deployment)
        {
            a.SetPosition = (a.Order ==-1) ? a.SetPosition = 
                new Vector3((_size.x - 20) / 2, -30 - 30 * NumberOfDuplicates(a), 0): 
                a.SetPosition = new Vector3(-(_size.x - 20) / 2 + _interval * _deploymentNumber.FindIndex(element => element == a.Order), -30 - 30 * NumberOfDuplicates(a), 0);;

        }
    }

    public int NumberOfDuplicates(Icon a)
    {
        int i = 0;
        foreach (Icon b in _deployment)
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
