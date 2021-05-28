export function AjustarInput() {
    let inputel = document.getElementById('inputFile')
    inputel.children[3].classList.add('d-flex')
    inputel.children[3].classList.add('justify-content-end')
    inputel.children[3].classList.add('pe-3')
    inputel.children[3].children[0].children[0].textContent = 'Procurar'
    inputel.children[3].children[0].insertAdjacentHTML("afterbegin", '<span class="mx-3">Selecione a(s) imagem(s)</span>')
}


export function IniciarPopOvers() {
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        console.log('iniciadoo popover')
        return new bootstrap.Popover(popoverTriggerEl)
    })
}

