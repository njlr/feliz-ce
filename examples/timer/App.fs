module App

open System
open Fable.Core
open Feliz
open Feliz.CE

module React =

  [<ReactComponent>]
  let Timer (isStarted : bool) (render : _ -> ReactElement) =
    let count, updateCount = React.useStateWithUpdater 0
    let isRunning, setRunning = React.useState isStarted

    let startTimer =
      (fun () ->
        let subscriptionID =
          JS.setInterval (fun _ -> if isRunning then updateCount ((+) 1)) 1000

        {
          new IDisposable with
            member this.Dispose() =
              JS.clearTimeout(subscriptionID)
        })

    React.useEffect(startTimer, [| box isRunning |])

    let reset () =
      updateCount (fun _ -> 0)

    render (count, reset, isRunning, setRunning)

  let withTimer isStarted =
    Timer isStarted

let stopwatch =
  react {
    let! seconds, reset, isRunning, setRunning = React.withTimer true

    return
      Html.div
        [
          Html.h1 $"Timer: {seconds}s"
          Html.button
            [
              prop.text "Start"
              prop.disabled isRunning
              prop.onClick (fun _ -> setRunning true)
            ]
          Html.button
            [
              prop.text "Pause"
              prop.disabled (not isRunning)
              prop.onClick (fun _ -> setRunning false)
            ]
          Html.button
            [
              prop.text "Reset"
              prop.disabled ((seconds = 0))
              prop.onClick (fun _ -> reset ())
            ]
        ]
  }

open Browser.Dom

ReactDOM.render(stopwatch, document.getElementById "root")
