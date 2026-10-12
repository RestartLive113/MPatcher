#!/usr/bin/env python3
"""Package only the native installer sources; no game/payload/UserData files."""
import argparse
import hashlib
import io
import json
from pathlib import Path
import tarfile

ROOT = Path(__file__).resolve().parents[1]
NAME = "MPatcherInstaller-Linux-0.1.2-Alpha"


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--output", type=Path, default=ROOT / "dist")
    parser.add_argument("--notices", type=Path, default=ROOT / "Installer/THIRD-PARTY-NOTICES.txt")
    args = parser.parse_args()
    output = args.output.resolve()
    output.mkdir(parents=True, exist_ok=True)
    archive = output / (NAME + ".tar.gz")
    checksums = output / (NAME + ".sha256")
    report = output / (NAME + ".json")
    if any(path.exists() for path in (archive, checksums, report)):
        parser.error("output already exists; preserving previous artifacts")
    source = ROOT / "Installer/Linux"
    files = {name: (source / name).read_bytes() for name in ("mpatcher_linux.py", "MPatcherInstaller.sh", "README.ru.txt", "README.en.txt", "README.ja.txt")}
    files["THIRD-PARTY-NOTICES.txt"] = args.notices.read_bytes()
    # Scripts must have Unix line endings even when packaged on Windows.
    for name in files:
        files[name] = files[name].replace(b"\r\n", b"\n")
    sums = {name: hashlib.sha256(data).hexdigest() for name, data in files.items()}
    files["SHA256SUMS.txt"] = ("\n".join(sums[name] + "  " + name for name in sorted(sums)) + "\n").encode()
    with archive.open("xb") as target:
        with tarfile.open(fileobj=target, mode="w:gz") as tar:
            for name, data in sorted(files.items()):
                info = tarfile.TarInfo(NAME + "/" + name)
                info.size = len(data)
                info.mode = 0o755 if name.endswith((".sh", ".py")) else 0o644
                info.mtime = 1791752400  # 2026-10-12 00:00 MSK
                tar.addfile(info, io.BytesIO(data))
    with tarfile.open(archive, "r:gz") as tar:
        for member in tar.getmembers():
            name = Path(member.name).name
            if not member.isfile() or tar.extractfile(member).read() != files[name]:
                raise RuntimeError("archive verification failed")
    digest = hashlib.sha256(archive.read_bytes()).hexdigest()
    checksums.write_text(digest + "  " + archive.name + "\n", encoding="utf-8")
    result = {"artifact": str(archive), "sha256": digest, "bytes": archive.stat().st_size,
              "contents": sorted(files), "sourceHashes": sums, "runtimeAcceptance": False}
    report.write_text(json.dumps(result, indent=2), encoding="utf-8")
    print(json.dumps(result, indent=2))


if __name__ == "__main__":
    main()
