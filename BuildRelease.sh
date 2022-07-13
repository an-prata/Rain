#!/bin/bash

dotnet restore
dotnet tool restore

dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-Generic-x64" --self-contained --runtime "win-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-Generic-x86" --self-contained --runtime "win-x86"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-Generic-arm64" --self-contained --runtime "win-arm64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-Generic-arm" --self-contained --runtime "win-arm"

dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-10-x64" --self-contained --runtime "win10-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-10-x86" --self-contained --runtime "win10-x86"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-10-arm64" --self-contained --runtime "win10-arm64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Windows-10-arm" --self-contained --runtime "win10-arm"

dotnet build "Rain.Engine" --configuration Release --output "Build\Release\macOS-Generic-x64" --self-contained --runtime "osx-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\macOS-10.15-Catalina-x64" --self-contained --runtime "osx.10.15-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\macOS-11.0-Big-Sur-x64" --self-contained --runtime "osx.11.0-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\macOS-11.0-Big-Sur-arm64" --self-contained --runtime "osx.11.0-arm64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\macOS-12.0-Monterey-x64" --self-contained --runtime "osx.12-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\macOS-12.0-Monterey-arm64" --self-contained --runtime "osx.12-arm64"

dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Linux-x64" --self-contained --runtime "linux-x64"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Linux-arm" --self-contained --runtime "linux-arm"
dotnet build "Rain.Engine" --configuration Release --output "Build\Release\Linux-arm64" --self-contained --runtime "linux-arm64"

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../TestCoverage/lcov.info