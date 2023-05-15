using UnityEngine;

/// <summary>
/// XONG CHANGEPANT
/// </summary>
public class Player : Characters
{
    [SerializeField] private FloatingJoystick joystick;

    [SerializeField] private CharacterController characterController;

    public StateMachine<Player> currentState;

    [SerializeField] private Transform rightHand;

    private int weaponID;

    private int skinHairID;

    private int skinShieldID;

    private int skinPantID;

    [SerializeField] private SkinnedMeshRenderer pants;

    private int coins;
    public int Coins { get => coins; set => coins = value; }

    private float gravity;

    private float horizontal;
    private float vertical;

    private void Start()
    {
        this.OnInit();   
    }

    public override void OnInit()
    {
        base.OnInit();

        weaponID = 0;
        ChangeWeapon(weaponID);
        currentState = new StateMachine<Player>();
        currentState.SetOwner(this);
        this.gravity = 20f;
        currentState.ChangeState(new IdleState());
    }

    //====Update======

    protected override void CharactersUpdate()
    {
        currentState.UpdateState(this);

        SetGravity();

        base.CharactersUpdate();

        IsMove();
    }

    //====Setup Joystick =======

    public void SetupJoystick(FloatingJoystick joystick)
    {
        this.joystick = joystick;
    }


    //=====SetGravity======
    private void SetGravity()
    {
        characterController.Move(Vector3.down * gravity * Time.deltaTime);
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
            characterController.Move(direction * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void SetSkinPantID(int id)
    {
        this.skinPantID = id;
    }

    public void SetSkinHairID(int id)
    {
        this.skinHairID = id;
    }

    public void SetSkinShieldID(int id)
    {
        this.skinShieldID = id;
    }




    public void ChangeWeapon(int idx)
    {
        ClearPastWeapon();

        weaponID= idx;

        WeaponSO weapon = WeaponSpawner.Instance.GetWeaponSOByID(idx);

        Vector3 localPosition = weapon.weaponModel.transform.localPosition;

        Quaternion localRot = weapon.weaponModel.transform.localRotation;

        var weaponClone = Instantiate(weapon.weaponModel);
        weaponClone.transform.SetParent(rightHand);

        weaponClone.transform.localPosition = localPosition;

        weaponClone.transform.localRotation = localRot;


        
    }
    private void ClearPastWeapon()
    {
        if (!rightHand || rightHand.childCount <= 0) return;

        for (int i = 0; i < rightHand.childCount; i++)
        {
            var child = rightHand.GetChild(i);

            if (child) Destroy(child.gameObject);
        }
    }

    public void ChangePant()
    {
        ChangeSkinPlayer.Ins.ChangePant(pants, skinPantID);
    }


}
