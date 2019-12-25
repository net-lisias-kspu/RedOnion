## Ship

Active vessel


**Instance Properties:**
- `native`: Vessel - \[`Unsafe`\] Native `Vessel` for unrestricted access to KSP API. Same as `FlightGlobals.ActiveVessel` if accessed through global `ship`.
- `name`: string - Name of the ship (vehicle/vessel).
- `target`: Object - \[`WIP`\] Target of active ship. Null if none.
- `autopilot`: [Autopilot](Autopilot.md) - Autopilot of this ship (vehicle/vessel).
- `throttle`: float - Current throttle (assign redirects to `Autopilot`, reads control state if autopilot disabled)
- `parts`: [ShipPartSet](../Parts/ShipPartSet.md) - All parts of this ship/vessel/vehicle.
- `root`: [Part](../Parts/PartBase.md) - Root part (same as `parts.root`).
- `nextDecoupler`: [Decoupler](../Parts/Decoupler.md) - One of the decouplers that will get activated by nearest stage. (Same as `Parts.NextDecoupler`.)
- `nextDecouplerStage`: int - Stage number of the nearest decoupler or -1. (Same as `Parts.NextDecouplerStage`.)
- `decouplers`: [ReadOnlyList](ReadOnlyList.1.md)\[[Decoupler](../Parts/Decoupler.md)\] - List of all decouplers, separators, launch clamps and docks with staging. (Docking ports without staging enabled not included.)
- `dockingports`: [ReadOnlyList](ReadOnlyList.1.md)\[[DockingPort](../Parts/DockingPort.md)\] - List of all docking ports (regardless of staging).
- `engines`: [EngineSet](../Parts/EngineSet.md) - All engines (regardless of state).
- `sensors`: [ReadOnlyList](ReadOnlyList.1.md)\[[Sensor](../Parts/Sensor.md)\] - All sensors.
- `id`: Guid - Unique identifier of the ship (vehicle/vessel). Can change when docking/undocking.
- `persistentId`: uint - Unique identifier of the ship (vehicle/vessel). Should be same as it was before docking (after undocking).
- `vesseltype`: VesselType - KSP API. Vessel type as selected by user (or automatically).
- `mass`: float - Total mass of the ship (vehicle/vessel). [tons = 1000 kg]
- `packed`: bool - Wheter the ship is still packed (reduced physics).
- `landed`: bool - Wheter the ship is landed (on the ground or on/in water).
- `splashed`: bool - Wheter the ship is in water.
- `longitude`: double - Longitude of current position in degrees.
- `latitude`: double - Latitude of current position in degrees.
- `altitude`: double - Altitude of current position (above sea level) in meters.
- `radarAltitude`: double - True height above ground in meters.
- `dynamicPressure`: double - Dynamic pressure [atm = 101.325kPa]
- `q`: double - Dynamic pressure [atm = 101.325kPa]
- `body`: [SpaceBody](SpaceBody.md) - KSP API. Orbited body.
- `orbit`: Orbit - \[`Unsafe`\] \[`WIP`\] KSP API. Orbit parameters. May get replaced by safe wrapper in the future.
- `eccentricity`: double - Eccentricity of current orbit.
- `semiMajorAxis`: double - Semi-major axis of current orbit.
- `semiMinorAxis`: double - Semi-minor axis of current orbit.
- `apoapsis`: double - Height above ground of highest point of current orbit).
- `periapsis`: double - Height above ground of lowest point of current orbit).
- `apocenter`: double - Highest distance between center of orbited body and any point of current orbit.
- `pericenter`: double - Lowest distance between center of orbited body and any point of current orbit.
- `timeToAp`: double - Eta to apoapsis in seconds.
- `timeToPe`: double - Eta to periapsis in seconds.
- `period`: double - Period of current orbit in seconds.
- `trueAnomaly`: double - Angle in degrees between the direction of periapsis and the current position.
- `meanAnomaly`: double - Angle in degrees between the direction of periapsis and the current position extrapolated on circular orbit.
- `position`: [Vector](Vector.md) - Center of mass relative to (CoM of) active ship (zero for active ship).
- `velocity`: [Vector](Vector.md) - Current orbital velocity.
- `surfaceVelocity`: [Vector](Vector.md) - Current surface velocity.
- `srfVelocity`: [Vector](Vector.md) - Current surface velocity (Alias to `surfaceVelocity`).
- `srfvel`: [Vector](Vector.md) - Current surface velocity (Alias to `surfaceVelocity`).
- `forward`: [Vector](Vector.md) - Vector pointing forward (from cockpit - in the direction of the 'nose').
- `back`: [Vector](Vector.md) - Vector pointing backward (from cockpit - in the direction of the 'tail').
- `up`: [Vector](Vector.md) - Vector pointing up (from cockpit).
- `down`: [Vector](Vector.md) - Vector pointing down (from cockpit).
- `left`: [Vector](Vector.md) - Vector pointing left (from cockpit).
- `right`: [Vector](Vector.md) - Vector pointing right (from cockpit).
- `north`: [Vector](Vector.md) - Vector pointing north in the plane that is tangent to sphere centered in orbited body.
- `east`: [Vector](Vector.md) - Vector pointing east (tangent to sphere centered in orbited body).
- `away`: [Vector](Vector.md) - Vector pointing away from orbited body (aka *up*, but we use `up` for cockpit-up).
- `pitch`: double - Current pitch / elevation (the angle between forward vector and tangent plane) [-90..+90]
- `heading`: double - Current heading / yaw (the angle between forward and north vectors in tangent plane) [0..360]. Note that it can change violently around the poles.
- `roll`: double - Current roll / bank (the angle between up and away vectors in the plane perpendicular to forward vector) [-180..+180]. 
Note that it can change violently when facing up or down.
- `angularVelocity`: [Vector](Vector.md) - Angular velocity (ω, deg/s), how fast the ship rotates
- `angularMomentum`: [Vector](Vector.md) - Angular momentum (L = Iω, kg⋅m²⋅deg/s=N⋅m⋅s⋅deg) aka moment of momentum or rotational momentum.
- `momentOfInertia`: Vector3d - Moment of inertia (I, kg⋅m²=N⋅m⋅s²) aka angular mass or rotational inertia.
- `maxTorque`: [Vector](Vector.md) - Maximal ship torque [N⋅m⋅deg=deg⋅kg⋅m²/s²] (aka moment of force or turning effect, maximum of positive and negative).
- `maxVacuumTorque`: [Vector](Vector.md) - Maximal ship torque in vacuum [N⋅m⋅deg=deg⋅kg⋅m²/s²] (ignoring control surfaces).
- `maxAngular`: [Vector](Vector.md) - Maximal angular acceleration (deg/s²)
- `maxVacuumAngular`: [Vector](Vector.md) - Maximal angular acceleration in vacuum (ignoring control surfaces).

**Instance Methods:**
- `positionAt()`: [Vector](Vector.md), time double
  - Predicted position at specified time.
- `velocityAt()`: [Vector](Vector.md), time double
  - Predicted velocity at specified time.
- `local()`: [Vector](Vector.md), v [Vector](Vector.md)
  - Translate vector/direction into ship-local coordinates (like looking at it from the cockpit).
- `world()`: [Vector](Vector.md), v [Vector](Vector.md)
  - Translate vector/direction into world coordinates (reverse the `local` transformation).
