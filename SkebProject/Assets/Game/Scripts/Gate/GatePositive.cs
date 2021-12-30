using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;
public class GatePositive : MonoBehaviour, ICollectable
{
    public float IncreaseAmount;
    public GameObject MySprite;
    public GameObject MyParticle;
    public GameObject MyText;
    public GameObject ArkaPlan;
    public GameObject MaterialObject;
    public void DoCollect()
    {
        TapticManager.Impact(ImpactFeedback.Medium);
        gameObject.GetComponent<Collider>().enabled = false;
        var Player = GameManager.Instance.Player;
        FemaleTurnAdjust();
        MaleTurnAdjust();
        Player.RelationCount += IncreaseAmount;
        var newParticle = Instantiate(GameManager.Instance.Data.Particles[14], transform.position, Quaternion.identity);
        Destroy(newParticle, 2f);
        Destroy(MySprite);
        Destroy(MyParticle);
        Destroy(MyText);
        Destroy(ArkaPlan);
    }

    // Start is called before the first frame update
    void Start()
    {
        AdjustTextObj();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPositionControl();
        //Debug.Log(Vector3.Distance(transform.position.normalized, GameManager.Instance.Player.transform.position.normalized));
        
    }
    private void AdjustTextObj()
    {
        int index = Random.Range(0, GameManager.Instance.TextData.PositiveTexts.Count);
        var newText = Instantiate(GameManager.Instance.TextData.PositiveTexts[index], transform.position, Quaternion.identity,transform);
        newText.transform.localPosition = new Vector3(0, 6f, 0);
        MyText = newText;
        MySprite.GetComponent<MeshRenderer>().material = GameManager.Instance.Data.GatePositiveMaterials[index];
    }

    private void FemaleTurnAdjust()
    {
        var Player = GameManager.Instance.Player;
        Player.Female.GetComponent<Female>().TrigAnimationTime(0.5f);
        Player.Female.GetComponent<Female>().IsTurn = true;
        Player.Female.GetComponent<Female>().HumanState = HumanState.TURN;
        Player.Female.GetComponent<Female>().AnimationPosition(HumanState.TURN);
    }

    private void MaleTurnAdjust()
    {
        var Player = GameManager.Instance.Player;
        Player.Male.GetComponent<Male>().TrigAnimationTime(0.5f);
        Player.Male.GetComponent<Male>().IsTurn = true;
        Player.Male.GetComponent<Male>().HumanState = HumanState.TURN;
        Player.Male.GetComponent<Male>().AnimationPosition(HumanState.TURN);
    }

    private void PlayerPositionControl()
    {
        if (transform.position.z<GameManager.Instance.Player.transform.position.z)
        {
            var newMaterial = GameManager.Instance.Data.AlphaMat;
            var alpha = 0;
            //DOTween.To(() => alpha, x => alpha = x, 0, 0.5f);
            newMaterial.color =new Color(MaterialObject.GetComponent<Renderer>().material.color.r, MaterialObject.GetComponent<Renderer>().material.color.g, MaterialObject.GetComponent<Renderer>().material.color.b,alpha);
            MaterialObject.GetComponent<MeshRenderer>().material =newMaterial;
            Destroy(MySprite);
            Destroy(MyParticle);
            Destroy(MyText);
            Destroy(ArkaPlan);
            Destroy(this);
            
        }
    }
}
