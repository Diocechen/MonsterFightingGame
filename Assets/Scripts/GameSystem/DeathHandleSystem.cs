using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandleSystem : MonoBehaviour
{
    public event System.EventHandler<SkillPointArgs> DeathHappen;

    public void AnEmemyDied(float addSkillPoint)
    {
        //很重要 不然會有null reference
        DeathHappen?.Invoke(this, new SkillPointArgs(addSkillPoint));
    }
}

public class SkillPointArgs : System.EventArgs
{
    public float addSkillPoint;
    public SkillPointArgs(float addSkillPoint)
    {
        this.addSkillPoint = addSkillPoint;
    }
}
