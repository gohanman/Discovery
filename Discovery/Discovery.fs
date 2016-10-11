namespace Discovery

open System

module Discovery =
    
    let assemblies = AppDomain.CurrentDomain.GetAssemblies()
    let types = assemblies 
                    |> Array.toList
                    |> List.map (fun i -> i.GetTypes())
                    |> List.map (fun i -> Array.toList i)
                    |> List.concat

    let getType (name:string) =
        types |> List.tryFind (fun i -> i.FullName = name)

    let getSubclasses (name:string) =
        let actualType = getType name
        match actualType with
        | None -> []
        | Some t -> types |> List.filter (fun i -> i.IsSubclassOf(t))

    let implements (someType:Type) (iface:Type) =
        someType.GetInterfaces()
        |> Array.toList
        |> List.exists (fun i -> i = iface)

    let getImplementations (name:string) =
        let actualType = getType name
        match actualType with
        | None -> []
        | Some t -> types |> List.filter (fun i -> implements i t)

