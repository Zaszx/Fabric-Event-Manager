using UnityEngine;
using System.Collections;

public class ExampleScript1 : StoryScriptBase
{
    public override IEnumerator Run()
    {
        SubtitleManager.Instance.currentText = "Oh, in case you got covered in that Repulsion Gel\nhere's some advice the lab boys gave me:";
        yield return new WaitForSeconds(3.0f);
        SubtitleManager.Instance.currentText = "\n\"DO NOT get covered in the Repulsion Gel.\"";
        yield return new WaitForSeconds(3.0f);
        SubtitleManager.Instance.currentText = "";
    }

}