# PixieInspector

A reflection-based object inspector for .NET: point it at a type, bind one or
more objects, and it builds a tree of the members, reads and writes them, and
reports what it could not do. The core knows no UI framework; a view per
framework shows the tree and edits it. The independent PixieEditor is one
consumer; Engine-specific fields, scene history and SDK selection remain
host-owned.

Early stage. The core and each view are a package on nuget.org, all five at
one version; a consumer takes the view it shows the inspector with, and the
core and PixieLib come with it:

| Package | Target | What it is |
|---|---|---|
| `PixieInspector` | `net10.0` | The core |
| `PixieInspector.ImGui` | `net10.0` | The Dear ImGui view, on Hexa.NET.ImGui; the host must use the same binding and version |
| `PixieInspector.Avalonia` | `net10.0` | The Avalonia view |
| `PixieInspector.WinForms` | `net10.0-windows` | The WinForms view |
| `PixieInspector.Wpf` | `net10.0-windows` | The WPF view |

```bash
dotnet add package PixieInspector.ImGui
```

For source builds and sample hosts, see the [development guide](docs/development.md).
The [core reference](docs/modules/core/REFERENCE.md) owns the documented API
contract; the [module index](docs/README.md#modules) routes each view.

View capabilities can differ. The [flags guide](docs/flags-editor.md) and
[edit-chain guide](docs/edit-chains.md) describe host integration, manual
locks, gestures and the current view-parity and release limits.

## Layout

| Path | What it is |
|---|---|
| `PixieInspector/` | The core: the inspector, its nodes, options, binding, and reports (`net10.0`) |
| `views/` | One project per UI framework; see the module index for each contract |
| `hosts/` | Sample applications, one per view and two consoles, and the objects they inspect |
| `docs/` | Documentation |

A consumer takes three assemblies: the core, one view, and PixieLib, which
supplies the primitives.

## Documentation

- [`ROADMAP.md`](ROADMAP.md): direction, rationale, phase outcomes, criteria and guardrails.
- [`TODO.md`](TODO.md): active work. [`TASKLIST.md`](TASKLIST.md): optional decomposition.
- [`DEPENDENCIES.md`](DEPENDENCIES.md): every dependency, its status, and candidates.
- [`docs/`](docs/README.md): documentation index, the module references, the
  development guide, proposals and [recorded outcomes](docs/history/README.md).

MIT license; see [`LICENSE`](LICENSE).
