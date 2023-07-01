using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.StatusEffectn
{
    public class StatusEffect : MonoBehaviour
    {/// <summary>
     ///  임의로 만든 상태이상 스크립트
     /// </summary>
        public string Type; // 디버프, 버프 실수형 구별 타입
        public Image Icon;
        public int CurrentTime; //지난 턴 값
        public float Persentage; // 값 범위
        public int Durration; // 상태이상 지속 턴


        public void Init()// 초기화
        {

        }

        void Execute() //턴제 타이머와 Image 턴마다 업데이트
        {
            if (CurrentTime == Durration) DeAction();
            //쿨타임 까지만
        }

        void DeAction()
        {//타이머가 다되었다면 스스로 이미지와 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}