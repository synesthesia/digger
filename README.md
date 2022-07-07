# Digger

A tool to pull annotations from the Diigo social links platform and generate markdown files

<!-- markdownlint-disable-next-line MD036-->
**Not production ready**

## Installing

1. Ensure you have [dotnet SDK installed](https://dotnet.microsoft.com/en-us/download/dotnet)
2. run `dotnet tool add digger -g`
3. Configure Environment variables
4. run the tool `digger` (details on command line options to follow)

## Using

To follow....

### Environment Variables

Digger requires the following environment variables to be set:

|Key|Comment
|----|----|
|DIIGO__APIKEY|Diigo API key|
|DIIGO__USERNAME|Diigo Username|
|DIIGO__PASSWORD|Diigo Password|

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
