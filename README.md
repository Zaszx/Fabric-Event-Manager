# Fabric Event Manager

Event system we use for story scripting in our game Fabric.

Each level in the game has a state machine with states and transitions.
Every action in the game has a corresponding event. Events trigger state transitions.
Each transition will trigger a user (in our case, level designer) implemented script, derived from StoryScriptBase.

Pre-alpha release of fabric:
http://fabric.torrenglabs.com/fabric.rar
