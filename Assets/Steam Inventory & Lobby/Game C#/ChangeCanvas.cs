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

        public void ToGameButtons()
        {
            CanvasB.SetActive(false);
            CanvasC.SetActive(false);
            CanvasD.SetActive(false);
            CanvasA.SetActive(true);
        }

        public void ToLobbySettings()
        {
            CanvasA.SetActive(false);
            CanvasC.SetActive(false);
            CanvasD.SetActive(false);
            CanvasB.SetActive(true);
        }

        public void ToCreateLobby()
        {
            CanvasA.SetActive(false);
            CanvasB.SetActive(false);
            CanvasD.SetActive(false);
            CanvasC.SetActive(true);
        }

        public void ToLobbyList()
        {
            CanvasA.SetActive(false);
            CanvasB.SetActive(false);
            CanvasC.SetActive(false);
            CanvasD.SetActive(true);
        }
    }
}