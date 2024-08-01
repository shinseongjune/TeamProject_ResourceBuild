using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class AnimationHandler
{
    Animator anim;
    Dictionary<string, int> animIDs = new Dictionary<string, int>();
    float currentProgress;
    float lastStopTime;
    public bool isAnimationStopped;

    public AnimationHandler(Animator anim)
    {
        this.anim = anim;
    }

    public void SetAnimID(string id)
    {
        if (!animIDs.ContainsKey(id))
        {
            animIDs.Add(id, Animator.StringToHash(id));
        }
    }

    public void SetFloat(string name, float value)
    {
        if (animIDs.ContainsKey(name))
        {
            anim.SetFloat(animIDs[name], value);
        }
        else
        {
            animIDs.Add(name, Animator.StringToHash(name));
            anim.SetFloat(animIDs[name], value);
        }

        ResetSpeedOnTransition();
    }

    public void SetBool(string name, bool value)
    {
        if (animIDs.ContainsKey(name))
        {
            anim.SetBool(animIDs[name], value);
        }
        else
        {
            animIDs.Add(name, Animator.StringToHash(name));
            anim.SetBool(animIDs[name], value);
        }

        ResetSpeedOnTransition();
    }

    public void SetInteger(string name, int value)
    {
        if (animIDs.ContainsKey(name))
        {
            anim.SetInteger(animIDs[name], value);
        }
        else
        {
            animIDs.Add(name, Animator.StringToHash(name));
            anim.SetInteger(animIDs[name], value);
        }

        ResetSpeedOnTransition();
    }

    public void SetTrigger(string name)
    {
        if (animIDs.ContainsKey(name))
        {
            anim.SetTrigger(animIDs[name]);
        }
        else
        {
            animIDs.Add(name, Animator.StringToHash(name));
            anim.SetTrigger(animIDs[name]);
        }

        ResetSpeedOnTransition();
    }

    public void StopMotion()
    {
        if (isAnimationStopped) return;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        currentProgress = stateInfo.normalizedTime % 1.0f;
        lastStopTime = Time.time;

        anim.speed = 0;
        isAnimationStopped = true;
    }

    public void ResumeMotion()
    {
        if (lastStopTime == 0) return;

        float elapsedTime = Time.time - lastStopTime;

        anim.Play(anim.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, currentProgress + elapsedTime);
        anim.speed = 1;

        isAnimationStopped = false;
    }

    void ResetSpeedOnTransition()
    {
        if (anim.IsInTransition(-1))
        {
            anim.speed = 1;
            isAnimationStopped = false;
        }
    }
}
