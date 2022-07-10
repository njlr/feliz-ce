module App

open Feliz
open Feliz.CE

let twoCounters =
  react {
    let! count1, setCount1 = React.withState 0
    and! count2, setCount2 = React.withState 0

    return
      Html.div
        [
          Html.h1 $"Count 1: {count1}"
          Html.button
            [
              prop.text "Increment"
              prop.onClick (fun _ -> setCount1 (count1 + 1))
            ]
          Html.h1 $"Count 2: {count2}"
          Html.button
            [
              prop.text "Increment"
              prop.onClick (fun _ -> setCount2 (count2 + 1))
            ]
        ]
  }

open Browser.Dom

ReactDOM.render(twoCounters, document.getElementById "root")
