# Documentation Policy

**Document ID:** GLOBAL-DOCUMENTATION-POLICY

**Schema version:** 1

**Kind:** reference

**Lifecycle:** current

**Subject:** authority, lifecycle, ownership, scheduling, and archiving rules for PixieInspector documentation

**Surface:** policy

**Boundary:** global

**Authority role:** normative

**Mutation:** authored

**Indexing:** include

This policy defines where each kind of PixieInspector fact is owned and how
documents move between states. The structural vocabulary lives in the
[documentation schema](../schemas/documentation-schema.json); this policy owns
the meaning of that vocabulary.

The model is derived from the PixieEngine documentation policy and schema
(schema version 1, observed 2026-10-12 at PixieEngine `46ee6aa`), which is in
turn a partial derivation of the NekoLib model that Void Agent Workplace
extracted. The derivation and every intentional difference are recorded in the
[schema index](../schemas/README.md). Neither project is an authority over this
repository. The workspace owns shared governance and external project profiles;
Inspector retains its source, contracts, product planning and evidence. Machine-local
bindings route that configuration and are not durable authority.

## Authority

The [shared documentation contract](../../../../.governance/DOCUMENTATION.md)
and [Pixie workspace policy](../../../../.governance/workplaces/void-workplace/docs/governance/documentation-policy.md)
are the adopted parent rules for shared constraints and coordination. Their
precedence is limited to that scope. Inspector source, build wiring, core/view
contracts, package responsibilities and evidence keep their owners below.
A consumed peer contract governs the dependency it defines; consuming Lib or
serving an Editor host does not make either project Inspector's parent.

The [filesystem map](../../FILESYSTEM.md) provides the short ownership route.
Inspector's branch-per-task, owner-merged PR and release procedures remain local
specializations. Prepared documentation does not activate or rebind VAW.
If the parent is absent, report unresolved context rather than waiving its rules.

Every fact has exactly one owner. Other documents may summarize a fact and link
to its owner; they must not maintain a competing mutable copy.

| Fact | Owner |
|---|---|
| Implemented behavior | Current source code |
| Targets, references, package references, and build properties | Each project file (`*.csproj`) |
| Product version (four components) | `version.json`; `Directory.Build.props` derives the package `Version` |
| Metadata the packages share | `Directory.Build.props` |
| CI and publishing | `.github/workflows/` |
| Solution membership | `PixieInspector.slnx` |
| Dependencies (toolchain, packages, own projects), versions, status, and candidates | Root `DEPENDENCIES.md` |
| Public API | The compiled assemblies of the core and the views; no accepted API baseline exists yet |
| Module technical contract | `docs/modules/<boundary>/REFERENCE.md` |
| Module routing and identity | `docs/modules/<boundary>/MANIFEST.md` |
| Inspector direction, rationale, phase outcomes, acceptance criteria, guardrails and freezes | Root `ROADMAP.md` |
| Accepted Inspector work, contributions, status, ordering, dependencies and execution gates | Root `TODO.md` |
| Optional decomposition of a promoted `TODO.md` entry into tasks | Root `TASKLIST.md` |
| Shared coordination parents originating as Engine 053-056, 060 and 061 | Workspace `ROADMAP.md` and `TODO.md`; project planners link their contributions |
| Project filesystem and ownership routes | Root `FILESYSTEM.md` |
| Proposed designs and their originating rationale | One record per file under `docs/proposals/`; the index retains promotion/outcome states |
| How to build, check, and commit a change | `docs/development.md`, under the commit rules below |
| What a host shows and how to run it | `hosts/<host>/README.md` |
| Confirmed defects | Module `ISSUES.md` |
| Unconfirmed observations | Module `FINDINGS.md` |
| Evidence requirements | Module `VALIDATION_REQUIREMENTS.md` |
| Executed evidence | Canonical reports, scenarios and original archive records; indexes link to those owners |
| Completed outcomes and acceptance dispositions | `docs/history/`, linking evidence and the Git planning baseline |
| Legacy chronology, completed work, superseded state and dated rationale | `docs/archive/`, preserved at its original baselines |
| Documentation structure and vocabulary | `docs/schemas/documentation-schema.json` |
| Open questions and answers before promotion | `.local/` (unversioned; never authority) |
| VAW coordination records: workflow state, checkpoints, premises, validation | `.local/vaw/` (unversioned; never authority) |

Generated output is never authority and is never versioned: `bin/`, `obj/`, and
the `imgui.ini` a host writes where it runs.

## Scheduling: `TODO.md` and `TASKLIST.md`

The workspace adopts ROADMAP -> TODO -> optional TASKLIST. ROADMAP owns the
big picture, rationale, phases and acceptance criteria. TODO records accepted
work needed to achieve those outcomes. TASKLIST decomposes only large items;
children reference their parent and do not expand its scope. Owner instructions
can authorize work; record the accepted scope without creating another queue.

Shared coordination parents live in the
[workspace ROADMAP](../../../../.governance/workplaces/void-workplace/ROADMAP.md)
and [workspace TODO](../../../../.governance/workplaces/void-workplace/TODO.md).
Inspector contribution entries link those parents while keeping local scope,
dependencies and gates here. Pinned fallback excerpts in project planners are
dated, non-authoritative context; only the workspace owner maintains current
parent status. Fallback excerpts cannot authorize current coordination work when
parent context is missing. A project TASKLIST remains optional, including for
contributions.

Completion preserves the roadmap block and its outcome in one document under
`docs/history/`, linking evidence and incorporating only important nested details.
Record original work IDs and the last planning commit. Remove completed entries
from active TODO/TASKLIST; do not archive copies of all three lists. A completed
child does not close its active roadmap parent. Keep permanent rules and freezes
in their governing document. Legacy direct-owner work without a roadmap block
states that limitation instead of inventing one.

Use the history index to discover closures. Existing archive records remain
unchanged at their historical baselines. Evidence records distinguish requirements,
actual execution and limitations; a structural check cannot accept a product gate.

## Dependencies

`DEPENDENCIES.md` lists every dependency: toolchain, packages, and the owner's
own projects. Each one has a status observed on the file's `Last checked`
date:

- `ok`: present where it is consumed, at a current or deliberately pinned
  version;
- `outdated`: present, but a newer release exists and the gap matters;
- `not-installed`: not present yet.

A suggested dependency stays under candidates until the owner adopts it. A
status is an observation from its date, not a guarantee; recheck before a
version change. The project files own the wiring: which project references
which package, at which version. `DEPENDENCIES.md` records why and the status.

## Unversioned working documentation: `.local/`

`.local/` holds unversioned working documents, retained raw evidence, package
handoffs and coordination records. It is not a disposable directory by default.
Before separately authorized cleanup, identify the owner, active consumers and
retention requirements; an ignored path is not deletion permission. Promote
durable conclusions to their versioned owner while retaining required evidence.

Working questions, such as `.local/decisions/open-questions.md`, are numbered so
the owner can answer by number. After an answer, an item is either promoted,
expanded into `TODO.md`, recorded in `ROADMAP.md`, turned into a proposal, or
explicitly discarded without deleting evidence that still supports a claim.

`.local/` is never authority, and versioned documents never link into it,
because a clone does not contain it. An answer that shapes code, structure, or
direction must be promoted to a versioned owner; an unpromoted answer is not a
repository fact.

## Archive

`docs/archive/` preserves the earlier chronology and imported provenance.
Its records and router keep their original identities, bodies and baselines.
The [history index](../history/README.md) routes completed outcomes to those
records without copying them.

For new completions, use the roadmap closure flow under `docs/history/`.
Preserve intent and outcome, link canonical evidence and record the planning
commit. Do not copy complete TODO/TASKLIST sections or create separate decision,
promotion and completion records for the same fact. Important nested decisions
belong in the closure or remain linked at their existing owner.

The following fields describe the legacy archive shape and remain applicable
when maintaining its records:

Every record identifies what produced it, when, and in which commits:

- `Archive ID`: a stable `PXI-ARCHIVE-NNN`, assigned in order and never reused.
- `Type`: `decision`, `promotion`, `completion`, `fix`, `finding`, `proposal`,
  `dependency`, `roadmap`, or `import`.
- `Origin`: the identity of what produced the event, such as `PXI-TODO-001`,
  `PXI-PROPOSAL-002`, `CORE-FINDING-001`, or `owner decision`.
- `Author`: who wrote the record: `owner`, `claude`, or `codex`. It names the
  writer of the record, not of the commits, which carry the owner's identity.
- `Reference date`: when the event happened.
- `Commits`: the full hashes of the commits that carried the event. A record
  added in the same documentation-only commit as its event cannot name that
  commit; it says `with this record (parent <full hash>)`, and
  `git log -S <Archive ID>` finds it.
- `Reference commit`, optional: the baseline a record describes, when it
  differs from `Commits`.
- `Original path`, for a moved or imported record; imported records name the
  source repository in it.

A record about one module carries that module's key in `Boundary`; there is no
second, per-module history.

[`docs/archive/README.md`](../archive/README.md) is a router: one table row per
record, in `Archive ID` order, with the ID, date, type, origin, author, a
one-line summary, short commit hashes, and the link to the record. A row says
enough to decide whether a record is the one needed; the record holds the full
context. Find the event in the router, then read only its record.

- Records are `Lifecycle: historical` and `Mutation: snapshot`. The body and
  baseline are preserved; only narrowly identified metadata or link
  corrections and appended reconciliation sections are allowed.
- A record is true only for its baseline. It never owns current technical
  facts, open work, or direction.
- An imported record keeps the language of its body. The documents written
  before this policy (PXI-ARCHIVE-001 to PXI-ARCHIVE-004) are in Portuguese,
  under an English header, and the source still cites them by question number
  (`P5.6`) and by section (`3.11`).

## Module surfaces

A module is one boundary under `docs/modules/<boundary>/`: the core, or one
view. A module is created when its boundary is decided.

Required from creation:

- `MANIFEST.md` routes identity, projects, target frameworks, dependencies,
  public API surface, documentation surfaces, evidence locations, and related
  boundaries. It does not reproduce public symbols.
- `README.md` is a concise introduction.
- `REFERENCE.md` is the single normative current technical contract.

Created on demand, together with their first record: `ISSUES.md`,
`FINDINGS.md`, `VALIDATION_REQUIREMENTS.md`, `VALIDATIONS.md`, `CHANGELOG.md`,
`INTERNALS.md`, `audits/`, and `migrations/`. The absence of an on-demand
surface means no record has been written; it is not evidence that no defect,
finding, or requirement exists.

`Related boundaries` aids traversal only. It does not create or imply a build
or package dependency.

## Hosts, scenarios, and oracles

The hosts under `hosts/` are samples: each demonstrates what the core and a
view already do. A host is documented by one `README.md` in its own directory:
what it shows, how to build and run it, and its options. It is not a module.
`hosts/DemoObjects/` holds the objects the hosts inspect and is documented the
same way.

A project with a purpose beyond demonstration needs more: a scenario proves a
specific claim, and an oracle breaks something on purpose to show that the
failure is caught. Its `README.md` also states the claim or the failure, the
expected outcome, and how to read the result. What such a project demonstrates
is evidence; it never defines behavior.

## Commits

- Messages are in English and start with a prefix in parentheses: `(feat)`,
  `(fix)`, `(refactor)`, `(testing)`, `(chore)`, or `(docs)`. The subject is
  short; the body says what changed and why.
- Code and documentation go in separate commits. The documentation commit that
  records code work cites the hashes of the code commits.
- Commits carry the owner's identity only, as author and committer:
  `Rafael Sakamoto <rafael.sakamoto1@hotmail.com>`, with no co-author or tool
  attribution lines.
- Work is pushed to a branch of its own, one per task, and reaches `main`
  through a pull request merged by owner decision. A branch is deleted once it
  is merged; no long-lived work branch is kept.
- History may be rewritten only before it is pushed; afterwards, a correction
  is a new commit.

## Metadata

Indexable documents carry the bold-label metadata defined by the schema, one
value per physical line, near the top of the file. `Kind`, `Lifecycle`,
`Surface`, `Boundary`, `Authority role`, `Mutation`, and `Indexing` are
independent dimensions. Paths in metadata are repository-relative with forward
slashes.

- `current` is maintained with the repository.
- `frozen` stays live but cannot expand without its recorded unfreeze condition.
- `historical` is a dated snapshot and never current-state authority.
- An empty registry states its baseline explicitly.

Document IDs and record IDs are stable and never renumbered or reused.

## Language and authoring

Versioned and `.local/` documentation is written in English, except the body of
an imported record. Documents must be understandable without a private prompt,
a conversation, an external workspace, or knowledge of which person or tool
wrote them. External agent-tool procedures may add emphasis but do not override
the adopted parent scope, local fact owners, or product acceptance criteria.

## Conflict handling

- If source and a current normative reference disagree, record the exact
  conflict and stop for disposition; do not silently rewrite either side.
- Evidence demonstrates claims but does not define behavior.
- A finding becomes an issue only after verification.
- Historical bodies are not rewritten to match later truth; later outcomes are
  recorded separately.

## Enforcement

Use the generic PowerShell documentation checker with this repository as
`-RepositoryPath` and an explicit configuration selecting its metadata schema
and declared scopes. `-AdditionalPath` includes new documents without scanning
unrelated ignored state. A standalone configuration may declare absent parent
or peer scopes optional, reporting them as unavailable and not validated.
Full workspace qualification requires all participating scopes and current
catalog source hashes; an optional-scope pass is not that qualification.

The checker validates declared structure, links/anchors, identifiers, metadata
and work references. It does not establish implementation, visual, package or
consumer acceptance. Documentation-only changes use structural review and
history-preservation checks; they do not require reopening product tests.
Standalone source builds remain independent of VAW. Reports name the checker
scope and distinguish structural verification from manual semantic review.
