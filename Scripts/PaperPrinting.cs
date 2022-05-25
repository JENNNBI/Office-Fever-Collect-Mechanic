using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PaperPrinting : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public GameObject paperPrefab;
    public GameObject printer;
    public bool isWorking = true;
    private CollectManager collectManager;


    public int stackCount = 10;
    public Transform collectSpot;


    void Start()
    {
        collectManager = GetComponent<CollectManager>();
        StartCoroutine(PrintPaper());
    }

    public void RemoveLast()
    {
        Vector3 collectPosition = collectSpot.transform.position;
        if (paperList.Count > 0)
        {
            paperList[paperList.Count - 1].transform.DOLocalJump
                (new Vector3(collectPosition.x, collectManager.playerList.Count *.2f, collectPosition.z), 5, 1, 1);
            // paperList[paperList.Count - 1].transform.DOLocalMoveX(collectPosition.x,1);
            // paperList[paperList.Count - 1].transform.DOLocalMoveY(collectManager.playerList.Count *.2f,1);
            // paperList[paperList.Count - 1].transform.DOLocalMoveZ(collectPosition.z,1);
            paperList[paperList.Count - 1].transform.DOScale(1, 1);
            Destroy(paperList[paperList.Count - 1], .5f);
            paperList.RemoveAt(paperList.Count - 1);
        }
    }

    public IEnumerator PrintPaper()
    {
        while (true)
        {
            float paperCount = paperList.Count;
            int rowCount = (int) paperCount / stackCount;
            if (isWorking)
            {
                GameObject temp = Instantiate(paperPrefab);
                var position = printer.transform.position;
                temp.transform.localScale = Vector3.zero;
                temp.transform.DOScale(new Vector3(3, 3, 3), .5f);
                temp.transform.localPosition =
                    new Vector3(position.x - ((float) rowCount % 3),
                        (position.y - 1.3f) + (paperCount % stackCount) / 5, position.z);
                paperList.Add(temp);

                if (paperList.Count >= 20)
                {
                    isWorking = false;
                }
            }
            else if (paperList.Count < 20)
            {
                isWorking = true;
            }

            yield return new WaitForSeconds(.2f);
        }
    }
}