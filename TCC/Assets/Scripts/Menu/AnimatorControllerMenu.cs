using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorControllerMenu : MonoBehaviour
{
    public Animator animator_Menu;
    public Rigidbody rigidbody;
    float time_WaitPlay;
    float time;
    bool jump = false;

    private void Update()
    {
        ControllerAnimator();
        if (jump)
        {
            time_WaitPlay += 1 * Time.deltaTime;
            if (time_WaitPlay > 4)
            {
                LoadScene_Menu(5);
            }
        }
    }

    void ControllerAnimator()
    {
        time += 1 * Time.deltaTime;
        animator_Menu.SetFloat("State", time);
        if (time >= 19)
        {
            animator_Menu.SetBool("State", true);
            time = 0;
        }
    }

    public void Play_Click()
    {
        animator_Menu.SetBool("jump", true);
        jump = true;
    }

    public void LoadScene_Menu(int index)
    {

        SceneManager.LoadScene(index);
    }

    public void Jump_BetoMenu()
    {
        rigidbody.AddForce(new Vector3(-200, 500, 0) * Time.deltaTime, ForceMode.Impulse );
        Debug.Log("Pulei");
    }
}
