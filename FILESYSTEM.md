# Inspector filesystem and authority map

**Document ID:** GLOBAL-FILESYSTEM
**Schema version:** 1
**Kind:** reference
**Lifecycle:** current
**Subject:** Inspector ownership, parent workspace and focused documentation routes
**Surface:** index
**Boundary:** global
**Authority role:** non-normative
**Mutation:** authored
**Indexing:** include

This map locates Inspector-owned facts. Start at the
[documentation index](docs/README.md) for a technical question, or the
[workspace entry](../../.governance/workplaces/void-workplace/README.md) for
coordination across Engine, Editor, Inspector and Lib. The
[shared contract](../../.governance/DOCUMENTATION.md) and
[workspace policy](../../.governance/workplaces/void-workplace/docs/governance/documentation-policy.md)
govern shared constraints; the [Inspector policy](docs/governance/documentation-policy.md)
defines local specialization. A peer dependency does not become Inspector's parent.

## Where to work

| Area | Owner and direct reading route |
|---|---|
| `PixieInspector/` | Inspector core discovery, binding, nodes and reports; [core contract](docs/modules/core/REFERENCE.md) |
| `views/` | Inspector UI adapters; [module index](docs/README.md#modules) routes each view's contract and manifest |
| `hosts/` | Sample applications and shared demo objects; [host index](docs/README.md#sample-hosts) routes their purpose and execution instructions |
| `*.csproj`, `Directory.Build.props`, `Directory.Build.targets`, `PixieInspector.slnx` | Project targets, dependency wiring, shared package properties and solution membership; [development](docs/development.md) explains their use |
| `version.json`, `.github/workflows/` | Product version and CI/publication wiring; [package and release procedure](docs/development.md#packages) |
| `DEPENDENCIES.md` | [Dependency status and rationale](DEPENDENCIES.md); consumed Lib and host contracts retain their own owners |
| Root planners | [ROADMAP](ROADMAP.md) owns Inspector outcomes and criteria; [TODO](TODO.md) owns accepted Inspector work; [TASKLIST](TASKLIST.md) is optional decomposition |
| `docs/modules/`, `docs/proposals/` | Technical contracts, module routes and proposed designs; the [documentation index](docs/README.md) selects the relevant owner |
| `docs/history/`, `docs/archive/` | Existing outcomes, evidence and original chronology, preserving IDs, bytes and baseline limitations |
| `.local/` | Local working state, retained evidence and coordination records; inspect ownership and retention needs before any separately authorized cleanup |
| `bin/`, `obj/`, host-generated `imgui.ini` | Generated outputs; change their inputs rather than treating generated copies as authored authority |

## Shared coordination and project responsibility

The [workspace ROADMAP](../../.governance/workplaces/void-workplace/ROADMAP.md)
and [workspace TODO](../../.governance/workplaces/void-workplace/TODO.md) own the
shared coordination parents originating as Engine 053-056, 060 and 061.
Inspector records its contributions in its own planners and links the workspace
parent; it does not maintain a second mutable copy of that parent's status.
Pinned project fallback excerpts are dated, non-authoritative reading context.

Inspector still owns its core and view contracts, package responsibilities and
bounded evidence. Editor host behavior and Engine semantics remain with their
projects. Shared coordination does not accept package, UI or consumer gates.
Inspector's [branch and PR rules](docs/governance/documentation-policy.md#commits)
and [release procedure](docs/development.md#packages) remain applicable.

## Standalone reading and retained evidence

Local references remain usable without the parent checkout. An unavailable parent
or peer is unresolved context, not a waived dependency or verified workspace.
Structural reports must identify absent optional scopes; full workspace checks
require every declared scope. Prepared documentation does not activate VAW.

Local evidence may be the only record of a bounded check or package handoff.
Being ignored or unversioned is not permission to delete it. Promote durable
conclusions to their versioned owner while retaining required raw evidence.
