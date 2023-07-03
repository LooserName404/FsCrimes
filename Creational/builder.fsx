open System
open System.Collections.Generic

type IBuilder =
    abstract BuildPartA: unit -> unit
    abstract BuildPartB: unit -> unit
    abstract BuildPartC: unit -> unit

type Product() =
    let _parts = List<obj>()

    member _.Add (part: string) = _parts.Add part

    member _.ListParts () =
        let mutable str = String.Empty

        for i = 0 to _parts.Count - 1 do
            str <- str + _parts[i].ToString() + ", "
        
        str <- str.Remove(str.Length - 2)

        $"Product parts: {str}\n"

type ConcreteBuilder() as this =
    let mutable _product = Product()

    do
        this.Reset()
        
    member _.Reset() = _product <- Product()

    member _.GetProduct() =
        let result = _product
        this.Reset()
        result

    interface IBuilder with
        member _.BuildPartA() = _product.Add "PartA1"
        member _.BuildPartB() = _product.Add "PartB1"
        member _.BuildPartC() = _product.Add "PartC1"

type Director() =
    let mutable _builder: IBuilder = Unchecked.defaultof<_>

    member _.Builder with set value = _builder <- value

    member _.BuildMinimalViableProduct() = _builder.BuildPartA()

    member _.BuildFullFeaturedProduct() =
        _builder.BuildPartA()
        _builder.BuildPartB()
        _builder.BuildPartC()

let director = Director()
let builder = ConcreteBuilder()
director.Builder <- builder

printfn "Standard basic product:"
director.BuildMinimalViableProduct()
printfn "%s" (builder.GetProduct().ListParts())

printfn "Standard full featured product:"
director.BuildFullFeaturedProduct()
printfn "%s" (builder.GetProduct().ListParts())

printfn "Custom product:"
(builder :> IBuilder).BuildPartA()
(builder :> IBuilder).BuildPartC()
printfn "%s" (builder.GetProduct().ListParts())
