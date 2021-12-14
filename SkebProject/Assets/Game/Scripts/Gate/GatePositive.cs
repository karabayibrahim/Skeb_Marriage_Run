using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePositive : MonoBehaviour,ICollectable
{
    public float IncreaseAmount;

    public void DoCollect()
    {
        GameManager.Instance.Player.RelationCount += IncreaseAmount;
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
        Destroy(gameObject);
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
