#!/usr/bin/env pwsh
# Merge openapi files
cd openapi
$paths = Get-ChildItem -Path "*.y*ml" -Recurse
cd ..
yq eval-all '. as $item ireduce ({}; . * $item )' $paths.FullName > openapi.yaml

# Generate client code
kiota generate -d openapi.yaml -l CSharp --exclude-backward-compatible -o ./Fegmm.Elvanto/Generated -c ElvantoClient -n Fegmm.Elvanto --clean-output --ll Information