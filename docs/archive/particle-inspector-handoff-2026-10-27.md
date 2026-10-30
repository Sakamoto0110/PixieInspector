# Particle Consumer Handoff Assessment

**Document ID:** ARCHIVE-PARTICLE-INSPECTOR-HANDOFF-20261027

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** reuse of the existing flag editor and remaining particle consumer dependencies

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-018

**Type:** decision

**Origin:** owner decision, Engine particle-physics handoff

**Author:** codex

**Reference date:** 2026-10-27

**Commits:** uncommitted working tree, parent `7701df775c6b3e586323d86fdfa7616824cd9732`; no commit requested

## Accepted action and provenance

The owner explicitly chose to reuse the existing implementation after being
informed that PR #10 had been closed without merging. Its code and documents
were applied without committing to `codex/particle-inspector-support`, based on
`7701df7`. The source is `codex/flags-enum` at
`f44ed272a287bda14605873f48fabacb7963b0f5`, including implementation
`585ed602a6d8c8e03f2701b478008ff481390ea2`. Its original completion record,
PXI-ARCHIVE-017, remains unchanged. This is reuse of PXI-TODO-002, not a second
implementation of flags or authorization to reopen/merge its PR.

VAW profile `pixieinspector/1` returned `bound` with scope `all`, all eight routes
checked. Core, four-view, development and documentation references were read.
The initial Inspector checkout was clean. No Engine source, package version,
dependency or publication was changed.

## Capability assessment

| Consumer requirement | Inspector support and remaining boundary |
|---|---|
| ParticlePhysics2D fields | Existing manual bool/int/double fields, descriptor options and multi-bind suffice. No component-specific core API is needed. |
| Conditional attachment | Engine `Authoring.AdditionError` queries the native component-add reason, and the panel disables refused additions. Runtime execution of this path was not retested here. |
| ParticleSpawner.dynamic | Existing Toggle and descriptor ordering suffice. Native descriptors place it after target and before ownLook; system remains an Entity reference handled by existing choices. Birth semantics stay native. |
| Named bit masks | Reused Flags API and four editors supply the missing generic capability. Engine must explicitly bridge runtime names for the new Int32 fields. |
| Validation and recovery | Existing binding failures and display nodes suffice for generic presentation. Native tick diagnostics must be polled independently of field-read/write success. |
| Native mass and angular units | Existing read-only fields/displays and display units suffice; no managed mass/inertia calculation was added. |
| Timing and gauges | Remain Engine telemetry, with no Inspector simulation or per-particle tree. |

## Consumer evidence and outstanding dependencies

Engine inspection used only the local worktree
`C:/Users/Nekovih/.codex/worktrees/pixieengine-codex/PixieEngine` on
`codex/physics-workloads`. HEAD
`d03db62c775005fc4c614f07e205b7a4a36d9efa` is the committed base; particle
changes were uncommitted. This record does not claim that fetching that commit
delivers them.

`runtime/src/RuntimeComponents.cpp` defines the pool fields as ordinary numeric
descriptors. Unlike Collider2D, the new category/mask fields do not carry
LayerMask/CollisionLayers2D editor metadata. `InspectorFactory` currently builds
Int32 nodes as numbers, skips LayerMask descriptors for the panel's custom layer
UI, and never supplies `FlagChoices`. Native descriptor/editor metadata and/or
the Engine adapter therefore need an explicit bridge to scene layer names.
Category requires a positive 31-bit value; mask permits zero. These are per-field
policies, not a global Flags restriction.

The descriptor uses `Range(...).Scrub(...)`: its .05/.01 increments are scrub
multipliers, not evidence of a nonzero Range.Step. Preserve that distinction.
The collision radius range begins at .01 world units; actual native validity
requires a radius strictly above .01 * unitsPerMeter after float conversion.
No Inspector range or managed fixture validates that physical constraint.

`EngineSession.MovementStatus` already measures and reads the native status
buffer; `SessionPanel` polls it every 250 ms and displays nonempty text. Thus the
new pool summary has an existing generic presentation path. This is source
inspection, not acceptance of native recovery, entity association or UI layout.
No new C export, struct or API version is required by this handoff.

The consumer still references `PixieInspector.ImGui` 0.2.1. Checkout changes do
not update that package. A separate release/adoption step and a matching native
consumer test remain necessary. Such a test must use this Engine worktree's
`bin/Release/sparks-capi.dll` and `samples/sparks`; no native DLL was loaded here.

## Fresh verification

Against the integrated Inspector checkout on 2026-10-27:

- Release solution build with Windows targeting and warnings as errors: zero
  warnings and zero errors.
- Reused external harnesses: 76 core checks, 21 actual retained-view control
  checks (Avalonia/WinForms/WPF), six headless ImGui input checks passed.
- Sixteen additional managed adapter-contract checks passed: primitive and
  metadata selection, boolean multi-write, native-status snapshot/recovery,
  distinct category/mask zero policies, per-object unknown bits, radians/degree
  conversion, native read-only mass, whole-batch preparation refusal and corrected
  write recovery, and struct-owner mask write-back.
- TuxHost and TerminalHost dump remained byte-identical to the pre-integration
  baselines. `git diff --check` passed.

These are Inspector functional checks. The new adapter-contract console uses
managed fixtures, not EngineSession or native physics, and does not validate the
native conditional add path or solver recovery. The Engine's prior runtime and
CTest results are separate historical evidence, not Inspector acceptance.

Fresh logs and baselines are local under
`.local/validation/particle-handoff-2026-10-27/`. External harness sources are
under `C:/Users/Nekovih/.codex/tmp/inspector-particle-handoff-2026-10-27/`, per the
development guide. They are not CI tests or a manual visual acceptance.

## Publication reconciliation — 2026-10-27

After this assessment, the owner authorized commits, a branch push and a new PR
against main, without merging or releasing packages. The pre-publication facts
above remain the original snapshot. See [PXI-ARCHIVE-019](flags-pr-publication-2026-10-27.md)
for the integrated code commit and updated Engine provenance.
