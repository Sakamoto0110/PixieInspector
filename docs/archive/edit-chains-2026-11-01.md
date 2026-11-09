# Numeric UI Chains and Manual Edit Locks

**Document ID:** ARCHIVE-EDIT-CHAINS-20261101

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** local completion of the generic numeric edit-chain and UI-lock base

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-022

**Type:** completion

**Origin:** PXI-TODO-003, owner request and delegated responsibility

**Author:** codex

**Reference date:** 2026-11-01

**Commits:** `bfe04d5c3ae300a6d2a787577943d76419a186e1`; documentation with this record (parent `bfe04d5c3ae300a6d2a787577943d76419a186e1`)

**Reference commit:** working-tree delivery atop `139267f6e8edf741fc7d659bbbc172c55988dbf1`

**Current state:** [core contract](../modules/core/REFERENCE.md), [ImGui contract](../modules/view-imgui/REFERENCE.md), [host guide](../edit-chains.md)

## Accepted work snapshot

### PXI-TODO-003 — UI edit chains and manual edit locks

**Status:** done locally

**Source:** owner request and delegated responsibility, 2026-11-01.

**Accepted scope:** symmetric static eligibility, canonical runtime chains
(including an armed singleton), absolute and snapshot-delta UI edits, a
direct-edit-only lock, ImGui controls/interaction lifecycle, bounded view parity,
and meaningful binding and input checks. Existing programmatic setters, real
read-only guards, conversion, pending edits and failure reporting remain
authoritative. Cross-Inspector links are explicit host configuration, in view
units; no semantic pairs are inferred.

**Gates:** no commit, push, version bump, package publication or Engine edit here.

**Completion criteria:** core and ImGui checks including mixed/multi-bind, cyclic
eligibility, lock bypass only for propagation, cancel, clamp/refusal/partial
effects and lifetime invalidation; solution builds without warnings/errors;
host baselines unchanged; contracts, evidence and view limitations recorded.

## Delivery and verification

Static eligibility is symmetric; active chains share one flat group. The first
click arms a singleton and a right click clears the runtime group. Explicit UI
edits visit each member once. Absolute edits use the same displayed value;
scrubbing uses each member's and each binding's start snapshot. UI locks reject
direct edits while allowing propagation through normal guarded setters.
Rebinding, collection selection changes, topology changes and disposal invalidate
old gesture snapshots. ImGui exposes interaction events, explicit completion or
abandonment, renderer-owned icon slots and reusable custom-row controls.

Local Debug and Release solution builds passed with zero warnings and errors.
TuxHost and TerminalHost dump outputs remained byte-identical to their baselines.
External behavior harnesses passed 175 checks in total:

- 50 core assertions cover groups/cycles, source and peer guards, per-binding
  delta baselines, units, ranges, pending state, refusal/partial writes, cancel,
  large-integer precision, nested structs and lifetime invalidation.
- 19 native headless ImGui assertions cover mouse controls, label scrubbing,
  Escape, unlinking, renderer texture slots and abandonment. Real keyboard
  Ctrl+A, typed `42` and Enter through the production numeric editor update both
  bindings and the locked peer, with exactly one interaction start and finish.
- 76 core flags, 24 retained-view and 6 native ImGui flags checks preserve the
  earlier generic flags implementation. Retained-view checks include UI locks.

A deliberately broken external core copy used the first member's snapshot for
all deltas. The harness failed specifically at per-member/per-binding baseline
preservation. Restoring the copy passed the then-current 47 core assertions;
three collection-lifetime assertions subsequently expanded the final set to 50.
Production source was not mutated for this sensitivity check.

The Engine integration owner reported 42/42 native-backed feature checks in each
configuration, default/local dependency builds, grouped history and structural
replacement checks. That is separate consumer evidence; the Inspector checks
above do not prove native history or world replacement. No additional production
Inspector change was required by that integration.

## Limits and remaining acceptance

This completes the local library base, not owner visual acceptance, CI adoption
of the external harnesses, publication or a default consumer upgrade. Avalonia,
WinForms and WPF honor manual locks but retain their existing setter paths and
have no chain controls or chained-gesture integration in this increment.
Custom handlers must enforce their node's UI lock themselves. Cancellation uses
ordinary non-transactional setters and can fail; restoring native values need
not clear a host's sticky dirty flag. Maximum compound-group performance remains
unqualified by the consumer. Additional field candidates and possible chain-group
features were advance notice only and are outside this completed scope.

Code remains an uncommitted working-tree delivery at the parent recorded above.
Version and package references are unchanged. Documentation was reviewed under
the repository policy; no repository-wide documentation verifier was executed.

## Commit reconciliation — 2026-11-02

The owner authorized committing the pending Inspector delivery without pushing.
The 25 production source hashes matched the validated checkpoint before commit;
no implementation change or repeated matrix was necessary. Code is committed
at the hash in metadata, with documentation in a separate commit on
`codex/edit-chains-locks`. Local evidence and generated files remain outside Git.
The earlier uncommitted description is the preserved November 1 snapshot.
Owner visual acceptance, release and default consumer adoption remain separate.
