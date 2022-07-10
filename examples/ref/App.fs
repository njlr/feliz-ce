module App

open Feliz
open Feliz.CE
open Browser.Types

let app =
  react {
    let! inputRef = React.withRef None

    let onButtonClick _ =
      match inputRef.current with
      | Some element ->
        let inputElement = unbox<HTMLInputElement> element
        inputElement.focus()
      | None -> ()

    return
      Html.div
        [
          Html.input
            [
              prop.type'.text
              prop.ref inputRef
            ]
          Html.button
            [
              prop.text "Focus the input"
              prop.onClick onButtonClick
            ]
        ]
  }

open Browser.Dom

ReactDOM.render(app, document.getElementById "root")
