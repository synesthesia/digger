# Digger

A tool to pull annotations from the Diigo social links platform and generate markdown files

## Badges

[![NuGet version (Digger)](https://img.shields.io/nuget/vpre/Digger.svg?style=flat-square)](https://www.nuget.org/packages/Digger/)

[![Issues (Digger)](https://img.shields.io/github/issues/synesthesia/digger?style=flat-square)](https://github.com/synesthesia/digger/issues)

[![Pull Requests (Digger)](https://img.shields.io/github/issues-pr/synesthesia/digger?style=flat-square)](https://github.com/synesthesia/digger/pulls)

## Caution

You are welcome to use this tool yourself, however as .Net tools run with full trust you should read the source code and satisfy yourself that it is safe to do so on your machine.

## Installing

1. Ensure you have [dotnet SDK installed](https://dotnet.microsoft.com/en-us/download/dotnet)
2. run `dotnet tool add -g digger`
3. Obtain your [Diigo API key](https://www.diigo.com/api_keys/new/)
4. Configure [Environment variables](#environment-variables)
5. run the tool `digger` (see [Using](#using))

## Updating to latest version

1. run `dotnet tool update -g digger`

## Removing from your system

1. run `dotnet tool uninstall -g  digger`

## Environment Variables

Digger requires the following environment variables to be set:

|Key|Comment
|----|----|
|DIIGO__APIKEY|Diigo API key|
|DIIGO__USERNAME|Diigo Username|
|DIIGO__PASSWORD|Diigo Password|
|HYPOTHESIS__APITOKEN|Hypothesis API Token|

## Using

1. make sure that any Diigo entries you wish to process are tagged `#toprocess`
2. run `digger diigo -o <relative path to my output directory`>
3. for each entry processed a file will be created in the directory you nominated
4. Diigo entries that have been processed will have tag `#toprocess` removed and tag `#processed` added
5. edit the resulting files to your taste in the editor of your choosing
6. that's it!

## Building

- clone this repo
- run `yarn install`
- install VS Code
- open working copy
- install all recommended extensions
- install [Versionize](https://github.com/versionize/versio nize)
- make changes on a branch
- commit using [conventional commits](https://www.conventionalcommits.org/en/v1.0.0/)

## Install from local copy during development

If you want to test a build locally:

`dotnet tool uninstall -g digger`

And then from root of working copy, after build:

`dotnet tool install digger -g --prerelease --add-source ./nupkg`

## Releasing

- assumes you have [nuget installed and configured](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools) on your machine [with an API key](https://docs.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-setapikey)
- `git push` from branch
- create Pull Request
- review and squash merge
- `git checkout master`
- `git pull origin master`
- run `versionize`
- `git push --follow-tags origin master`
- `dotnet pack`
- `dotnet nuget push`

## Contributing

This tool is built first and foremost to solve some irritations I had in my own personal knowledge management processes. However, contributions are welcome, please open an issue first to discuss before creating a pull request.

Please follow the standard [Github "contribution by forking"](https://docs.github.com/en/get-started/quickstart/contributing-to-projects) process.

## License

This project is licenced under the [MIT Licence](./LICENSE)

## Acknowledgements

Initial code to communicate with the Diigo API refactored from [DiigoSharp](https://github.com/aforank/DiigoSharp), copyright [Ankit Sharma](https://github.com/aforank), [MIT licence](https://opensource.org/licenses/MIT).

This tool also relies heavily on the following third-party libraries (aside from .Net and various standard testing libraries)

|Library|Source|Author(s)|Licence|
|----|----|----|----|
|markdown-generator|[ap0llo/markdown-generator](https://github.com/ap0llo/markdown-generator)|[Andreas Grünwald](https://github.com/ap0llo) and others|[MIT](https://opensource.org/licenses/MIT)|
|CommandLineParser|[commandlineparser/commandline](https://github.com/commandlineparser/commandline)|[Giacomo Stelluti Scala](https://github.com/gsscoder) & Contributors|[MIT](https://opensource.org/licenses/MIT)|
|Html2Markdown|[baynezy/Html2Markdown](https://github.com/baynezy/Html2Markdown)|[Simon Baynes](https://github.com/baynezy)|[Apache Licence 2.0](https://opensource.org/licenses/Apache-2.0)|
