type IAbstractProductA =
    abstract UsefulFunctionA: unit -> string

type ConcreteProductA1() =
    interface IAbstractProductA with
        member _.UsefulFunctionA() = "The result of the product A1."

type ConcreteProductA2() =
    interface IAbstractProductA with
        member _.UsefulFunctionA() = "The result of the product A2."

type IAbstractProductB =
    abstract UsefulFunctionB: unit -> string
    abstract AnotherUsefulFunctionB: IAbstractProductA -> string

type ConcreteProductB1() =
    interface IAbstractProductB with
        member _.UsefulFunctionB() = "The result of the product B1."

        member _.AnotherUsefulFunctionB collaborator =
            $"The result of B1 collaborating with ({collaborator.UsefulFunctionA()})"

type ConcreteProductB2() =
    interface IAbstractProductB with
        member _.UsefulFunctionB() = "The result of the product B2."
        
        member _.AnotherUsefulFunctionB collaborator =
            $"The result of B2 collaborating with ({collaborator.UsefulFunctionA()})"

type IAbstractFactory =
    abstract CreateProductA: unit -> IAbstractProductA
    abstract CreateProductB: unit -> IAbstractProductB

type ConcreteFactory1() =
    interface IAbstractFactory with
        member _.CreateProductA(): IAbstractProductA = ConcreteProductA1()
        member _.CreateProductB(): IAbstractProductB = ConcreteProductB1()

type ConcreteFactory2() =
    interface IAbstractFactory with
        member _.CreateProductA(): IAbstractProductA = ConcreteProductA2()
        member _.CreateProductB(): IAbstractProductB = ConcreteProductB2()

let clientCode (factory: IAbstractFactory) =
    let productA = factory.CreateProductA()
    let productB = factory.CreateProductB()

    printfn "%s" (productB.UsefulFunctionB())
    printfn "%s" (productB.AnotherUsefulFunctionB productA)

printfn "Client: Testing client code with the first factory type..."
clientCode (ConcreteFactory1())

printfn ""

printfn "Client: Testing the same client code with the second factory type..."
clientCode (ConcreteFactory2())