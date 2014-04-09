﻿namespace AST

[<AbstractClass>]
type InterfaceMember(isStatic : bool, name : string, pos : Position) =
    inherit Node(pos)
    member x.Name     = name
    member x.IsStatic = isStatic

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

[<AbstractClass>]
type InterfaceMethod(isStatic : bool, name : string, parameters : FormalParameter list, pos : Position) =
    inherit InterfaceMember(isStatic, name, pos)
    member x.Parameters = parameters

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

type InterfaceReturnMethod(isStatic : bool, returnType : Type, name : string, 
                           parameters : FormalParameter list, pos : Position) =
    inherit InterfaceMethod(isStatic, name, parameters, pos)
    member x.ReturnType = returnType

    override x.ToString() = 
        let staticStr = if isStatic then "static " else ""
        let parametersStr = parameters |> List.map string |> String.concat ", " |> sprintf "(%s)"
        sprintf "%s%A %s%s;" staticStr returnType name parametersStr

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

type InterfaceVoidMethod(isStatic : bool, name : string, parameters : FormalParameter list, pos : Position) =
    inherit InterfaceMethod(isStatic, name, parameters, pos)

    override x.ToString() = 
        let staticStr     = if isStatic then "static " else ""
        let parametersStr = parameters |> List.map string |> String.concat ", " |> sprintf "(%s)"
        sprintf "%svoid %s%s;" staticStr name parametersStr

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

type InterfaceField(isStatic : bool, isFinal : bool, fieldType : Type, name : string, body : Expression, pos : Position) =
    inherit InterfaceMember(isStatic, name, pos)
    member x.Type    = fieldType
    member x.IsFinal = isFinal
    member x.Body    = body
    
    override x.ToString() = 
        let staticStr = if isStatic then "static " else ""
        let finalStr  = if isStatic then "final "  else ""
        sprintf "%s%s%A %s = %A;" staticStr finalStr fieldType name body
