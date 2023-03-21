///the quiz script
let optionsGroup = document.querySelectorAll('.options')

optionsGroup.forEach(group => {
    let options = group.querySelectorAll('li')
    console.log(options)

    options.forEach(option=>{
        option.addEventListener("click", ()=>OnOptionClick(options, option))

    })
});

function OnOptionClick(options, option){
    console.log(option)
    options.forEach(option=>{
        option.classList.remove("bg-green-500")
    })

    option.classList.add("bg-green-500")
}