using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GataNegative : MonoBehaviour,ICollectable
{
    public float IncreaseAmount;
    public GameObject MySprite;
    public GameObject MyParticle;

    public void DoCollect()
    {
        GameManager.Instance.Player.RelationCount += IncreaseAmount;
        //var Player = GameManager.Instance.Player;
        //switch (Player.AgeStatus)
        //{
        //    case AgeStatus.YOUNG:
        //        Player.AgeStatus = AgeStatus.ADULT;
        //        break;
        //    case AgeStatus.ADULT:
        //        Player.AgeStatus = AgeStatus.OLD;
        //        break;
        //    default:
        //        break;
        //}
        Destroy(MySprite);
        Destroy(MyParticle);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomSpriteAdjust();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomSpriteAdjust()
    {
        MySprite.GetComponent<MeshRenderer>().material = GameManager.Instance.Data.GateNegativeMaterials[Random.Range(0, GameManager.Instance.Data.GateNegativeMaterials.Count)];
    }
}
