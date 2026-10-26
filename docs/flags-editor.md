# Editing Flags and Native Masks

**Document ID:** GLOBAL-FLAGS-EDITOR-GUIDE

**Schema version:** 1

**Kind:** guide

**Lifecycle:** current

**Subject:** enum flags and runtime-named integral masks in the four views

**Surface:** guide

**Boundary:** global

**Authority role:** non-normative

**Mutation:** authored

**Indexing:** include

## Enum discovery

An enum marked with `[Flags]` automatically uses a checkbox popup in ImGui,
Avalonia, WinForms and WPF. Ordinary enums retain the single-choice editor.
`[InspectorEditor(EditorKind.Choice)]` or a manual option can override discovery.
For example:

```csharp
[Flags]
public enum Features : uint
{
    None = 0,
    Render = 1,
    Simulate = 2,
    Both = Render | Simulate,
}
```

The popup lists named constants, including aliases and composites. A composite
is indeterminate when partly enabled, as is a bit that differs across selected
objects. Checking it sets its bits; clearing it clears its bits. Other bits in
each object survive. Selecting `None` is an explicit full reset, including
unknown bits. Unchecking `None` changes nothing. Null is indeterminate and
becomes a numeric value only on an explicit edit.

## Runtime names from a native host

Native descriptors can use the same manual field API as other native values:

```csharp
var inspector = Inspector.Create<NativeHandle>(TypeBinderMode.Manual);
var layers = inspector.AddField<NativeHandle, uint>(
    "layers", handle => handle.ReadLayers(),
    (handle, value) => handle.WriteLayers(value));
layers.FlagChoices = () => ReadLayerNames().Take(32)
    .Select((name, bit) => new FlagChoice(1UL << bit, name));
inspector.Bind(handle);
```

`FlagChoice` lives in `PixieInspector.Options`. A non-null `FlagChoices`
provider selects `EditorKind.Flags`. Labels are read again whenever a popup
opens and stay stable during that interaction. Assign a new provider or let
the existing one read the host's current names. The host chooses which bits to
offer; omitted bits are retained when another bit changes. Use masks in the
field's storage width: for a signed 8-bit `All = -1`, that is `0xFF`, not the
64-bit `ulong.MaxValue`.

The generic inspector owns bit editing, multi-selection, pending values,
read-only behavior and diagnostics. The host owns native reads/writes, history,
units, physical calculations and scene synchronization. Calculated physics
values can continue to use read-only `AddField` nodes; share a native snapshot
when several fields come from one expensive native query. This feature neither
changes the Engine's C API nor upgrades its package references.

## Failure and validation boundaries

Only enum and 8/16/32/64-bit integral storage, including nullable forms, is
supported. Floating-point values, native-sized integers and display units on
numeric masks are rejected. Provider failures, null entries/labels and masks
outside the storage width produce a diagnostic and an empty list. Reopening
retries the provider; its recovery does not hide an existing value-read or write
failure. A failed getter cannot be treated as zero and overwritten.

Each edit uses the normal `SetValues` path. With manual binder control, edits
compose in pending values until `Apply()`. Normal value rules can reject or
transform them. Preparation precedes all writes; setter execution retains the
existing non-transactional semantics. Applications needing transactional native
writes implement that in their adapter.

No new dependency or assembly is needed. Package publication and downstream
consumer upgrades remain explicit release actions. Core contract details are
in the [technical reference](modules/core/REFERENCE.md#flags).
