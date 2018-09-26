using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] playerControlScripts;

    private PhotonView photonView;

    Character c;

    private void Awake()
    {
        c = GetComponent<Character>();
        photonView = GetComponent<PhotonView>();
        Initialize();
        Canvas[] ca = gameObject.GetComponentsInChildren<Canvas>();

        foreach (Canvas cc in ca)
        {
            if (cc.gameObject.name.Equals("NonVisibleCanvas"))
            {   
                PopupController.Initialize(cc);
            }
        }
       
        
    }
    private void Initialize()
    {
        if (photonView.isMine)
        {
            gameObject.layer = LayerMask.NameToLayer("isSelf");

        }
        else
        {
            
            playerCamera.SetActive(false);
            foreach (MonoBehaviour m in playerControlScripts)
            {
                m.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (!photonView.isMine) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            c.currentHealth -= 5;
          
            
            PopupController.CreateText(Random.Range(-100, -5).ToString());
        }
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(c.currentHealth);
        }
        else if(stream.isReading)
        {
            c.currentHealth = (int)stream.ReceiveNext();
        }
    }
}
