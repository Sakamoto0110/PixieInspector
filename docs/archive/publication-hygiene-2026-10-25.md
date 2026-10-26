# Publication Hygiene

**Document ID:** ARCHIVE-PUBLICATION-HYGIENE-20261025

**Schema version:** 1

**Kind:** roadmap/status

**Lifecycle:** historical

**Subject:** generated packages ignored under the root artifacts directory, and the NoHost ignore rule described correctly

**Surface:** archive-record

**Boundary:** global

**Authority role:** evidence

**Mutation:** snapshot

**Indexing:** include

**Archive ID:** PXI-ARCHIVE-016

**Type:** fix

**Origin:** owner decision

**Author:** claude

**Reference date:** 2026-10-25

**Commits:** `1f8205f88a36626c80824ec92a009f3b5f277f9f`

**Current state:** [`docs/development.md`](../development.md)

Two corrections to publication hygiene.

`dotnet pack` writes packages and symbols to `artifacts/` at the repository
root, both in CI (`ci.yml`) and in the publish workflow (`publish.yml`), and
nothing ignored that directory, so a local pack left untracked `.nupkg` and
`.snupkg` files to be committed by accident. `.gitignore` now ignores
`/artifacts/`.

`docs/development.md` and `hosts/NoHost/README.md` said that the
`/hosts/NoHost` rule hides local changes and that `git add -u` stages them.
A `.gitignore` rule only hides untracked files: edits to NoHost's tracked
files still show in `git status`, and a bare `git add -u` stages every
tracked change in the repository. Both documents now say so, and the staging
example is scoped to `git add -u -- hosts/NoHost/`.

Verified: `git check-ignore -v` matches `artifacts/x.nupkg` to `/artifacts/`
and an untracked `hosts/NoHost/x.cs` to `/hosts/NoHost`; the directory matches
the `-o artifacts` of both workflows; the commit carries the owner's identity
as author and committer, with no attribution lines. No build was needed: the
commit changes no code or project file.
