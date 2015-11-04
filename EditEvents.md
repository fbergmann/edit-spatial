## Event Support
Discrete events are helpful to modify the behavior of the simulation. We added only limited support for time based events, that is events that hit after a specific time has been reached. To create events, you place a file called `eventinfo.ini`. Here an example INI file: 


	skipOutputUntilEvent = true
	
	[event0] 
	start = 10                 # start time of the event, the first accepted time point after this the event will be applied
	target = 0                 # index of the variable to assign to 
	file = event.dmp           # the dmp file with the information
	uniform = 0                # alternatively, the value to be assigned to it uniformly

You can add as many sections in that file as you need. Each section should contain the following entries: 

* `start`: this has to be a numeric value indicating the start time, when this event ought to hit. Note that no interpolation is performed here. As soon as the start time is hit (or passed) the event will be applied. 
* `target`: this is a zero based index of the model variable to be changed. To find the index for the variable to be changed you could have a look at the `host.config` file, there in the `[Data]` section all variables are listed, in the same order. (A future version will allow to use the variable names directly)
* `file`: this is a `.dmp` file containing the concentrations to be set onto the variable. In case a uniform value should be applied, this entry can be left empty. 
* `uniform`: if no `file` is specified, this value will be taken when the event is applied, and the whole field is uniformly filled. 

There is also a boolean entry `skipOutputUntilEvent` in the general section outside the file. If this value is set to `true`, output will be suppressed until the first event is applied.

---
  
