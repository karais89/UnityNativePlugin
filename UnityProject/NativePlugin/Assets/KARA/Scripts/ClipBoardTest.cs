namespace KARA
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ClipBoardTest : MonoBehaviour
    {
        void Start()
        {
            UnityNativePlugin.Instance.ClipBoardCopy("클립보드 테스트 용도");
        }
    }
}