#! /bin/sh

# Sonarcloud commands that we need to run in the project's folder.

key="Brain-Control"
organization="graygzou-github"
sources="."
url="https://sonarcloud.io"
login="33b57f9ed8df1d097109d905a34ad1fc784366cd"

echo "Execute the sonarcloud scanner from the computer"
sonar-scanner \
  -Dsonar.projectKey=$key \
  -Dsonar.organization=$organization \
  -Dsonar.sources=$sources \
  -Dsonar.host.url=$url \
  -Dsonar.login=$login
