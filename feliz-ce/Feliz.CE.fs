namespace Feliz.CE

open System
open Feliz

type ReactHoC<'props> = ('props -> ReactElement) -> ReactElement

module ReactHoC =

  let zip (a : ReactHoC<'t>) (b : ReactHoC<'u>) : ReactHoC<'t * 'u> =
    fun next ->
      a
        (fun (t : 't) ->
          b
            (fun (u : 'u) ->
              next (t, u)))

  let bind (f : 't -> ReactHoC<'u>) (m : ReactHoC<'t>) : ReactHoC<'u> =
    fun next ->
      m
        (fun (t : 't) ->
          let n = f t
          n next)

  let map (f : 't -> 'u) (m : ReactHoC<'t>) : ReactHoC<'u> =
    fun next ->
      m
        (fun (t : 't) ->
          let u = f t
          next u)

  let bindReturn (f : 't -> ReactElement) (x : ReactHoC<'t>) : ReactElement =
    x f

  let render (el : ReactElement) (hoc : ReactHoC<unit>) : ReactElement =
    hoc (fun () -> el)

[<AutoOpen>]
module ComputationExpression =

  type ReactBuilder() =
    member this.MergeSources(a, b) =
      ReactHoC.zip a b

    member this.BindReturn(m, f) =
      ReactHoC.bindReturn f m

    member this.BindReturn(m, f) =
      ReactHoC.map f m

    member this.Return(x : ReactElement) =
      x

  let react = ReactBuilder()

[<RequireQualifiedAccess>]
module React =

  module InternalComponents =

    [<ReactComponent>]
    let WithState (initialState : 'state) (render : ('state * ('state -> unit)) -> ReactElement) =
      let state, setState = React.useState(initialState)
      render (state, setState)

    [<ReactComponent>]
    let WithRef (initialValue : 't) (render : IRefValue<'t> -> ReactElement) =
      let refValue = React.useRef(initialValue)
      render refValue

    [<ReactComponent>]
    let WithEffect (effect : unit -> #IDisposable) (deps : #seq<obj>) (render : unit -> ReactElement) =
      React.useEffect(effect, Array.ofSeq deps)
      render ()

  let withState (initialState : 't) : ReactHoC<'t * ('t -> unit)> =
    InternalComponents.WithState
      initialState

  let withRef (initialValue : 't) : ReactHoC<IRefValue<'t>> =
    InternalComponents.WithRef
      initialValue

  let withEffect (effect : unit -> #IDisposable) (deps : #seq<obj>) : ReactHoC<unit> =
    InternalComponents.WithEffect
      effect
      deps
