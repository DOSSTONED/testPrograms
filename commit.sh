#!/bin/bash
if [ "$1" = "" ]
then
  echo "No date param provided. Example:"
  echo "$0 \"2009-03-01 08:00\" [other git params]"
else
  dat=$(date --date "$1" -R)
  # Shift one place, remaining params are transferred to git
  shift
  GIT_COMMITTER_DATE="$dat" git commit --date "$dat" "$@"
fi
