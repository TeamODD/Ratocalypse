using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    /// <summary>
    /// 개인적으로 알고리즘 만 구현한 상태
    /// 깃 파일과 합칠려면 상당한 수정 필요.   
    /// </summary>
    public Vector2 size;
    public List<Image> CreatureIcon;

    [Serializable]
    struct IconDeployment
    {
        public int Number; // 순서
        public List<Image> DataList; // 아이콘 리스트
    }
    [SerializeField] 
    List<IconDeployment> IconDeploymentList = new List<IconDeployment>();

    public float MoveSpeed = 1f;
    public int Interval;
    Vector3 Dir;

    // Start is called before the first frame update
    void Start()
    {
        // creature -> Hp, 스테미나,좌표 --> creature.LIst -> 행동 순서와 
        size = GetComponent<RectTransform>().sizeDelta;

    }

    // Update is called once per frame
    public void Update()
    {
        Interval = Mathf.RoundToInt((size.x - 20) / CreatureIcon.Count);
        Deployment();
        SetTrun();
    }

    public void Deployment()
    {
        IconDeployment tmp;

        foreach (Image a in CreatureIcon) // 계산 값 비교해서 리스트로 값 밇어 넣어서 정리
        {
            tmp = new IconDeployment();
            tmp.DataList = new List<Image>();
            int Number = a.GetComponent<CretureIcon>().Number;
            tmp.Number = Number;

            if (IconDeploymentList.Count == 0)
            {
                tmp.DataList.Add(a);
                IconDeploymentList.Add(tmp);
            }
            else if (!IconDataConation(Number)) // 다른 숫자의 중복값 제거하면서 갑 추가
            {
                tmp.DataList.Add(a);
                Deduplication(Number,a);
                IconDeploymentList.Add(tmp);
            }
            else if (IconDataConation(Number)) //중복만 수정하기
            {
                tmp.DataList.Add(a);
                if (!IconDeploymentList[IconDataSeekIndex(Number)].DataList.Contains(a)) 
                {
                    Deduplication(Number, a);
                    IconDeploymentList[IconDataSeekIndex(Number)].DataList.Add(a); 
                }
            }
            if (Number == -1)
            {
                SwapLast(IconDataSeekIndex(Number));
            }
        }
    }

    void Deduplication(int N, Image Target) 
    {
        foreach (IconDeployment c in IconDeploymentList)
        {
            if (c.Number !=N && c.DataList.Contains(Target)) 
            {
                c.DataList.Remove(Target);
            }
        }
    }
    bool IconDataConation(int A)
    {
        foreach (IconDeployment c in IconDeploymentList)
        {
            if (A == c.Number) { return true; }
        }
        return false;
    }
    int IconDataSeekIndex(int A)
    {
        int i = 0;
        foreach (IconDeployment c in IconDeploymentList)
        {
            if (A == c.Number) { return i; }
            i++;
        }
        Debug.Log("못찾았어요");
        return -1;
    }

    int IconDataSeekIndex(IconDeployment A)
    {
        int i = 0;
        foreach (IconDeployment c in IconDeploymentList)
        {
            if (A.Number == c.Number && A.DataList == c.DataList) { return i; }
            i++;
        }
        Debug.Log("못찾았어요");
        return -1;
    }
    int DataListSeekIndex(Image a, IconDeployment B) 
    {
        int i = 0;
        foreach (Image c in B.DataList)
        {
            if (a== c) { return i; }
            i++;
        }
        return -2;
    }
    void SwapLast(int a) 
    {
      
        if (IconDeploymentList.Count - 1!=a) 
        {
            IconDeployment tmp = IconDeploymentList[IconDeploymentList.Count - 1];
            IconDeploymentList[IconDeploymentList.Count - 1] = IconDeploymentList[a];
            IconDeploymentList[a] = tmp;
        }

    }

    public void SetTrun()
    {
        /*foreach (Image a in CreatureIcon) //  배치
        {
            Dir = new Vector3(-(size.x - 20) / 2 + Interval * a.GetComponent<IconData>().Number, -30, 0) - a.rectTransform.localPosition;
            a.rectTransform.localPosition += Dir * MoveSpeed * Time.deltaTime;
        }*/
        foreach (IconDeployment D in IconDeploymentList) //  배치
        {
            D.DataList.RemoveAll(s=>s == null);
            IconDeploymentList.RemoveAll(d => d.DataList.Count == 0);

            foreach (Image a in D.DataList) //  배치
            {
              
                if (a.GetComponent<CretureIcon>().Number == -1)
                {
                    Dir = new Vector3((size.x - 20) / 2, -30 - 15 * DataListSeekIndex(a, D), 0) - a.rectTransform.localPosition;
                    a.rectTransform.localPosition = Vector3.MoveTowards(a.rectTransform.localPosition, new Vector3((size.x - 20) / 2, -30 - 30 * DataListSeekIndex(a, D), 0), Dir.magnitude * Time.deltaTime*3);
                }
                else if(IconDataSeekIndex(D) != -1)
                { 
                    Dir = new Vector3(-(size.x - 20) / 2 + Interval * IconDataSeekIndex(D), -30 - 15 * DataListSeekIndex(a, D), 0) - a.rectTransform.localPosition;
                    a.rectTransform.localPosition = Vector3.MoveTowards(a.rectTransform.localPosition, new Vector3(-(size.x - 20) / 2 + Interval * IconDataSeekIndex(D), -30 - 30 * DataListSeekIndex(a, D), 0), Dir.magnitude * Time.deltaTime * 3);
                }
            }

        }
    }

}