using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Deployment : MonoBehaviour
{
    [SerializeField]
    public List<Image> IconList;
    [Serializable]
    public struct DeploymentData
    {
        public int Number;
        public List<Image> DataList;
    }

    public List<DeploymentData> datas = new List<DeploymentData>();

    public int DeploymentContain(int target)
    {
        int i = 0;
        foreach (DeploymentData c in datas)
        {
            if (target == c.Number) { return i; }
            i++;
        }
        return -2;
    }
    public int DeploymentContainAt(DeploymentData target)
    {
        int i = 0;
        foreach (DeploymentData c in datas)
        {
            if (target.Number == c.Number && target.DataList == c.DataList) { return i; }
            i++;
        }
        return -2;
    }

    public int DeploymentDataContain(Image target, DeploymentData deploymentdata)
    {
        int i = 0;
        foreach (Image Ob in deploymentdata.DataList)
        {
            if (target == Ob) { return i; }
            i++;
        }
        return -2;
    }
    public void Swap(int current)
    {
        DeploymentData tmp = datas[current];
        if (current != datas.Count - 1) {datas.Remove(tmp); if (!datas.Exists(x => x.Number == current)) datas.Add(tmp); }

    }

    public void DeleteDuplicate(Image target)
    {//DeploymentData
        foreach (DeploymentData c in datas)
        {
            if (c.DataList.Contains(target))    
            {
                c.DataList.Remove(target);
                c.DataList.RemoveAll(s => s == null);
            }
        }
        datas.RemoveAll(d => d.DataList.Count == 0);
    }
    public void DeploymentDataSort()
    {
        //값의 재배치
        DeploymentData tmp;
       
        foreach (Image target in IconList) // 계산 값 비교해서 리스트로 값 밇어 넣어서 정리
        {
            tmp = new DeploymentData();
            tmp.DataList = new List<Image>();
            int number = target.GetComponent<CretureIcon>().Number;
            tmp.Number = number;


            if (datas.Count == 0)
            {          
                tmp.DataList.Add(target);
                datas.Add(tmp);
            }
            else if (DeploymentContain(number)==-2) // 다른 숫자의 중복값 제거하면서 갑 추가
            {
                tmp.DataList.Add(target);
                DeleteDuplicate(target);
                datas.Add(tmp);
                datas = datas.OrderBy(x => x.Number).ToList();
            }
            else if (DeploymentContain(number) != -2) //중복만 수정하기, 정렬
            {
                tmp.DataList.Add(target);
                if (!datas[DeploymentContain(number)].DataList.Contains(target))
                {
                    DeleteDuplicate(target);
                    datas[DeploymentContain(number)].DataList.Add(target);
                    datas = datas.OrderBy(x => x.Number).ToList();
                }
            }

            if (number == -1)
            {
                Swap(DeploymentContain(number));
            }
        }
    }
}
