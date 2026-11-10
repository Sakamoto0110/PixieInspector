# Documentation Index

**Document ID:** GLOBAL-DOCUMENTATION-INDEX

**Schema version:** 1

**Kind:** reference

**Lifecycle:** current

**Subject:** entry point to PixieInspector documentation

**Surface:** index

**Boundary:** global

**Authority role:** non-normative

**Mutation:** authored

**Indexing:** include

Choose a route by the question you need to answer. The
[documentation policy](governance/documentation-policy.md#authority) names the
owner of each fact; recorded evidence keeps its original candidate scope.

## Workspace and repository routes

| Route | Responsibility |
|---|---|
| [Inspector filesystem map](../FILESYSTEM.md) | Project areas, local owners and retained evidence |
| [Workspace entry](../../../.governance/workplaces/void-workplace/README.md) | Parent navigation across the four independent projects |
| [Shared documentation contract](../../../.governance/DOCUMENTATION.md) and [workspace policy](../../../.governance/workplaces/void-workplace/docs/governance/documentation-policy.md) | Shared constraints and coordination; Inspector technical contracts and local PR/release procedures retain their owners |
| [Workspace ROADMAP](../../../.governance/workplaces/void-workplace/ROADMAP.md) and [TODO](../../../.governance/workplaces/void-workplace/TODO.md) | Current shared coordination parents, including transferred Engine 053-056, 060 and 061 |
| [Workspace catalog](../../../.governance/workplaces/void-workplace/docs/catalog/README.md) | Discovery of canonical project documents, without duplicate mutable status |

Inspector planners own its local work and contributions to shared parents.
Pinned fallback excerpts are non-authoritative context when the parent is absent;
they do not establish current workspace status or waive missing context.

## Use and integrate

| Guide | Contents |
|---|---|
| [Overview and packages](../README.md) | Core/view split and package entry points |
| [Core contract](modules/core/REFERENCE.md) | Discovery, binding, manual fields, options, errors and layout |
| [Flags and native masks](flags-editor.md) | Enum discovery, runtime names, multi-object edits and host-owned semantics |
| [Edit chains and UI locks](edit-chains.md) | Host configuration, displayed units, partial effects, lifecycle and view-parity limits |
| [Development](development.md) | Build, affected checks, packages, Git and known pitfalls |
| [Dependencies](../DEPENDENCIES.md) | Toolchain and package versions, ownership and candidates |

## Modules

Public API is supplied by the assemblies; references document each contract.
Manifests route source, dependencies and evidence without copying the symbol list.

| Boundary | Introduction | Reference | Manifest |
|---|---|---|---|
| Core | [Overview](modules/core/README.md) | [Contract](modules/core/REFERENCE.md) | [Routes](modules/core/MANIFEST.md) |
| ImGui | [Overview](modules/view-imgui/README.md) | [Contract](modules/view-imgui/REFERENCE.md) | [Routes](modules/view-imgui/MANIFEST.md) |
| Avalonia | [Overview](modules/view-avalonia/README.md) | [Contract](modules/view-avalonia/REFERENCE.md) | [Routes](modules/view-avalonia/MANIFEST.md) |
| WinForms | [Overview](modules/view-winforms/README.md) | [Contract](modules/view-winforms/REFERENCE.md) | [Routes](modules/view-winforms/MANIFEST.md) |
| WPF | [Overview](modules/view-wpf/README.md) | [Contract](modules/view-wpf/REFERENCE.md) | [Routes](modules/view-wpf/MANIFEST.md) |

## Sample hosts

| Host | Purpose |
|---|---|
| [ImGuiHost](../hosts/ImGuiHost/README.md) | Dear ImGui sample and renderer/context integration |
| [AvaloniaHost](../hosts/AvaloniaHost/README.md) | Avalonia sample |
| [WindowsHost](../hosts/WindowsHost/README.md) | WinForms sample |
| [WpfHost](../hosts/WpfHost/README.md) | WPF sample |
| [TuxHost](../hosts/TuxHost/README.md) | Console demonstration of the inspection tree and options |
| [TerminalHost](../hosts/TerminalHost/README.md) | Interactive console and reflection/binding dump |
| [DemoObjects](../hosts/DemoObjects/README.md) | Shared sample objects |

## Direction, work and history

| Document | Responsibility |
|---|---|
| [Roadmap](../ROADMAP.md) | Product direction, rationale, phase outcomes, acceptance criteria and guardrails |
| [TODO](../TODO.md) | Accepted active work, status, dependencies and gates |
| [TASKLIST](../TASKLIST.md) | Optional decomposition of large TODO entries |
| [Proposals](proposals/README.md) | Design origins and their individual promotion/outcome states |
| [History and evidence](history/README.md) | Completed outcomes, canonical evidence and remaining acceptance limits |
| [Legacy archive](archive/README.md) | Complete earlier chronology, imported rationale and original work snapshots |

## Documentation maintenance

| Document | Contents |
|---|---|
| [Policy](governance/documentation-policy.md) | Fact ownership, planning, closure, evidence and commit rules |
| [Schemas](schemas/README.md) | Structural vocabulary and historical derivation |
| [Phase template](templates/ROADMAP-ITEM.md) | Rationale, outcome, scope and criteria |
| [TODO template](templates/TODO-ENTRY.md) | Work and gates linked to a roadmap phase |
| [Task template](templates/TASKLIST-SECTION.md) | Child tasks without duplicating the parent |
| [Closure template](templates/CLOSURE-RECORD.md) | Preserved roadmap intent, outcome, evidence and Git baseline |
| [Other templates](templates/) | Module, proposal and legacy record shapes |
