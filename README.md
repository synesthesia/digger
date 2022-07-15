# Digger

A tool to pull annotations from the Diigo social links platform and generate markdown files

## Caution

You are welcome to use this tool yourself, however as .Net tools run with full trust you should read the source code and satisfy yourself that it is safe to do so on your machine.

## Installing

1. Ensure you have [dotnet SDK installed](https://dotnet.microsoft.com/en-us/download/dotnet)
2. run `dotnet tool add digger -g`
3. Obtain your [Diigo API key](https://www.diigo.com/api_keys/new/)
4. Configure [Environment variables](#environment-variables)
5. run the tool `digger` (see [Using](#using))

## Environment Variables

Digger requires the following environment variables to be set:

|Key|Comment
|----|----|
|DIIGO__APIKEY|Diigo API key|
|DIIGO__USERNAME|Diigo Username|
|DIIGO__PASSWORD|Diigo Password|

## Using

1. make sure that any Diigo entries you wish to process are tagged `#toprocess`
2. run `digger diigo -o <relative path to my output directory`>
3. edit the resulting files to your taste in the editor of your choosing
4. that's it!



## Building

- clone this repo
- run `yarn install`
- install VS Code, open working copy
- install all recommended extensions
- install [Versionize](https://github.com/versionize/versionize)
- make changes on a branch, commit using conventional commits

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


