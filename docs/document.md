# Functional Requirements
## Program
- C#/.Net Core Application that can be run via the command-line.
- Should take as parameters:
    - The path to a local repository.
    - The mode the program should run.
- Should be able to run in two modes:
    - 'Frequency' mode prints a list of the number of commits a day and the date of those commits.
    - 'Commit auhor' mode prints the name of each author and a list of the number of commits a day and the date for those commits for that author.
## Development
- Use the 'libgit2sharp' library to collect git repository data.
- Create one or more Github Action workflows.
- workflow(s) should build and test your code.
- Workflow(s) shoul be triggered on every push/pull requests to the main branch.
# GitInsight Use-Cases
