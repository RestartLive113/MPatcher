#!/bin/sh
set -eu
installer_dir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd)
if ! command -v python3 >/dev/null 2>&1; then
    printf '%s\n' 'MPatcher: Python 3.10+ is required. Arch: sudo pacman -Syu python tk' >&2
    exit 1
fi
exec python3 "$installer_dir/mpatcher_linux.py" "$@"
