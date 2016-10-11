namespace DiscoveryTests

open Discovery
open NUnit.Framework
open FsUnit

module Tests =

    let isNone value =
        match value with
        | Some s -> false
        | None -> true

    [<Test>]
    let ``Find Class`` () =
        isNone (Discovery.getType "System.Type") |> should equal false

    [<Test>]
    let ``Do Not Find Class`` () =
        (isNone (Discovery.getType "System.Nonsense")) |> should equal true

    [<Test>]
    let ``Find Subclasses`` () =
        let matches = Discovery.getSubclasses "System.Type"
        matches.IsEmpty |> should equal false

    [<Test>]
    let ``Do Not Find Subclasses`` () =
        let matches = Discovery.getSubclasses "System.Nonsense"
        matches.IsEmpty |> should equal true

    [<Test>]
    let ``Do Not Find Interfaces`` () =
        let matches = Discovery.getImplementations "System.Nonsense"
        matches.IsEmpty |> should equal true
