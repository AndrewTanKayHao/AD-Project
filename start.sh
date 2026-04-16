#!/bin/bash
set -e

echo "Starting ADWebApplication..."
exec dotnet ADWebApplication.dll
