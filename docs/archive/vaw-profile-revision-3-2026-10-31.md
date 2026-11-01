# VAW Profile Revision 3

**Document ID:** ARCHIVE-VAW-PROFILE-REVISION-3-20261031

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** the owner's adoption of VAW workflow, premises, validation and versioning capabilities for PixieInspector, in parity with PixieEngine

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-021

**Type:** decision

**Origin:** owner decision

**Author:** claude

**Reference date:** 2026-10-31

**Commits:** with this record (parent `5ebd68183dc0bd74a02a19044217be2ed3898dbc`); VAW `01bcf94`, `8ce42fe` on its local branch `codex/pixie-workplace`

**Current state:** the [development guide](../development.md#void-agent-workplace)

## Decision

On 2026-10-31 the three Pixie repositories moved into the VAW checkout's managed
Pixie family. After PixieEngine's profile gained VAW's working capabilities, the
owner asked for the same here ("pode fazer a revisao completa entao para manter
paridade com review 2 da engine"). Revision 2 of `pixieinspector` refined its routes and
validation list from the repository's real layout and documented checks;
revision 3 adds workflow state, continuity, premises, repository state,
validation, package validation and versioning (`void.versioning/2`), and the
`pixieinspector-documentation` and `pixieinspector-release` skills. Campaigns stay frozen. The
product version is owned by `version.json` since `40b2901`.

## Verification

- `verify-profile-adapters` valid for `pixieinspector`/3 and `verify-platform` valid.
- The binding resolves `bound` at revision 3 with all 8 routes, from the
  explicit registry; Claude lists the same seven skills as for PixieEngine.
- One workflow cycle under `.local/vaw/records/`: created, started, paused with a
  checkpoint on `version.json`, verified compatible; one added byte made it
  incompatible and the byte-identical restoration compatible again; resumed,
  completed, and a stale expected revision was refused.
- The profile's validation list passed: the solution built with no warning (`-p:EnableWindowsTargeting=true`), TuxHost printed its 67 lines, and TerminalHost `--dump` reported `bindings: 36/36 ok` with exit code 0.
