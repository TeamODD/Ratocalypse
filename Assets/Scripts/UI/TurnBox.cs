using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Deployment;

public class TurnBox : MonoBehaviour
{
    public Vector2 size;
    public List<Image> CreatureIcon; // 임의 데이터
    public float MoveSpeed = 1f;
    public int Interval;
    Vector3 Dir;

    [SerializeField]
    private Deployment deployment;

    void Start()
    {

        size = GetComponent<RectTransform>().sizeDelta;
    }

    private void Update()
    {
        SetTrun();
      
    }

    public void SetTrun()
    {
        deployment.DeploymentDataSort();
        deployment.IconList = CreatureIcon;
        Interval = Mathf.RoundToInt((size.x - 20) / CreatureIcon.Count);
        foreach (DeploymentData D in deployment.datas) //  이동
        {
            foreach (Image a in D.DataList) 
            {
                if (a.GetComponent<CretureIcon>().Number == -1)
                {
                    Dir = new Vector3((size.x - 20) / 2, -30 - 15 * deployment.DeploymentDataContain(a, D), 0) - a.rectTransform.localPosition;
                    a.rectTransform.localPosition = Vector3.MoveTowards(a.rectTransform.localPosition, new Vector3((size.x - 20) / 2, -30 - 30 * deployment.DeploymentDataContain(a, D), 0), Dir.magnitude * Time.deltaTime * 3);
                }
                if (a.GetComponent<CretureIcon>().Number != -1)
                {
                    Dir = new Vector3(-(size.x - 20) / 2 + Interval * deployment.DeploymentContainAt(D), -30 - 15 * deployment.DeploymentDataContain(a, D), 0) - a.rectTransform.localPosition;
                    a.rectTransform.localPosition = Vector3.MoveTowards(a.rectTransform.localPosition, new Vector3(-(size.x - 20) / 2 + Interval * deployment.DeploymentContainAt(D), -30 - 30 * deployment.DeploymentDataContain(a, D), 0), Dir.magnitude * Time.deltaTime * 3);

                }
            }
        }
    }

}
