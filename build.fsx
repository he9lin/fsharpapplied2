// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"
open Fake
// Directories
let buildDir  = "./build/"
// Filesets
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

let noFilter = fun _ -> true

// Targets
Target "Views" (fun _ ->
    let srcDir = "./src/FsTweet.Web/views"
    let targetDir = combinePaths buildDir "views"
    CopyDir targetDir srcDir noFilter
)

Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

Target "Run" (fun _ ->
    ExecProcess
        (fun info -> info.FileName <- "./build/FsTweet.Web.exe")
        (System.TimeSpan.FromDays 1.)
    |> ignore
)

// Build order
"Clean"
  ==> "Build"
  ==> "Views"
  ==> "Run"

// start build
RunTargetOrDefault "Build"
