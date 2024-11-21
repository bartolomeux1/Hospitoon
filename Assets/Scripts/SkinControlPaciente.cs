using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinControlPaciente : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] skins;
    public PhotonView pView;
    public Animator animator;

    void Awake()
    {
        //pView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        //pView.RPC("ChangeSkin", RpcTarget.AllBuffered, Random.Range(0, skins.Length));
        //CallChangeSkin();
        ChangeSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void  CallChangeSkin()
    {
        pView.RPC("ChangeSkin", RpcTarget.All);
    }
    //change the skin randonly
    [PunRPC]
    private void ChangeSkin()
    {
        //animator.runtimeAnimatorController = skins[index];
        animator.runtimeAnimatorController = skins[Random.Range(0, skins.Length)];

    }
}
