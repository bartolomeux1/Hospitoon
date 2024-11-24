using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButtons : MonoBehaviour
{
    public Color objectColor;
    public SirurgiaMinigame sirurgiaMinigame;

    public bool Task1 = false;
    public bool Task2 = false;
    public bool Task3 = false;

    public bool isEbleed = true;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = objectColor;
    }
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(objectColor.r, objectColor.g, objectColor.b, 0.5f);
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = objectColor;
    }
    private void OnMouseDown()
    {
        if (isEbleed)
        {
            if (Task1)
                sirurgiaMinigame.cutpoints1++;
            if (Task2)
                sirurgiaMinigame.stitchPoints++;
            if (Task3)
                sirurgiaMinigame.stitchPoints2++;

            GetComponent<SpriteRenderer>().color = objectColor;
            gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        Invoke("Ebleed", 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (sirurgiaMinigame.objective1Started)
            Task1 = true; //Task2 = false;
            Task3 = false;
        //Invoke("Ebleed", 1);
        if (sirurgiaMinigame.objective2Started)
            Task2 = true;
        //Invoke("Ebleed", 1);
        if (sirurgiaMinigame.objective3Started)
            Task3 = true;
        //Invoke("Ebleed", 1);

        if (sirurgiaMinigame.objective1Completed)
            Task1 = false;
            //isEbleed = false;
        if (sirurgiaMinigame.objective2Completed)
            Task2 = false;
            //isEbleed = false;
        if (sirurgiaMinigame.objective3Completed)
            Task3 = false;
            //isEbleed = false;
    }
    private void Ebleed()
    {
        isEbleed = true;
    }
}
