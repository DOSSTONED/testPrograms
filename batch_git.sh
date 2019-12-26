#!/bin/bash

function CreateBranch()
{
  # Create and change to new branch
  git branch "$1" || exit 2
  git checkout "$1" || exit 3
}

function CheckBranchExists()
{
  ori_name="$1"
  # Substitute space with dash
  name="${ori_name// /_}"
  exists=$(git show-ref refs/heads/$name)
  if [ "$exists" != "" ]
  then
    echo "  branch exists: $name"
    git checkout "$name" || exit 4
  else
    echo "  branch not exist: $name, creating one"
    CreateBranch "$name"
  fi
}

LAST_DT=""
function GetLastModifiedTime()
{
  # Already pushd, so we are working in current dir.
  dir="."
  # Get oldest file
  file=$(ls -tr "$dir" | head -n1)
  [ $? == 0 ] || exit 11
  # Get the time
  dt=$(stat -c %y "$dir/$file")
  [ $? == 0 ] || exit 12
  # Print
  echo "  last time: $dt, $dir, $file"
  LAST_DT="$dt"
}

function ConvertGb2312ToUtf8()
{
  file="$1"
  echo "  " iconv -f "gb2312" -t "UTF-8" "$file" -o "$file.utf8" || (echo "cannot convert" && exit 13)
}

function CheckAndConvertGb2312Folder()
{
  # file variable is in sub-shell, so does not change current file var
  find "." -type f | while read file;
  do
    charset=$(uchardet "$file")
    # Cannot get charset, abnormal exit
    [ $? == 0 ] || (echo "cannot get charset" && exit 14)
    echo "  charset $charset -- $file"
    if [[ $charset == gb* ]]
    then
      ConvertGb2312ToUtf8 "$file"
    fi
  done
}

# Get current branch
curBranch=$(git rev-parse --abbrev-ref HEAD)

for folder in */
do
  # change back to master branch
  git checkout master || exit 1

  # Remove last slash
  folder=${folder%%/}
  echo "Checking ... $folder"
  CheckBranchExists "$folder"
  # Now we switched to seprate branch.
  # Next enter the folder and add files.
  pushd "$folder" || exit 50
  # Check the last modified date of the file, find the oldest one.
  GetLastModifiedTime "$folder"
  # More todo:
  # Check each file whether it is in GB2312/GB18030, and convert the codec into UTF-8
  CheckAndConvertGb2312Folder "$folder"
  
  echo "current dir: $PWD" >&2
  lastCommitMsg=$(git log -1 --pretty=%B)
  # If we already commited with auto committer, do nothing
  if [[ $lastCommitMsg = Auto\ init\ commit\ for\ project*  ]] || [[ "$lastCommitMsg" = "Add utf8 text"  ]]
  then
    echo "Already automatically committed, do nothing."
  else
    # Finally add all files in current folder.
    git add . || exit 52
    # Using custom commit script to commit with modified date.
    commit.sh "$LAST_DT" --message "Auto init commit for project $folder" || exit 53
  fi
  # Return master branch
  git checkout master || exit 54
  # Exit the dir
  popd || exit 51

  # something wrong with Begin folder, but sleep solved problem?
  echo "Now finished $folder, enter anything to go next." >&2 && sleep 1
done

# Finally switch back
git checkout master || exit 100