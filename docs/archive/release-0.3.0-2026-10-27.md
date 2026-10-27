# Version 0.3.0 Release Preparation

**Document ID:** ARCHIVE-RELEASE-030-20261027

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** authorized release of generic flags and runtime-named masks

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-020

**Type:** dependency

**Origin:** owner decision

**Author:** codex

**Reference date:** 2026-10-27

**Commits:** `ba92f5c061205b8c40ade4b76dbec2cefe1e1064`; documentation with this record (parent `ba92f5c061205b8c40ade4b76dbec2cefe1e1064`)

## Authorization and source

The owner authorized merge and public package publication, superseding the
earlier no-release boundary recorded in PXI-ARCHIVE-019. PR #11 was merged using
a merge commit, `6daa7451a57bcaf7c772ccb8f7a4431c0c7c6c2a`, preserving code commit
`4b9c197f654d26cc25aab58954fb8c6365edec30` and its documentation commit
`031e6334acd8df2e0925c2b230e5b3ae3df21d52`. Both PR CI jobs passed.

Version 0.3.0 introduces public generic flag editing for enums and runtime-named
numeric masks in the core and all four views. Existing archives retain their
historical facts. This release does not add component-specific physics or change
the compatible PixieLib package reference.

## Preparation evidence

All eight VAW routes passed for profile pixieinspector/1. Before preparation,
each of the five NuGet version indices contained only 0.1.0, 0.2.0 and 0.2.1;
the v0.3.0 remote tag was absent and GitHub's latest release was v0.2.1.

The shared Version is now 0.3.0. Release solution build with Windows targeting
and warnings as errors passed with zero warnings/errors. Both existing console
hosts completed successfully. Local packing produced five packages at 0.3.0;
inspection of each nuspec confirmed all four views require core exactly
`[0.3.0]`, preserving the pack contract.

The release-preparation branch is reviewed through a separate PR. After green
PR CI and authorized merge, the existing Publish workflow on main builds/checks
again, pushes all five packages through trusted publishing and creates the
v0.3.0 tag/release at that workflow's revision. Those external outcomes must be
verified separately; this preparation record does not claim they already ran.

The 119 external functional checks remain local evidence from PXI-ARCHIVE-018,
not CI tests. Engine layer-name bridging, package adoption and native consumer
acceptance remain separate integration work.
