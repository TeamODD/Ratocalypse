using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.StatusEffect
{
    public class StatusEffectClass : MonoBehaviour
    {/// <summary>
     ///  ���Ƿ� ���� �����̻� ��ũ��Ʈ
     /// </summary>
        public string Type; // �����, ���� �Ǽ��� ���� Ÿ��
        public Image Icon;
        public int CurrentTime; //���� �� ��
        public float Persentage; // �� ����
        public int Durration; // �����̻� ���� ��


        public void Init()// �ʱ�ȭ
        {
            
        }

        void Execute() //���� Ÿ�̸ӿ� Image �ϸ��� ������Ʈ
        {
            if (CurrentTime == Durration) DeAction();
            //��Ÿ�� ������
        }

        void DeAction()
        {//Ÿ�̸Ӱ� �ٵǾ��ٸ� ������ �̹����� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
