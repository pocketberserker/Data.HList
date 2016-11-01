#r @"packages/build/FAKE/tools/FakeLib.dll"
#r @"packages/build/FAKE.Persimmon/lib/net451/FAKE.Persimmon.dll"
open Fake
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open System
open System.IO

let project = "Data.HList"

let testAssemblies = "test/**/bin/Release/net45/*Tests*.dll"

let outDir = "bin"

let gitOwner = "pocketberserker"
let gitHome = "https://github.com/" + gitOwner

let gitName = "Data.HList"

let gitRaw = environVarOrDefault "gitRaw" "https://raw.github.com/pocketberserker"

let release = LoadReleaseNotes "RELEASE_NOTES.md"

Target "Clean" (fun _ ->
  CleanDirs ["bin"]
)

Target "Build" (fun _ ->
  DotNetCli.Restore id

  !! "./**/project.json"
  |> DotNetCli.Build (fun p ->
    {
      p with
        Configuration = "Release"
    }
  )
)

Target "RunTests" (fun _ ->
  !! testAssemblies
  |> Persimmon id
)

Target "SetVersionInProjectJSON" (fun _ ->
  !! "./src/**/project.json"
  |> Seq.iter (DotNetCli.SetVersionInProjectJson release.NugetVersion)
)

Target "NuGet" (fun _ ->
  !! "src/**/project.json"
  |> DotNetCli.Pack (fun p ->
    {
      p with
        OutputPath = outDir
    }
  )
)

Target "PublishNuget" (fun _ ->
  Paket.Push(fun p ->
    { p with
        WorkingDir = "bin" })
)

#load "paket-files/build/fsharp/FAKE/modules/Octokit/Octokit.fsx"
open Octokit

Target "Release" (fun _ ->
  StageAll ""
  Git.Commit.Commit "" (sprintf "Bump version to %s" release.NugetVersion)
  Branches.push ""

  Branches.tag "" release.NugetVersion
  Branches.pushTag "" "origin" release.NugetVersion

  // release on github
  createClient (getBuildParamOrDefault "github-user" "") (getBuildParamOrDefault "github-pw" "")
  |> createDraft gitOwner gitName release.NugetVersion (release.SemVer.PreRelease <> None) release.Notes
  |> releaseDraft
  |> Async.RunSynchronously
)

Target "BuildPackage" DoNothing

Target "All" DoNothing

"Clean"
  ==> "SetVersionInProjectJSON"
  ==> "Build"
  ==> "RunTests"
  ==> "All"

"All"
  ==> "NuGet"
  ==> "BuildPackage"

"BuildPackage"
  ==> "PublishNuget"
  ==> "Release"

RunTargetOrDefault "All"
