# Flags Implementation PR Publication

**Document ID:** ARCHIVE-FLAGS-PR-PUBLICATION-20261027

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** authorized publication of the reused flags implementation for review

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-019

**Type:** decision

**Origin:** owner decision, PXI-ARCHIVE-018

**Author:** codex

**Reference date:** 2026-10-27

**Commits:** `4b9c197f654d26cc25aab58954fb8c6365edec30`; documentation with this record (parent `4b9c197f654d26cc25aab58954fb8c6365edec30`)

## Authorized publication

The owner authorized committing the accepted flags/mask implementation and
particle consumer assessment, pushing `codex/particle-inspector-support`, and
opening a new PR against main. Closed PR #10 remains closed. Review publication
does not authorize a merge, package release, version change or Engine edit.

Code was committed separately from documentation under the configured owner's
author and committer identity. The implementation is byte-identical to the
core and four-view code of `codex/flags-enum` at
`f44ed272a287bda14605873f48fabacb7963b0f5`. PXI-ARCHIVE-017 retains the original
delivery provenance, and PXI-ARCHIVE-018 retains its pre-publication observations
with an appended pointer to this reconciliation.

VAW profile `pixieinspector/1` was revalidated with scope all: bound, eight routes
checked, none unchecked. Existing local code and documentation were preserved.

## Evidence and CI boundary

The integrated-checkout validation recorded in PXI-ARCHIVE-018 consists of a
warning-free Release solution build, unchanged host dumps and 119 external local
checks: 76 core, 21 retained-view controls, six headless ImGui input checks and
16 managed adapter-contract fixtures. These external harnesses are not CI tests
and do not exercise the actual native EngineSession consumer.

The repository CI separately builds on Ubuntu and Windows with warnings as
errors, runs TuxHost and TerminalHost --dump, and packs the five artifacts. Packing
in CI does not publish them to NuGet. PR/check URLs and terminal CI outcomes are
reported with the published review; no CI result is claimed before it completes.

## Engine provenance reconciliation

Engine [PR #2](https://github.com/Sakamoto0110/PixieEngine/pull/2) is now merged
into main as `703e860b40ccbb496178ae27d9c0e06258c10fe3`. Its implementation commit
is `3eb87f02f9147b4153f8b847c89309ce8471d87a` and its documentation commit is
`89a7c90f1230c6d06f838c410f878f96f23614c0`. These identities and the merged state
were verified from GitHub. The earlier observation of uncommitted changes on
base `d03db62` remains valid for the historical assessment; fetching the updated
Engine main now includes the implementation.

Remaining consumer work is unchanged: bridge particle numeric category/mask
descriptors to scene-owned layer names and `FlagChoices`, explicitly adopt a
matching Inspector package/source build, and test the actual EngineSession/native
consumer, including conditional attachment and native diagnostics/recovery.
Neither generic ranges nor scrub multipliers establish physical validity;
Scrub and Range.Step remain separate metadata. No native physics, component
implementation or simulation telemetry ownership moved into the Inspector.
