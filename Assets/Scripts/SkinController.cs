using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SkinController : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] skins;
    private PhotonView pView;
    private Animator charAnimator;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        charAnimator = transform.GetChild(3).GetComponent<Animator>();
    }

    public void CallChangeSkin(int index)
    {
        charAnimator.runtimeAnimatorController = skins[index];
        pView.RPC("ChangeSkin", RpcTarget.OthersBuffered, index, pView.ViewID);
    }

    [PunRPC]
    private void ChangeSkin(int index, int viewID)
    {
        if (viewID == pView.ViewID)
        {
            charAnimator.runtimeAnimatorController = skins[index];
        }
    }
}
