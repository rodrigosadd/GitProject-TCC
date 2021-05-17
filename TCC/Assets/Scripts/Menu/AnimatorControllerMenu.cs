using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorControllerMenu : MonoBehaviour
{   
    public Material head;
    public Material body;
    public Animator anim;
    public Rigidbody rbody;
    public Transform targetDirection;
    public float forceJump;
    public float waitTimeToLoadLevel;
    public float speedHeadCutoffHeight;
    public float speedBodyCutoffHeight;
    private Vector3 _direction;
    private float _headCutoffHeight;
    private float _bodyCutoffHeight;
    private bool _canActivateShaderHeadBody;

    void Start()
    {
        _headCutoffHeight = 5f;
        _bodyCutoffHeight = 5f;
        head.SetFloat("_Cutoff_Height", 5f);
        body.SetFloat("_Cutoff_Height", 5f);
        StartCoroutine("TimeToStartLeavingPortal");
    }

    void Update()
    {
        ActivateShaderHeadBody();
    }

    IEnumerator TimeToStartLeavingPortal()
    {
        yield return new WaitForSeconds(1f);
        
        _canActivateShaderHeadBody = true;
        anim.SetBool("Leaving Portal", true);
        anim.SetBool("Lost", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Falling Idle", false);
    }

    public void ActivateShaderHeadBody()
    {   
        if(_canActivateShaderHeadBody)
        {
            _headCutoffHeight -= Time.deltaTime * speedHeadCutoffHeight;
            _headCutoffHeight = Mathf.Clamp(_headCutoffHeight, -1f, 5f);
            head.SetFloat("_Cutoff_Height", _headCutoffHeight);

            _bodyCutoffHeight -= Time.deltaTime * speedBodyCutoffHeight;
            _bodyCutoffHeight = Mathf.Clamp(_bodyCutoffHeight, -1f, 5f);
            body.SetFloat("_Cutoff_Height", _bodyCutoffHeight);
        }
    }

    public void SetLost()
    {
        anim.SetBool("Leaving Portal", false);
        anim.SetBool("Lost", true);
        anim.SetBool("Idle", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Falling Idle", false);
    }

    public void SetIdle()
    {
        anim.SetBool("Leaving Portal", false);
        anim.SetBool("Lost", false);
        anim.SetBool("Idle", true);
        anim.SetBool("Crouch", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Falling Idle", false);
    }

    public void SetCrouch()
    {
        anim.SetBool("Leaving Portal", false);
        anim.SetBool("Lost", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Crouch", true);
        anim.SetBool("Jump", false);  
        anim.SetBool("Falling Idle", false);              
    }

    public void SetJump()
    {
        anim.SetBool("Leaving Portal", false);
        anim.SetBool("Lost", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Jump", true);
        anim.SetBool("Falling Idle", false);
        Jump();
    }

    public void SetFallingIdle()
    {
        anim.SetBool("Leaving Portal", false);
        anim.SetBool("Lost", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Falling Idle", true);
    }

    public void Jump()
    {
        _direction = (targetDirection.position - transform.position).normalized;
        rbody.AddForce(_direction * forceJump, ForceMode.Impulse);        
    }

    public void SetLoadLevelAfterJump(int indexLevel)
    {
        StartCoroutine(TimeToLoadLevel(indexLevel));
    }

    IEnumerator TimeToLoadLevel(int indexLevel)
    {
        SetCrouch();

        yield return new WaitForSeconds(waitTimeToLoadLevel);

        LevelLoader.instance.LoadNextLevel(indexLevel);
    }
}
