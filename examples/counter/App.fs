module App

open Feliz
open Feliz.CE

let counter initialCount =
  react {
    let! count, setCount = React.withState initialCount

    return
      Html.div
        [
          Html.h1 $"Count: {count}"
          Html.button
            [
              prop.text "Increment"
              prop.onClick (fun _ -> setCount (count + 1))
            ]
        ]
  }

open Browser.Dom

ReactDOM.render(counter 0, document.getElementById "root")
