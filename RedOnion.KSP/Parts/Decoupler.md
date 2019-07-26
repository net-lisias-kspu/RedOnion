## Decoupler

Decoupler, separator, launch clamp or docking port.

- `ship`: [Ship](../API/Ship.md) - Ship (vehicle/vessel) this part belongs to.
- `native`: Part - Native `Part` - KSP API.
- `parent`: [Part](PartBase.md) - Parent part (this part is attached to).
- `decoupler`: Decoupler - Decoupler that will decouple this part when staged.
- `stage`: Int32 - Stage number as provided by KSP API. (`Native.inverseStage`)
- `decoupledin`: Int32 - Stage number where this part will be decoupled or -1. (`Decoupler?.Stage ?? -1`)
- `resources`: [ResourceList](ResourceList.md) - Resources contained within this part.
- `state`: PartStates - State of the part (IDLE, ACTIVE (e.g. engine), DEACTIVATED, DEAD, FAILED).
- `istype()`: Boolean, name String
  - Accepts `decoupler` and `separator`. (Case insensitive)
