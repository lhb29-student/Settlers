THE SETTLERS
======

## Product Logo
## Description

## Instructions to create a branch

First, you must create your branch locally
> git checkout -b your_branch

After that, you can work locally in your branch, when you are ready to share the branch, push it. The next command push the branch to the remote repository origin and tracks it
> git push -u origin your_branch

Teammates can reach your branch, by doing:
> git fetch
> git checkout origin/your_branch

You can continue working in the branch and pushing whenever you want without passing arguments to git push (argumentless git push will push the master to remote master, your_branch local to remote your_branch, etc...)
> git push

Teammates can push to your branch by doing commits and then push explicitly
> ... work ...
> git commit
> ... work ...
> git commit
> git push origin HEAD:refs/heads/your_branch

Or tracking the branch to avoid the arguments to git push
>git checkout --track -b your_branch origin/your_branch
>... work ...
>git commit
>... work ...
>git commit
>git push
