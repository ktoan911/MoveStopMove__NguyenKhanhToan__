using UnityEngine;
using UnityEngine.Events;

public class Player : Characters
{
    private int coins;

    [SerializeField] private Transform leftHand;

    [SerializeField] private FloatingJoystick joystick;

    [SerializeField] private CharacterController characterController;

    public int skinShieldID;

    public CircleAroundPlayer circleRange;

    public UnityAction<Characters, int> DeadUI;

    public UnityAction<int> WinUI;

    public StateMachine<Player> currentState;

    public int coinUp;
    public int Coins { get => coins; set => coins = value; }

    private float horizontal;
    private float vertical;

    private void Awake()
    {
        this.OnInit();
    }
    private void Start()
    {
        currentState = new StateMachine<Player>();
        currentState.SetOwner(this);

        currentState.ChangeState(new SleepState());
    }

    public override void OnInit()
    {
        base.OnInit();

        this.coinUp = 0;

        this.ChangeNamePlayer(Pref.NamePlayer);
    }

    public void SetInitalEquip()
    {
        this.ChangeWeapon(Pref.CurWeaponId);

        this.ChangePant(Pref.CurPantId);

        this.ChangeShield(Pref.CurShieldId);  

        this.ChangeHair(Pref.CurHairId);
    }

    //====Update======

    protected override void CharactersUpdate()
    {
        currentState.UpdateState(this);

        //SetGravity();

        base.CharactersUpdate();

        IsMove();
    }

    //====Setup Joystick =======

    public void SetupJoystick(FloatingJoystick joystick)
    {
        this.joystick = joystick;
    }

    //===PlayerMovement=====

    private void GetInput()
    {
        if (joystick == null) return;
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
    }
    public bool IsMove()
    {
        GetInput();

        if (Mathf.Abs(this.horizontal) > 0.1f || Mathf.Abs(this.vertical) > 0.1f)
        {
            this.IsMoving= true;

            return true;
        }
        return false;
    }
    public void Moving()
    {
        GetInput();

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            characterController.Move(direction * Speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public override void UpdateLevel(bool isUp)
    {
        base.UpdateLevel(isUp);

        ChangepropertiesCharacter.Ins.ChangePlayerAttackRange(this.currentScale, this);
    }

    public void UpdateCoin(int coinChange, bool isUp)
    {
        if (isUp) this.Coins += coinChange;
        else this.Coins -= coinChange;
        Pref.Coins = this.Coins;
    }

    public void ChangeWeapon(int id)
    {
        this.weaponID= id;

        WeaponSpawner.Instance.ChangeModelWeaponPlayer(this,rightHand, id);
    }

    public void ChangePant(int id)
    {
        this.skinPantID = id;

        ChangeSkin.Ins.ChangePant(this, pants, skinPantID);
    }

    public void ChangeHair(int id)
    {

        this.skinHairID = id;

        ChangeSkin.Ins.ChangeModelHair(hair, skinHairID);
    }

    public void ChangeShield(int id)
    {
        this.skinShieldID = id;

        ChangeSkin.Ins.ChangeModelShield(leftHand, skinShieldID);
    }

    public void ChangeNamePlayer(string name)
    {
        this.characterName = name;
    }

    public override void UpCoin(Characters character)
    {
        base.UpCoin(character);

        if (character == null) return;

        this.coinUp += character.level;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        this.currentState.ChangeState(new DeadState());
    }

}
