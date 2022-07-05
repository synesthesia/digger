# Digger

A tool to pull annotations from the Diigo social links platform and generate markdown files

## Environment Variables

Digger will use the following environment variables if found

|Key|Comment
|----|----|
|DIIGO_KEY|Diigo API key|

## Building

- clone this repo
- run `yarn install`
- install VS Code, open working copy
- isntall all recommended extensions
- install [Versionize](https://github.com/versionize/versionize)
- make changes on a branch, commit using conventional commits

## Releasing

- `git push` from branch
- create Pull Request
- review and squash merge
- `git checkout master; git pull origin master`
- run `versionize`
- `git push --follow-tags origin master`
- `dotnet pack`
- `dotnet nuget push`
