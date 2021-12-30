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
    public Human NewMale;
    public float Speed;
    public float HorizontalSpeed;
    public float swerveSpeed;
    public static Action WalkAction;
    public static Action IdleAction;
    public int WalkRandomIndex;
    public int IdleRandomIndex;
    public bool IsClampRight = false;
    public bool IsClampLeft = false;
    public float MoveFactorX => _moveFactorX;

    private float _reletionCount;
    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    private float _movementClampPositive;
    private float _movementClampNegative;
    public Vector3 startPosition;

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
        if (RelationCount >= 0)
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
        if (RelationCount < -100)
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
        else if (RelationCount >= 75 && RelationCount < 100)
        {
            RelationStatus = RelationStatus.EXCELLENT;
        }
        else if (RelationCount >= 100)
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
                AdjustMovementClamp(7.5f, -23f);
                NewMale = NewHumanSpawn();
                ParticleSpawn(1);
                GameManager.Instance.UIManager.Bar.color = Color.black;
                GameManager.Instance.UIManager.StatusTextAdjust("Terrible", Color.black);
                CouplePositionAdjust(new Vector3(3, 0, 0), new Vector3(-3f, 3, 1.3f));
                //Speed = 10f;
                break;
            case RelationStatus.BAD:
                AdjustMovementClamp(7f, -22f);
                NewMaleDestroy();
                ParticleSpawn(1);
                GameManager.Instance.UIManager.Bar.color = Color.red;
                GameManager.Instance.UIManager.StatusTextAdjust("Bad", Color.red);
                CouplePositionAdjust(new Vector3(4, 0, 0), new Vector3(-4f, 0, 0f));
                //Speed = 12f;
                break;
            case RelationStatus.NORMAL:
                AdjustMovementClamp(8.5f, -24f);
                NewMaleDestroy();
                Color color = new Color32(255, 165, 0, 255);
                GameManager.Instance.UIManager.Bar.color = color;
                GameManager.Instance.UIManager.StatusTextAdjust("Normal", color);
                CouplePositionAdjust(new Vector3(2, 0, 0), new Vector3(-2, 0, 0));
                gameObject.GetComponent<BoxCollider>().size = new Vector3(8.5f, 12f, 1f);
                //Speed = 14f;
                break;
            case RelationStatus.GOOD:
                AdjustMovementClamp(9.5f, -24.5f);
                NewMaleDestroy();
                ParticleSpawn(0);
                GameManager.Instance.UIManager.Bar.color = Color.yellow;
                GameManager.Instance.UIManager.StatusTextAdjust("Good", Color.yellow);
                CouplePositionAdjust(new Vector3(1.4f, 0f, 0), new Vector3(-1f, 0f, 0));
                gameObject.GetComponent<BoxCollider>().size = new Vector3(6f, 12f, 1f);
                //Speed = 16f;
                break;
            case RelationStatus.EXCELLENT:
                AdjustMovementClamp(10.5f, -26.5f);
                NewMaleDestroy();
                ParticleSpawn(0);
                GameManager.Instance.UIManager.Bar.color = Color.green;
                GameManager.Instance.UIManager.StatusTextAdjust("Excellent", Color.green);
                CouplePositionAdjust(new Vector3(0, 0, 0), new Vector3(0f, 3, 1.3f));
                //Speed = 18f;
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
                break;
            case AgeStatus.ADULT:
                GameManager.Instance.GameStateIndex = 1;
                break;
            case AgeStatus.OLD:
                GameManager.Instance.GameStateIndex = 2;
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
        if (other.gameObject.GetComponent<IClampCollect>() != null)
        {
            other.gameObject.GetComponent<IClampCollect>().DoClampCollect(other.ClosestPointOnBounds(transform.position));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IClampCollect>() != null)
        {
            other.gameObject.GetComponent<IClampCollect>().DoClampCollectExit(other.ClosestPointOnBounds(transform.position));
        }
    }

    void Start()
    {
        GameManager.Instance.UIManager.NextButton.onClick.AddListener(NextLevelSettings);
        startPosition = transform.position;
        Speed = 31f;
        RelationStatus = RelationStatus.NORMAL;
        AdjustMovementClamp(8.5f, -24f);
        GameManager.Instance.UIManager.Bar.fillAmount = RelationCount / 100f;
        Debug.Log(AgeCalculator());
    }

    private void OnDestroy()
    {
        if (GameManager.Instance!=null)
        {
            GameManager.Instance.UIManager.NextButton.onClick.RemoveListener(NextLevelSettings);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.UIManager.GameStart==false)
        {
            return;
        }
        MovementInput();
        //float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
        //transform.Translate(swerveAmount, 0, 0);
        MoveSystem();
        HorizontalMovement();
    }


    private void MoveSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdjustWalkRandomIndex();
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            transform.Translate(0, 0, Speed * Time.deltaTime);
            MoveControlSystem();
            
            //if (IsClampRight)
            //{
            //    if (MoveFactorX <= 0)
            //    {
            //    }
            //}
            //else if (IsClampLeft)
            //{
            //    if (MoveFactorX >= 0)
            //    {
            //        MoveControlSystem();
            //    }
            //}
            //else
            //{
            //    MoveControlSystem();
            //}


        }
        else if (Input.GetMouseButtonUp(0))
        {
            AdjustIdleRandomIndex();
            _moveFactorX = 0f;
            IdleAction?.Invoke();
        }
    }


    private void CouplePositionAdjust(Vector3 _malePos, Vector3 _femalePos)
    {
        Male.transform.DOLocalMove(_malePos, 0.1f);
        Female.transform.DOLocalMove(_femalePos, 0.1f);
    }

    private Human NewHumanSpawn()
    {
        var _newHuman = Instantiate(GameManager.Instance.Data.NewMale, transform.position, Quaternion.identity, transform);
        _newHuman.transform.localPosition = new Vector3(-3, 0, 0);
        return _newHuman;
    }

    private void NewMaleDestroy()
    {
        if (NewMale != null)
        {
            Destroy(NewMale.gameObject);
        }
    }

    private void ParticleSpawn(int _index)
    {
        var newParticle = Instantiate(GameManager.Instance.Data.Particles[_index], new Vector3(0, 4f, 0), Quaternion.identity, transform);
        newParticle.transform.localPosition = new Vector3(0, 7f, 0);
        Destroy(newParticle, 2f);
    }

    public float AgeCalculator()
    {
        var finishPoz = GameManager.Instance.CurrentLevel.Finish.transform.position.z;
        var distance = gameObject.transform.position.z / finishPoz;
        return distance;
        //if (GameManager.Instance.CurrentLevel.Finish != null)
        //{
            
        //}
        //else
        //{
        //    var finishPoz = GameManager.Instance.CurrentLevel.Finish.transform.position.z;
        //    var distance = gameObject.transform.position.z / finishPoz;
        //    return distance;
        //}


    }

    private void AdjustWalkRandomIndex()
    {
        WalkRandomIndex = UnityEngine.Random.Range(0, 2);
        //Debug.Log(WalkRandomIndex);
    }

    private void AdjustIdleRandomIndex()
    {
        IdleRandomIndex = UnityEngine.Random.Range(0, 2);
    }

    void MovementInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
        }
    }
    private void MoveControlSystem()
    {
        //Vector2 touchPos = _inputData.touchPosition;
        //if (touchPos != Vector2.zero)
        //{
        //    transform.Translate(touchPos.x * (Speed / 100) * Time.deltaTime, 0, 0);
        //    //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_movementClamp, _movementClamp), transform.position.y, transform.position.z);
        //}
        
        //_lastFrameFingerPositionX = Input.mousePosition.x;
        //_moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
        //float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
        //transform.Translate(swerveAmount, 0, 0);
        WalkAction?.Invoke();
    }

    private void HorizontalMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch _theTouch = Input.GetTouch(0);

            if (_theTouch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = _theTouch.deltaPosition;
                if (touchPos != Vector2.zero)
                {
                    transform.Translate(touchPos.x * (HorizontalSpeed / 100) * Time.deltaTime, 0, 0);
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, _movementClampNegative, _movementClampPositive), transform.position.y, transform.position.z);
                }
            }
        }
    }

    private void AdjustMovementClamp(float _poz, float _neg)
    {
        _movementClampPositive = _poz;
        _movementClampNegative = _neg;
    }

    private void NextLevelSettings()
    {
        transform.position = startPosition;
        Male.HumanState = HumanState.IDLE;
        Female.HumanState = HumanState.IDLE;
        RelationCount = 0;
        RelationStatus = RelationStatus.NORMAL;
        AgeStatus = AgeStatus.YOUNG;
        //for (int i = 0; i < GameManager.Instance.Females.Count; i++)
        //{
        //    if (GameManager.Instance.Females[i]!=null|| GameManager.Instance.Males[i]!=null)
        //    {
        //        GameManager.Instance.Females[i].gameObject.SetActive(false);
        //        GameManager.Instance.Males[i].gameObject.SetActive(false);
        //    }
        //}
        //GameManager.Instance.Females[0].gameObject.SetActive(true);
        //GameManager.Instance.Males[0].gameObject.SetActive(true);
        GameManager.AgeChanged?.Invoke();
        GameManager.Instance.Females[0].gameObject.transform.localPosition = new Vector3(-2, 0, 0);
        GameManager.Instance.Females[0].gameObject.transform.DORotate(new Vector3(0, 0, 0),0.5f);
        GameManager.Instance.Males[0].gameObject.transform.localPosition = new Vector3(2, 0, 0);
        Debug.Log(AgeCalculator());
        GameManager.Instance.HumansAdjust();
    }

}
