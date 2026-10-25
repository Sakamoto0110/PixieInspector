# NoHost

The owner's local test console (`net10.0-windows`). It prints trees of the demo
objects the way the TuxHost does, and the owner changes it freely.

It stays versioned and in the solution. Its `.gitignore` rule ignores new,
untracked files in this directory; edits to its tracked files remain visible
in `git status`. To stage changes to those tracked files, use
`git add -u -- hosts/NoHost/`.

## Build and run

```bash
dotnet run --project hosts/NoHost/NoHost.csproj
```

It needs Windows, or `dotnet exec --runtimeconfig` on another system, since it
touches neither WinForms nor WPF.
