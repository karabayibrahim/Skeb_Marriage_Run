using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAge : MonoBehaviour,ICollectable
{
    public GameObject MySprite;
    public GameObject MyParticle;
    public void DoCollect()
    {
        var Player = GameManager.Instance.Player;
        switch (Player.AgeStatus)
        {
            case AgeStatus.YOUNG:
                Player.AgeStatus = AgeStatus.ADULT;
                break;
            case AgeStatus.ADULT:
                Player.AgeStatus = AgeStatus.OLD;
                break;
            default:
                break;
        }
        Destroy(MySprite);
        Destroy(MyParticle);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
