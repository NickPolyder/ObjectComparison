﻿# Roadmap

- Re Work the roadmap


## Previous Roadmap 
- Add PatchInfo as return value to include more detailed patch information.
- Consider creating diff functionality as well ? (That can commit after ? )
- Finish Depth functionality on Patch
- Add Depth functionality on Diff code
- Maybe make a Diff functionality that can apply the patch later by calling a method ? 
- Figure out how to publish alpha packages outside of nuget.org
- Make a Decorator that will provide the full comparison functionality
- Add AutoAnalyze in HasChanges
- Make Factory extensions
- Make the README better
- Make Ignore Attributes etc
	- Make analyzer settings take a Default factory.
-  Give the option to also revert a patch (is it possible?)
   - If these 2 work we can go one step further and keep a history of DiffAnalysisResults that can be applied or get reverted ?
- Make a Memento object that will keep the history of the changes ?
- Rename Skip to Ignore to follow general naming conventions.
- Include Handlers that subscribe to the Current if it inherits from `INotifyPropertyChanged`
- Make the Comparison Tracker to have an index that will return the change tracking of that field like (ComparisonTracker["path.to.property"]) 
- Make the comparison tracker to make use of the WeakReference class.
- Make examples and samples 
- Unit Tests