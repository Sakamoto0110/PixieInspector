# PixieInspector Task List

**Document ID:** GLOBAL-TASKLIST

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** current

**Subject:** task breakdowns of promoted TODO.md entries

**Surface:** task-scheduler

**Boundary:** global

**Authority role:** scheduler

**Mutation:** authored

**Indexing:** include

## Purpose

This file is a child scheduler of [`TODO.md`](TODO.md). Each section breaks
one promoted `TODO.md` entry into tasks, so that large entries, or entries made
of many small tasks, do not pollute `TODO.md`.

- Every section names its parent in `Parent`, and the parent links back here
  in `Breakdown`.
- Tasks decompose the parent's accepted scope and never extend it.
- When the parent completes, remove its active section. Preserve the roadmap
  outcome, evidence links and important nested decisions through the
  [closure flow](docs/history/README.md); Git retains the full breakdown.

The full rules are in the
[documentation policy](docs/governance/documentation-policy.md#scheduling-todomd-and-tasklistmd),
and the section shape is in the
[template](docs/templates/TASKLIST-SECTION.md).

## Open breakdowns

The inherited package contribution below retains its existing parent and gates.
No new decomposition is required merely to populate this file. Completed
outcomes and their original breakdowns are routed by the
[history index](docs/history/README.md).

<!-- BEGIN inherited-contributions -->
## Inherited project contributions

These are relocated existing tasks, not new product scope. Parent status and acceptance remain with the workspace; each task below has its only mutable state here. Historical product block labels retain their baseline meaning.

### PIXIE-TODO-060 contribution

**Parent:** [PIXIE-TODO-060](../../.governance/workplaces/void-workplace/TODO.md#pixie-todo-060--separate-engine-sdk-game-runtime-and-editor-delivery).

<a id="pixie-task-249"></a>
- `PIXIE-TASK-249` — **Status:** open — B7: qualify the D08-selected immutable local Inspector core/view package pair and extra feature baseline after whole-block assignment and exact clearance. Supply package-only G4 evidence without requiring sibling Inspector sources or remote publication. B5 owns Editor feature conditions, icons, semantic mapping and grouped-history adoption. Broader core/view behavior changes need a path or contract amendment.

[Required current block constraints](../../.governance/workplaces/void-workplace/TASKLIST.md#b6b7b8--standalone-adapters-and-conditional-dependencies). If parent/block context is unavailable, this task is unresolved for execution; the pinned reference is not a substitute.
<!-- END inherited-contributions -->
