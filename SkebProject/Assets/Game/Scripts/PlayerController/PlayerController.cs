using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public RelationStatus _relationStatus;
    public AgeStatus _ageStatus;
    public Human Male;
    public Human Female;
    public float Speed;
    public float swerveSpeed;
    public static Action WalkAction;
    public static Action IdleAction;
    public float MoveFactorX => _moveFactorX;

    private float _reletionCount;
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    public float RelationCount
    {
        get
        {
            return _reletionCount;
        }
        set
        {
            if (RelationCount == value)
            {
                return;
            }
            _reletionCount = value;
            OnRelationCountControl();
            OnProgressBarControl();
        }
    }

    private void OnProgressBarControl()
    {
        if (RelationCount>=0)
        {
            GameManager.Instance.UIManager.Bar.fillAmount = RelationCount / 100f;
        }
        else
        {
            GameManager.Instance.UIManager.Bar.fillAmount = -RelationCount / 100f;
        }
    }

    private void OnRelationCountControl()
    {
        if (RelationCount<-100)
        {
            RelationCount = -100;
        }

        else if (RelationCount >= -100 && RelationCount < -50)
        {
            RelationStatus = RelationStatus.TERRIBLE;
        }

        else if (RelationCount >= -50 && RelationCount < 0)
        {
            RelationStatus = RelationStatus.BAD;
        }

        else if (RelationCount >= 0 && RelationCount < 50)
        {
            RelationStatus = RelationStatus.NORMAL;
        }
        else if (RelationCount >= 50 && RelationCount < 75)
        {
            RelationStatus = RelationStatus.GOOD;
        }
        else if (RelationCount>=75 && RelationCount <100)
        {
            RelationStatus = RelationStatus.EXCELLENT;
        }
        else if (RelationCount>=100)
        {
            RelationCount = 100;
        }
    }

    public AgeStatus AgeStatus
    {
        get
        {
            return _ageStatus;
        }
        set
        {
            if (AgeStatus == value)
            {
                return;
            }
            _ageStatus = value;
            OnAgeStatusChanged();
        }
    }


    public RelationStatus RelationStatus
    {
        get
        {
            return _relationStatus;
        }
        set
        {
            if (RelationStatus == value)
            {
                return;
            }
            _relationStatus = value;
            OnRelationStatusChanged();
        }
    }


    private void OnRelationStatusChanged()
    {
        switch (RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                var newParticle4 = Instantiate(GameManager.Instance.Data.Particles[1], new Vector3(0,4f,0), Quaternion.identity,transform);
                newParticle4.transform.localPosition = new Vector3(0, 10f, 0);
                Destroy(newParticle4, 2f);
                GameManager.Instance.UIManager.Bar.color = Color.black;
                GameManager.Instance.UIManager.StatusTextAdjust("Terrible", Color.black);
                Speed = 6f;
                break;
            case RelationStatus.BAD:
                var newParticle3 = Instantiate(GameManager.Instance.Data.Particles[1], new Vector3(0, 4f, 0), Quaternion.identity,transform);
                newParticle3.transform.localPosition = new Vector3(0, 10f, 0);
                Destroy(newParticle3, 5f);
                GameManager.Instance.UIManager.Bar.color = Color.red;
                GameManager.Instance.UIManager.StatusTextAdjust("Bad", Color.red);
                Speed = 8f;
                break;
            case RelationStatus.NORMAL:
                Color color = new Color32(255, 165, 0, 255);
                GameManager.Instance.UIManager.Bar.color = color;
                GameManager.Instance.UIManager.StatusTextAdjust("Normal", color);
                CouplePositionAdjust(new Vector3(4, 0, 0), new Vector3(-4, 0, 0));
                gameObject.GetComponent<BoxCollider>().size = new Vector3(11f, 12f, 1f);
                Speed = 10f;
                break;
            case RelationStatus.GOOD:
                var newParticle = Instantiate(GameManager.Instance.Data.Particles[0], new Vector3(0, 4f, 0), Quaternion.identity,transform);
                newParticle.transform.localPosition = new Vector3(0, 10f, 0);
                Destroy(newParticle, 2f);
                GameManager.Instance.UIManager.Bar.color = Color.yellow;
                GameManager.Instance.UIManager.StatusTextAdjust("Good", Color.yellow);
                CouplePositionAdjust(new Vector3(1.3f, 0f, 0), new Vector3(-1.3f, 0f, 0));
                gameObject.GetComponent<BoxCollider>().size = new Vector3(6f, 12f, 1f);
                Speed = 15f;
                break;
            case RelationStatus.EXCELLENT:
                var newParticle2 = Instantiate(GameManager.Instance.Data.Particles[0], new Vector3(0, 4f, 0), Quaternion.identity, transform);
                newParticle2.transform.localPosition = new Vector3(0, 10f, 0);
                Destroy(newParticle2, 2f);
                GameManager.Instance.UIManager.Bar.color = Color.green;
                GameManager.Instance.UIManager.StatusTextAdjust("Excellent", Color.green);
                CouplePositionAdjust(new Vector3(0, 0, 0), new Vector3(0f, 3, 1.3f));
                Speed = 20f;
                break;
            default:
                break;
        }
    }

    private void OnAgeStatusChanged()
    {
        switch (AgeStatus)
        {
            case AgeStatus.YOUNG:
                GameManager.Instance.GameStateIndex = 0;
                GameManager.GameStateChanged?.Invoke();
                CharacterChanged();
                break;
            case AgeStatus.ADULT:
                GameManager.Instance.GameStateIndex = 1;
                GameManager.GameStateChanged?.Invoke();
                CharacterChanged();
                break;
            case AgeStatus.OLD:
                GameManager.Instance.GameStateIndex = 2;
                GameManager.GameStateChanged?.Invoke();
                CharacterChanged();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ICollectable>() != null)
        {
            other.gameObject.GetComponent<ICollectable>().DoCollect();
        }
    }

    void Start()
    {
        RelationStatus = RelationStatus.NORMAL;
        GameManager.Instance.UIManager.Bar.fillAmount = RelationCount / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSystem();
        float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
        transform.Translate(swerveAmount, 0, 0);
    }

    private void MoveSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
            transform.Translate(0, 0, Speed * Time.deltaTime);
            WalkAction?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            IdleAction?.Invoke();
        }
    }

    private void CharacterChanged()
    {
        Destroy(Male);
        Destroy(Female);
        Male = Instantiate(GameManager.Instance.Data.Males[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity);
        Female = Instantiate(GameManager.Instance.Data.Females[GameManager.Instance.GameStateIndex], transform.position, Quaternion.identity);
    }

    private void CouplePositionAdjust(Vector3 _malePos, Vector3 _femalePos)
    {
        Male.transform.DOLocalMove(_malePos, 0.5f).SetEase(Ease.OutCubic);
        Female.transform.DOLocalMove(_femalePos, 0.5f).SetEase(Ease.OutCubic);
    }
}
