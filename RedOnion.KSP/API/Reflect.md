## Reflect

Reflects/imports native types provided as namespace-qualified string
(e.g. "System.Collections.Hashtable") or assembly-qualified name
("UnityEngine.Vector2,UnityEngine"). It will first try `Type.GetType`,
then search in `Assembly-CSharp`, then in `UnityEngine` and then in all assemblies
returned by `AppDomain.CurrentDomain.GetAssemblies()`.

- `new()`: Constructor - Construct new object given type or object and arguments. Example: `reflect.new("System.Collections.ArrayList")`.
- `create()`: Constructor - Alias to new().
- `construct()`: Constructor - Alias to new().
