# COMMANDS #

### WHENEVER YOU CREATE A NEW BRANCH AND PUSH IT YOU HAVE TO DO THIS
git push -u origin ENTERBRANCHNAMEHERE

### How to see file changes and check which branch you are on
git status

### How to add files to the list of files that you want to save to the remote repository
git add .

### How to save your changes to files locally
git commit -m "Insert message here"

### How to send your files to the git remote repository
git push


### How to sync your local branchds with the remote repositories branches
git fetch

### How to update your local files from the git server
git pull

### How to switch branches
git checkout InsertBranchToSwitchToHere

### How to create a new branch
git checkout -b InsertBranchNameHere

### How to switch to the master branch
git checkout master

## How to save your password in git (windows) Only do this once
git config --global credential.helper wincred

## FOR DIVERGED PROBLEMS
git reset --hard origin

## For resetting back to a commit on remote DONT USE THIS ONE, only for RYAN
git reset --hard <commit-hash>
git push -f origin master

## How to remove conflicted file
git update-index --assume-unchanged FILEORFOLDER