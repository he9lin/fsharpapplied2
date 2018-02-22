module FsTweetWeb.Main

open Suave
open Suave.DotLiquid
open System.IO
open System.Reflection
open Suave.Successful
open Suave.Operators
open Suave.Filters

let currentPath =
  Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)

let initDotLiquid () =
  let templatesDir = Path.Combine(currentPath, "views")
  setTemplatesDir templatesDir

[<EntryPoint>]
let main argv =
  initDotLiquid ()
  setCSharpNamingConvention ()

  let app =
    path "/" >=> page "guest/home.liquid" ""
  startWebServer defaultConfig app
  0
