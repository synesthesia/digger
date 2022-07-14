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

## Acknowledgements

This tool relies heavily on trhe following third-party libraries (aside from .Net and various standard testing libraries)

|Library|Source|Author(s)|Licence|
|----|----|----|----|
|markdown-generator|[ap0llo/markdown-generator](https://github.com/ap0llo/markdown-generator)|[Andreas Grünwald](https://github.com/ap0llo) and others|[MIT](https://opensource.org/licenses/MIT)|
|CommandLineParser|[commandlineparser/commandline](https://github.com/commandlineparser/commandline)|[Giacomo Stelluti Scala](https://github.com/gsscoder) & Contributors|[MIT](https://opensource.org/licenses/MIT)|
|Html2Markdown|[baynezy/Html2Markdown](https://github.com/baynezy/Html2Markdown)|[Simon Baynes](https://github.com/baynezy)|[Apache Licence 2.0](https://opensource.org/licenses/Apache-2.0)|


