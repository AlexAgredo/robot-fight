using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    None, 
    Punch1,
    Punch2,
    Punch3
}
public class PlayerAttack : MonoBehaviour
{
    private CharacterAnimation myAnim;

    private bool activateTimeToReset;
    private float defaultComboTimer = 0.5f;
    private float currentComboTimer;
    private ComboState currentComboState;

    private void Awake()
    {
        myAnim = GetComponent<CharacterAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.None;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttack();
        ResetComboState();


    }

    void ComboAttack()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(currentComboState == ComboState.Punch3)
            {
                return;
            }

            currentComboState++;
            activateTimeToReset = true;
            currentComboTimer = defaultComboTimer;

            if(currentComboState == ComboState.Punch1)
            {
                myAnim.punchR();
            }
            if (currentComboState == ComboState.Punch2)
            {
                myAnim.punchL();
            }
            if (currentComboState == ComboState.Punch3)
            {
                myAnim.punchR();
            }

        }

        /*if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentComboState == ComboState.Punch3)
            {
                return;
            }

            if (currentComboState == ComboState.None
                || currentComboState == ComboState.Punch1
                || currentComboState == ComboState.Punch2){
                currentComboState = ComboState.Punch3;
            }
            activateTimeToReset = true;
            currentComboTimer = defaultComboTimer;
            if(currentComboState == ComboState.Punch3)
            {
                myAnim.Punch3();
            }

        }*/
    }

    void ResetComboState()
    {
        if (activateTimeToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.None;
                activateTimeToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }
}
