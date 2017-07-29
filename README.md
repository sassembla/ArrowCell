# ArrowCell

change child's child's... child's component from far parent without settings.

## Motivation
control one or more items(child GameObject's components) from one parent GameObject without attaching child component to parent GameObject.

the direction of finding target component is one-way. (parent = root GameObject -> it's children -> it's children...)

this makes easy to change the child's child's .. child's specific typed component from parent.
especially in the case of the child contents are changing in game scene dynamically.

and found component will be cached with name of the GameObject and the Type of component.


## Usage
```C#
using ArrowCellCore;

// get Text component from "ItemLabel01" then change text to "changed.".
GAMEOBJECT.GetRemoteComponent<Text>("ItemLabel01", textComp => textComp.text = "changed.");
```

## Planned
* @Editor, visualiser of which type/component is connected from code.(visualise code -> ast -> connectivity by ArrowCell.)
* @Runtime, improve fail-design.


## license
see below.  
[LICENSE](./LICENSE)