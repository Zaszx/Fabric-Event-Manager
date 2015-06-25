using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class StoryState
{
    public int id;
    public string name;
    public List<KeyValuePair<int, int>> transitions = new List<KeyValuePair<int, int>>();

    public bool HasTransition(int eventId)
    {
        foreach(KeyValuePair<int,int> transition in transitions)
        {
            if(transition.Key == eventId)
            {
                return true;
            }
        }
        return false;
    }

    public int GetNextStateId(int eventId)
    {
        foreach (KeyValuePair<int, int> transition in transitions)
        {
            if (transition.Key == eventId)
            {
                return transition.Value;
            }
        }
        return -1;
    }

    public void Read(XmlNode node)
    {
        id = int.Parse(node.Attributes["id"].Value);
        name = node.Attributes["name"].Value;
        XmlNode transitionsNode = node.FirstChild;
        if(transitionsNode != null)
        {
            XmlNode transitionNode = transitionsNode.FirstChild;
            while (transitionNode != null)
            {
                KeyValuePair<int, int> newTransition = new KeyValuePair<int, int>
                (
                    int.Parse(transitionNode.Attributes["with"].Value),
                    int.Parse(transitionNode.Attributes["to"].Value)
                );
                transitions.Add(newTransition);
                transitionNode = transitionNode.NextSibling;
            }
        }
    }
}
