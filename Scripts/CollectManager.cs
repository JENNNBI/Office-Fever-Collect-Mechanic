using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public GameObject paperPrefab;
    public Transform collectSpot;
    public bool isCollecting = false;
    PaperPrinting paperPrinting;

    public int maxPaper = 10;


    private void Start()
    {
        paperPrinting = GetComponent<PaperPrinting>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player geldi");
            isCollecting = true;
            StartCoroutine(CollectPaper());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player alandan çıktı");
            isCollecting = false;
        }
    }

    public IEnumerator CollectPaper()
    {
        int paperCount = playerList.Count;
        if (isCollecting == true)
        {
            if (paperCount <= maxPaper)
            {
                GameObject temp = Instantiate(paperPrefab, collectSpot);
                temp.transform.localPosition = new Vector3(.4f, playerList.Count * .2f, 0);
                playerList.Add(temp);

                paperPrinting.RemoveLast();
                Debug.Log("paper alınıyor");

                yield return new WaitForSeconds(1);
            }
            else if (paperCount == maxPaper)
            {
                isCollecting = false;
                Debug.Log("paper max!!");
            }
        }
    }
}