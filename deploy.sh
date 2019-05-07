#!/usr/bin/env sh

# abort on errors
set -e

cd Website

echo 'nightmareacademy.ru' > CNAME

git init
git add -A
git commit -m 'deploy'
git push -f git@github.com:Elringus/NightmareAcademy.git master:gh-pages

cd -

