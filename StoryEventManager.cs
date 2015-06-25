using System.Diagnostics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

// Each level has a state machine with states and transitions.
// Every action in the game has a corresponding event. Events trigger state transitions.
// Each transition will trigger a user (in our case, level designer) implemented script, derived from StoryScriptBase.

// Summary: When an event is fired, if the current state uses that event to transition to another state, it does; and runs a script.

public class StoryEventManager : Singleton<StoryEventManager> 
{
    StoryState currentState;
    StoryEvent currentEvent;

    public List<StoryEvent> events = new List<StoryEvent>();
    public List<StoryState> states = new List<StoryState>();

    readonly Queue<int> eventIdsToBeProcessed = new Queue<int>();

    public GameObject storyScriptEntity;

    public override void Init()
    {
        base.Init();
        storyScriptEntity = GameObject.Find("StoryScripts");
        DontDestroyOnLoad(storyScriptEntity);
        ReadEvents(Application.dataPath + "/Resources/events.xml");
    }

    public void LoadLevel(string levelStateFilePath)
    {
        if(currentEvent != null)
        {
            currentEvent.Stop();
            EventFinished();
        }
        eventIdsToBeProcessed.Clear();
        currentState = null;

        if(File.Exists(levelStateFilePath))
        {
            XmlDocument document = new XmlDocument();
            document.Load(levelStateFilePath);
            XmlNode statesNode = document.FirstChild;
            XmlNode stateNode = statesNode.FirstChild;
            while(stateNode != null)
            {
                StoryState newState = new StoryState();
                newState.Read(stateNode);
                states.Add(newState);
                stateNode = stateNode.NextSibling;
            }
        }
        if(states.Count > 0)
        {
            currentState = states[0];
        }
    }

    public void ReadEvents(string eventFilePath)
    {
        XmlDocument document = new XmlDocument();
        document.Load(eventFilePath);
        XmlNode eventsNode = document.FirstChild;
        XmlNode eventNode = eventsNode.FirstChild;
        while(eventNode != null)
        {
            StoryEvent newEvent = new StoryEvent();
            newEvent.Read(eventNode);
            events.Add(newEvent);
            eventNode = eventNode.NextSibling;
        }
    }

	void Update ()
	{
	    if(currentEvent == null && currentState != null)
        {
            while(eventIdsToBeProcessed.Count != 0)
            {
                int nextEventId = eventIdsToBeProcessed.Dequeue();
                if(currentState.HasTransition(nextEventId))
                {
                    currentEvent = GetEventWithId(nextEventId);
                    StartCoroutine(currentEvent.Run());
                    break;
                }
            }
        }
	}

    public void EventFinished()
    {
        int nextStateId = currentState.GetNextStateId(currentEvent.id);
        currentState = GetStateWithId(nextStateId);
        currentEvent = null;
    }

    StoryEvent GetEventWithId(int id)
    {
        StoryEvent result = events.Find(e => e.id == id);
        return result;
    }

    StoryState GetStateWithId(int id)
    {
        StoryState result = states.Find(s => s.id == id);
        return result;
    }

    public void DispatchEvent(int eventId)
    {
        eventIdsToBeProcessed.Enqueue(eventId);
    }

    public void Kill()
    {
        Destroy(storyScriptEntity);
    }
}
