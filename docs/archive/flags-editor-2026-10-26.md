# Enum Flags and Runtime-Named Masks

**Document ID:** ARCHIVE-FLAGS-EDITOR-20261026

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** completion of generic flag editing in the core and four views

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-017

**Type:** completion

**Origin:** PXI-TODO-002, from PXI-PROPOSAL-007 and owner decision

**Author:** codex

**Reference date:** 2026-10-26

**Commits:** `585ed602a6d8c8e03f2701b478008ff481390ea2`; documentation with this record (parent `585ed602a6d8c8e03f2701b478008ff481390ea2`)

**Current state:** [core contract](../modules/core/REFERENCE.md#flags), [host guide](../flags-editor.md)

## Accepted work snapshot

### `PXI-TODO-002` — Edit enum flags and runtime-named masks

**Status:** done
**Source:** owner selection of [PXI-PROPOSAL-007](../proposals/flags-enum-editor.md) on 2026-10-26, explicitly including runtime-named numeric masks.
**Accepted scope:** detect `[Flags]` enums; expose named masks for integral fields; implement the editor in ImGui, Avalonia, WinForms and WPF. Preserve unknown bits and each object's other bits during multi-edit. Cover zero/composite masks, signed/unsigned widths, nullable values, read-only state, binder control and validation failures. Physics calculations and native synchronization remain host responsibilities.
**Dependencies and gates:** existing .NET 10 and view dependencies; no new assembly or package dependency. NuGet publication and consumer upgrades remain separate release actions.
**Completion criteria:** solution builds without warnings/errors; core and four-view checks pass, including a deliberate regression; existing host dumps retain their behavior; current contracts and executed evidence are recorded.

### Task snapshot

**Parent:** PXI-TODO-002

- `PXI-TASK-008` — **Status:** done — Typed choices and per-object mask editing preserve normal binder control and diagnostics.
- `PXI-TASK-009` — **Status:** done — Checkbox popup editors are wired in all four views, with mixed/read-only states and popup cleanup.
- `PXI-TASK-010` — **Status:** done — Core edge cases, actual view controls and native ImGui input, unchanged host baselines, and a deliberately broken implementation were checked.
- `PXI-TASK-011` — **Status:** done — Current references, host guidance, executed evidence and separate release boundaries are recorded.

## Delivery and verification

`EditorKind.Flags` was appended, leaving existing enum numbers intact. Native
adapters can supply `FlagChoices`; ordinary enums keep their previous editor.
Edits use each object's own held value and pass through `SetValues`, including
normal preparation, pending edits, read-only checks and notifications. Unknown
bits survive unrelated edits. Zero is an explicit reset; composite masks act
together. Provider recovery clears its own failure without hiding a failed
getter or rejected write. Popups close when disabled or disposed.

On Windows with .NET 10, Debug and Release solution builds passed with
`EnableWindowsTargeting=true` and warnings as errors: zero warnings/errors.
The existing TuxHost output stayed byte-identical at 67 lines and TerminalHost
`--dump` at 132 lines, including successful binding checks.

Per the development guide, behavior-check consoles were kept outside the
repository, under the local VAW validation directory. The final 76 core checks cover
all eight integral storage types, 64-bit/sign bits, negative enum constants,
aliases, partial composites, zero reset, unknown bits, nullable values,
mixed selection, pending edits, manual fields, read-only state, rules and
getter/provider failure/recovery. A rule rejecting the second prepared value
left both objects unchanged.

The retained-view console passed 21 checks over actual WinForms/WPF/Avalonia
controls and programmatic click events, including mixed states, per-object
preservation, literal labels, read-only controls and cleanup. The headless
ImGui console passed six checks: its real popup/widgets were drawn, mouse
events entered through ImGui's input queue, and clicks on an ordinary and a
mixed bit preserved each object's other bits. No Computer Use was needed.

Sensitivity replaced each object's source mask with the first object's mask.
The core console failed specifically at per-object preservation. The original
source was restored byte-for-byte, rebuilt, and all 68 checks passed again.
The final run expanded that set to 76 checks with additional enum storage cases.
Local logs, source snapshots and harnesses are retained under
`.local/validation/flags-2026-10-26/` and
`C:/Users/Nekovih/dev/VAW/.local/validation/pixieinspector-flags-2026-10-26/`.

## Limits

These are local functional checks and native/programmatic input evidence,
not independent manual visual acceptance or a pixel-layout guarantee.
The external consoles are not part of CI; repository CI continues to build,
run its existing hosts and pack artifacts. Documentation was reviewed against
the policy; no repository-wide documentation verifier exists.

No package was published, no package version was raised, and no Engine
dependency or native source was modified. Downstream use needs a separate
package release and consumer integration. PR review/merge remains the owner's
decision. Work was isolated on `codex/flags-enum`, based on `ed86261`, while the
Claude particle work stayed in the separate Engine checkout.

## Branch reconciliation on 2026-10-30

The owner authorized documenting and merging the remaining `codex/flags-enum`
branch. Comparison against main at
`d9716b130d7ee4d426e1e71c8dd90cf3772075fe` confirmed that the original code
and documentation were already present through the reused implementation
(`4b9c197f654d26cc25aab58954fb8c6365edec30`) and its documentation, merged
in [PR #11](https://github.com/Sakamoto0110/PixieInspector/pull/11).
The old branch's distinct commit identities did not represent missing content.

Reconciliation commit `b27275fb17977708e44553e4640b6a14ce9bfed0` merges that
main baseline into the original branch. Its resulting tracked tree is identical
to that baseline: the archive router and flags guide retain main's later
additions, and package metadata remains at 0.3.0. This appended record is the
only content change proposed by the branch reconciliation PR. The merge makes
the original delivery commits reachable from main without duplicating code.

VAW binding was revalidated with scope `All`: all eight routes checked, none
unchecked. Tree comparison establishes content equivalence; it does not rerun
the historical functional checks above. The reconciliation does not authorize
a new package release or change the Engine's physics work.
