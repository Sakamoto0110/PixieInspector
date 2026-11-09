# Edit Chains and Manual UI Locks

**Document ID:** GLOBAL-EDIT-CHAINS-GUIDE

**Schema version:** 1

**Kind:** guide

**Lifecycle:** current

**Subject:** host-configured numeric UI chains, manual locks and grouped interactions

**Surface:** guide

**Boundary:** global

**Authority role:** non-normative

**Mutation:** authored

**Indexing:** include

## Host configuration

The core remains independent of UI. A host declares semantically compatible
numeric leaves explicitly; no naming convention links fields automatically.

```csharp
var width = inspector["Width"];
var height = inspector["Height"];
width.ChainsWith = [height]; // The reverse edge is normalized automatically.
```

`ChainsWith` is static eligibility, not an active chain. `EnableChain()` arms a
singleton on the first field. Clicking an eligible field joins the active groups
of its eligible neighbors, once per member. Static cycles are legal: propagation
uses a flat canonical group, not recursive setters. `ChainedWith` is read-only
and includes the field itself, so Count == 1 identifies an armed singleton.
`ClearChain()` on any member clears the entire runtime group, retaining eligibility.
Replacing static edges clears affected runtime groups. These operations change
options only; they do not write values or issue value-change notifications.

Edges may cross Inspector instances. This is how a host can link full dimensions
and a collider whose native half dimensions have a display-unit factor of two.
The core does not own native component names or relationships. Unit compatibility
is a host responsibility; all absolute values and deltas below use displayed units.

## UI edits and locks

`IsEditLocked` blocks direct user input, including label scrubbing. It is separate
from `ReadOnly`: an already linked locked field can still receive propagated
edits. Real read-only/compromised guards, converters, ranges, value rules and
binding diagnostics always remain in force. Existing programmatic `SetValue`,
`SetValues` and `SetFlag` neither acquire the UI lock nor start propagation.

Use `EditValue(value)` for a user-originated absolute edit. It uses the same
absolute displayed value for each active member and each member's bound objects.
An armed singleton changes only itself. The source is visited first, and every
other group member is visited once through its normal setter.

Use a gesture for continuous input:

```csharp
using var edit = width.BeginEdit();
InspectorEditResult moved = edit.SetDelta(totalDeltaFromDragStart);
// On Esc, if the host has not replaced the selection or World:
InspectorEditResult restored = edit.Cancel();
```

Each member and each bound object's displayed value is captured at the start.
Repeated SetDelta calls use that snapshot, never the last clamped result. Integral
and decimal starts use decimal arithmetic, avoiding conversion of large integers
through double. Null/unreadable values are not silently interpreted as zero.
Each target's ordinary conversion and range applies independently. SetValue on
the gesture is absolute; Dispose completes without restoring values.

`InspectorEditResult.Outcomes` contains the member, Pending, Failure and Exception
for each attempt. `Accepted` means no captured exception/binding failure;
`Pending` still does not mean native storage changed. Diagnostics are captured
before later reads can recover the node. Setters remain non-transactional: a
failure can leave earlier objects or members changed. `MayHavePartialEffects`
is conservative, not proof that a partial write happened. Do not infer rollback,
physical validity or a transaction from an accepted request.

Cancel uses normal setters and reports its own result. It restores the initial
pending/no-pending state for manual binding when restoration succeeds. A real
read-only guard or native refusal can prevent restoration; the host must not
discard history or clear dirty state merely because Cancel was requested.

## Lifetime

Bind-set changes reset runtime chains and manual locks, retaining static edges.
Collection-item resets also invalidate their edit state. Disposal removes reverse
edges and the runtime group, avoiding a global registry retaining Inspectors.
Gestures reject stale bind/topology snapshots before writes and recheck between
members, including when a callback rebinds a later target. A host must end an
interaction before replacing native state, then rebuild/dispose its nodes.

## ImGui integration

The label slots are lock, chain (or empty), then field name. Chain has unsupported,
supported-off and supported-on states; the tooltip distinguishes the armed
singleton. Left click enables a supported field; right click on an active member
clears the entire chain. Label controls do not overlap the scrub target.

`InteractionStarted` and `InteractionFinished` on ImGuiInspectorView bracket
scalar UI edits and scrubs. Continuous writes share the open gesture until input
is released or a different source starts. Finished arguments provide Gesture,
Cancelled, RollbackAttempted, Result and Error. On successful cancel, require an
attempted rollback, no Error and an accepted Result before treating restoration
as complete. `EndInteraction(abandon: true)` ends without writing into replaced
native state; it must not be treated as rollback. A host hiding the view must
explicitly end its interaction. A view/Inspector disposal abandons its session.

The host owns undo and native dirty policy. Existing writers/recorders receive
normal writes; the host can close one history gesture on InteractionFinished.
Cancelling may restore values while a native sticky dirty flag remains set.
Configuration changes themselves emit no edit-interaction or value-write event.

`DrawEditControls(node)` reuses the controls for a host-owned custom row, without
requiring a table. It leaves the cursor on the same line for the host's label.
The host must honor that node's IsEditLocked in its own direct-action handlers;
calling this drawing method does not intercept arbitrary custom writes.

Renderer-owned textures can use the same slots:

```csharp
view.EditIcons = new ImGuiEditIcons(lockTexture, chainTexture); // ImTextureRef
```

The view does not decode PNGs, upload/free textures or own a renderer. It provides
draw-list fallback symbols when no textures are supplied. The owner's chain.png
(1254x1254) and lock.png (800x800) have alpha and can be supplied/tinted by the
host; their originals are preserved. No additional masks are required by this API.

## View parity and release boundary

ImGui integrates absolute edits, chained scrubs, cancel and the new controls.
Avalonia, WinForms and WPF honor IsEditLocked and disable label scrubbing, but
have no chain controls or chained-edit gesture integration in this increment.
Their existing user edits retain their prior setter path. Hosts can use the
core explicit edit API independently; this is not a claim of full view parity.

This is local source work. No version bump, package publication or consumer
upgrade is implied. Engine consumption must explicitly select a matching local
core/view build; changing this checkout does not update NuGet 0.3.0.
