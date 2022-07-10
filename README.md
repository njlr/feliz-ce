# Feliz.CE

Experimental Computation Expression for Feliz, as an alternative to React Hooks.

```fsharp
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
```
