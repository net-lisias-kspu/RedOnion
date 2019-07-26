## PartSet.1

Read-only list (or set). Enumerable (can be used in `foreach`).
Used e.g. for parts and all lists and sets you are not allowed to modify.

- `count`: Int32 - Number of elements in the list (or set).
- `[index]`: Part - Get element by index. Will throw exception if index is out of range.
- `indexOf()`: Int32, item Part
  - Get index of element. -1 if not found.
- `contains()`: Boolean, item Part
  - Test wether the list (or set) contains specified element.
