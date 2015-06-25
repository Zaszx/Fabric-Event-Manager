using System.Collections;
using System.Xml;

public class StoryEvent
{
    public int id;
    public string name;
    StoryScriptBase scriptToRun;

    public IEnumerator Run()
    {
		yield return scriptToRun.StartCoroutine(scriptToRun.Run());
		StoryEventManager.Instance.EventFinished();
    }

    public void Stop()
    {
        scriptToRun.StopAllCoroutines();
    }

    public void Read(XmlNode node)
    {
        id = int.Parse(node.Attributes["id"].Value);
        name = node.Attributes["name"].Value;
        string scriptName = node.Attributes["script"].Value;

        scriptToRun = StoryEventManager.Instance.storyScriptEntity.GetComponent(scriptName) as StoryScriptBase;
    }
}
