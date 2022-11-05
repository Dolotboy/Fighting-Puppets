using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrettArnett
{
    public class ChangeCanvas : MonoBehaviour
    {
        public GameObject CanvasA;
        public GameObject CanvasB;
        public GameObject CanvasC;
        public GameObject CanvasD;

        public void ToA()
        {
            CanvasB.SetActive(false);
            CanvasC.SetActive(false);
            CanvasD.SetActive(false);
            CanvasA.SetActive(true);
        }

        public void ToB()
        {
            CanvasA.SetActive(false);
            CanvasC.SetActive(false);
            CanvasD.SetActive(false);
            CanvasB.SetActive(true);
        }

        public void ToC()
        {
            CanvasA.SetActive(false);
            CanvasB.SetActive(false);
            CanvasD.SetActive(false);
            CanvasC.SetActive(true);
        }

        public void ToD()
        {
            CanvasA.SetActive(false);
            CanvasB.SetActive(false);
            CanvasC.SetActive(false);
            CanvasD.SetActive(true);
        }
    }
}