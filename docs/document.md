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
## Use case no. 1

- **Name:** Find out if a Repository is currently being worked on

    - **Goal, Description, Delianation:** 
        - The user wants to find out how many *Commits* that have happened on the current repository and who is working or not by using our *Program* amd as such need to interact with their prefered *Command Prompt*.
        - An example for this could be that the user is working on a repository and is unsure whether his fellow co-workers are working on it as well.

    - **Initiating actor:** The User.
    - **Initiating action:** The user has to find out whether people are working on the repository by using the command-line to run our program.  
    - **Starting conditons:** The user no knowledge about the insights of the repository but has knowledge how to use commands using the Command Prompt.
- **Main course:** 
    - The user opens the root folder of our project using their prefered Command Prompt. 
    - They change the directory into *GitInsight*.
    - By typing either *--frequency* or *--author* while running the program they get their prefered output.
    - If they type *--frequency* they get the number of **Commits** per day.
    - If they type *--author* they get number of commits per day grouped by who commits it. 
- **End result:**
    - The user was able to find out how frequent and who is working on the repository. 
- **End condition:** 
    - The *Program* terminates and is ready for use again.