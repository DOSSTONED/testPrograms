#!/bin/bash
if [ "$1" = "" ]
then
  echo "No date param provided. Example:"
  echo "$0 \"Sun, 1 Mar 2009 00:00:00 +0800\""
else
  GIT_COMMITTER_DATE="$1" git commit --date "$1"
fi
