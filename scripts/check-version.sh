#!/usr/bin/env bash
# Fails if the cache-busting ?v= tokens in index.html drift from APP_VERSION in
# version.js. version.js is the single source of truth for the web version; this
# guard exists because the tokens live in HTML attributes and cannot reference it.
set -euo pipefail

cd "$(dirname "$0")/.."

version=$(grep -oE "APP_VERSION = '[^']+'" web/version.js | grep -oE "'[^']+'" | tr -d "'")
if [ -z "$version" ]; then
  echo "check-version: could not read APP_VERSION from web/version.js" >&2
  exit 1
fi

status=0
while read -r token; do
  if [ "$token" != "$version" ]; then
    echo "check-version: index.html has ?v=$token but version.js says $version" >&2
    status=1
  fi
done < <(grep -oE '\?v=[0-9]+\.[0-9]+\.[0-9]+' web/index.html | sed 's/?v=//')

badge=$(grep -oE 'id="versionBadge">v[0-9]+\.[0-9]+\.[0-9]+' web/index.html | grep -oE '[0-9]+\.[0-9]+\.[0-9]+' || true)
if [ -n "$badge" ] && [ "$badge" != "$version" ]; then
  echo "check-version: version badge fallback says v$badge but version.js says $version" >&2
  status=1
fi

if [ "$status" -eq 0 ]; then
  echo "check-version: OK (web v$version)"
fi
exit "$status"
