const closeButton = document.getElementById('close')
const confirmDeleteButton = document.getElementById('confirmDelete')
const modal = document.getElementById("close-modal")
const openButton = document.getElementById("delete-button")

const closeModal = () => {
    modal.classList.remove('my-modal--open')
    modal.classList.add('my-modal--close')

}

const openModal = () => {
    modal.classList.add('my-modal--open')
    modal.classList.remove('my-modal--close')
}

closeButton.addEventListener('click', () => {
    closeModal()
})

confirmDeleteButton.addEventListener('click', () => {
    closeModal()
})

modal.addEventListener('click', (e) => {
    closeModal()
})

modal.children[0].addEventListener("click", (e) => {
    e.stopPropagation()
})

openButton.addEventListener('click', () => {
    openModal()
})
