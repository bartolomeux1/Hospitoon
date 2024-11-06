using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    public SirurgiaMinigame sirurgiaMinigame;

    public bool Task1 = false;
    public bool Task2 = false;
    public bool Task3 = false;
    private void Update()
    {
        if (sirurgiaMinigame.objective1Started)
            Task1 = true; //Task2 = false; Task3 = false;
        if (sirurgiaMinigame.objective2Started)
            Task2 = true;
        if (sirurgiaMinigame.objective3Started)
             Task3 = true; 
    }
    public void Hide()
    {
        if (Task1)
            sirurgiaMinigame.cutpoints1++;
        if (Task2)
            sirurgiaMinigame.stitchPoints++;
        if (Task3)
            sirurgiaMinigame.stitchPoints2++;
        gameObject.SetActive(false);
    }
}
