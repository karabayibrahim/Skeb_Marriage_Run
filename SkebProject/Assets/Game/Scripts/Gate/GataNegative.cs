using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GataNegative : MonoBehaviour,ICollectable
{
    public float IncreaseAmount;
    public GameObject MySprite;
    public GameObject MyParticle;
    public GameObject MyText;
    public void DoCollect()
    {
        GameManager.Instance.Player.RelationCount -= IncreaseAmount;
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
        Destroy(MyText);
    }

    // Start is called before the first frame update
    void Start()
    {
        AdjustTextObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AdjustTextObj()
    {
        int index = Random.Range(0, GameManager.Instance.TextData.NegativeTexts.Count);
        var newText = Instantiate(GameManager.Instance.TextData.NegativeTexts[index], transform.position, Quaternion.identity,transform);
        newText.transform.localPosition = new Vector3(0, 6f, 0);
        MyText = newText;
        MySprite.GetComponent<MeshRenderer>().material = GameManager.Instance.Data.GateNegativeMaterials[index];
    }
}
