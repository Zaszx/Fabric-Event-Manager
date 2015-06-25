using UnityEngine;
using System.Collections;

public class StoryScriptBase : MonoBehaviour
{
    public virtual IEnumerator Run()
    {
        //Intentionally left blank
        yield return null;
    }
}
