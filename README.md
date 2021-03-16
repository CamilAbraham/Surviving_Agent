# Surviving Agent
Small simulation of the intent for the survival of an agent in an unknown environment

This project was my first attempt to understand artificial intelligence. The objective of this project is to put an agent in an environment of which it doesn't have any information.

The agent will start walking randomly, while searching for "resources". While walkin, his food and stamina will deplete. The "resources" the agent find will help him increase again those elements. If the agent can't find any resources and the food or stamina are depleted he will start loosing health. If all the health is depleted the agent will "die". This "resources" are placed randomly at the start of each run.

There is also a "bad" agent. This agent starts inside of a labyrinth. If he manages to get out then he will search for our main agent. This agent will "attack" the main agent, thus, reducing the main agent health.

![Starting a run](https://github.com/CamilAbraham/Surviving_Agent/blob/main/AgentSimulation3.PNG?raw=true)
This image shows the start of a run for the agent.

![After a while](https://github.com/CamilAbraham/Surviving_Agent/blob/main/AgentSimulation1.PNG?raw=true)
Here we can see how the stats of the main agent have been depleted.

![Bad agent](https://github.com/CamilAbraham/Surviving_Agent/blob/main/AgentSimulation4.PNG?raw=true)
The bad agent, before starting the runn.

![Game over](https://github.com/CamilAbraham/Surviving_Agent/blob/main/AgentSimulation2.PNG?raw=true)
When the main agent looses all his life, he "dies".
