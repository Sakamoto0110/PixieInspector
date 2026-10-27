# Editor for Flags Enums

**Document ID:** GLOBAL-PROPOSAL-FLAGS-ENUM

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** current

**Subject:** an editor that sets and clears the bits of a `[Flags]` enum

**Surface:** proposal

**Boundary:** core

**Authority role:** non-normative

**Mutation:** authored

**Indexing:** include

## PXI-PROPOSAL-007

**State:** completed as [PXI-TODO-002](../archive/flags-editor-2026-10-26.md) on 2026-10-26

**Idea:** An editor for `[Flags]` enums that sets and clears each bit, chosen by
the reflection layer when the enum type carries `FlagsAttribute`.

**Rationale at the original baseline:** A `[Flags]` enum was shown as a single-choice list of its
defined values, so a combination of bits could be neither picked nor shown as one
of the choices. Verified on 2026-10-12: neither the core nor any view looked at
`FlagsAttribute`. Engines often keep layers and render options as flags.

**Accepted extension:** The owner selected both `[Flags]` enums and integral
masks with runtime-provided names, covering the native Engine's collision
layers and debug masks. All four views gain the same editing contract. Unknown
bits and per-object differences must survive unrelated bit edits.

**Constraints:** This is a generic editor; the host supplies native names and
values. Physics, scene synchronization and viewport tools remain outside the
inspector. Package publication is a separate release action.

**Source:** owner discussion of the PixieEngine editor on 2026-10-11 and
2026-10-12, recorded in
[PXI-ARCHIVE-008](../archive/editor-discussion-2026-10-12.md)
