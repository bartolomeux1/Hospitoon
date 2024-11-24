using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMinigame : MonoBehaviour
{
    public SirurgiaMinigame sirurgiaMinigame;
    public Animator corteAnimator;
    public Animator sangueAnimator;

    public GameObject PT2;
    public Animator costurarAnimator;
    public Animator sangue2Animator;
    
    public GameObject PT3;
    public Animator costurar2Animator;
    public Animator sangue3Animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sirurgiaMinigame.objective1Started)
        {
            PT3.SetActive(false);
        }
        if (sirurgiaMinigame.cutpoints1 > 0)
        {
            corteAnimator.SetInteger("CutPoints", sirurgiaMinigame.cutpoints1);
            sangueAnimator.SetInteger("CutPoints", sirurgiaMinigame.cutpoints1);
        }
        if(sirurgiaMinigame.cutpoints1 == 0)
        {
            corteAnimator.SetInteger("CutPoints", 0);
            sangueAnimator.SetInteger("CutPoints", 0);
        }
        if (sirurgiaMinigame.objective2Started)
        {
            PT2.SetActive(true);
        }
        /*if (sirurgiaMinigame.objective2Completed)
        {
            PT2.SetActive(false);
        }/**/
        if (sirurgiaMinigame.stitchPoints > 0)
        {
            costurarAnimator.SetInteger("StichPoints", sirurgiaMinigame.stitchPoints);
            sangue2Animator.SetInteger("StichPoints", sirurgiaMinigame.stitchPoints);
        }
        if (sirurgiaMinigame.stitchPoints == 0)
        {
            costurarAnimator.SetInteger("StichPoints", 0);
            sangue2Animator.SetInteger("StichPoints", 0);
        }
        if (sirurgiaMinigame.objective3Started)
        {
            PT2.SetActive(false);
            PT3.SetActive(true);
        }
        if (sirurgiaMinigame.stitchPoints2 > 0)
        {
            costurar2Animator.SetInteger("StichPoints", sirurgiaMinigame.stitchPoints2);
            sangue3Animator.SetInteger("StichPoints", sirurgiaMinigame.stitchPoints2);
        }
        if (sirurgiaMinigame.stitchPoints2 == 0)
        {
            costurar2Animator.SetInteger("StichPoints", 0);
            sangue3Animator.SetInteger("StichPoints", 0);
        }
    }
}
