using UnityEngine;
using System.Collections;

public class ExampleScript2 : StoryScriptBase 
{
    public override IEnumerator Run()
    {
        SubtitleManager.Instance.currentText = "TEST";
        yield return new WaitForSeconds(3.0f);
        SubtitleManager.Instance.currentText = "ANOTHER TEST";
        yield return new WaitForSeconds(3.0f);
        SubtitleManager.Instance.currentText = "";
        yield return new WaitForSeconds(1.0f);
    }

}
