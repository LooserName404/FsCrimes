type IProduct =
    abstract Operation: unit -> string

type ConcreteProduct1() =
    interface IProduct with
        override _.Operation() = "{Result of ConcreteProduct1}"

type ConcreteProduct2() =
    interface IProduct with
        override _.Operation() = "{Result of ConcreteProduct2}"

[<AbstractClass>]
type Creator() =
    abstract FactoryMethod: unit -> IProduct

    member this.SomeOperation () =
        let product = this.FactoryMethod()
        let result = "Creator: The same creator's code has just worked with " + product.Operation()

        result

type ConcreteCreator1() =
    inherit Creator()

    override _.FactoryMethod() =
        ConcreteProduct1()

type ConcreteCreator2() =
    inherit Creator()

    override _.FactoryMethod() =
        ConcreteProduct2()

let clientCode (creator: Creator): unit =
    printfn "Client: I'm not aware of the creator's class, but it still works.\n%s" (creator.SomeOperation())

printfn "App: Launched with the ConcreteCreator1."
clientCode (ConcreteCreator1())

printfn ""

printfn "App: Launched with the ConcreteCreator2."
clientCode (ConcreteCreator2())
