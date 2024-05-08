const openArticles = document.querySelectorAll('[data-article-id]')

openArticles.forEach((article) => {
    const deleteButton = article.querySelector('[data-article="delete"]')
    console.log(deleteButton)
    deleteButton.addEventListener('click', (e) => {
        e.preventDefault()
        console.log("qefqefqfeqf")
        const modalContainer = document.createElement('div')
        modalContainer.setAttribute('id', 'modal-container')

        document.body.appendChild(modalContainer)

        modalContainer.innerHTML = `
            <div class="my-modal my-modal--close col" data-modal="modal">
                <div class="modal-content my-modal__container" data-modal="content">
                    <div class="modal-body">
                        <h3>Вы уверены, что хотите удалить статью?</h3>
                    </div>
                    <div class="modal-footer">
                        <form action="/articles/${article.getAttribute('data-article-id')}"
                              method="post">
                            <button class="btn btn-danger btn-lg"
                                    type="submit">
                                Удалить
                            </button>
                        </form>
                        <button type="button"
                                class="btn btn-secondary btn-lg"
                                data-modal="closeModal">
                                Отмена
                        </button>
                    </div>
                </div>
            </div>
            `

        const confirmDelete = modalContainer.querySelectorAll('[data-modal="closeModal"]')
        const modal = modalContainer.querySelector('[data-modal="modal"]')
        const modalContent = modalContainer.querySelector('[data-modal="content"]')

        modal.addEventListener('click', (e) => {
            e.preventDefault()
            closeModal(modalContainer)
        })

        modalContent.addEventListener('click', (e) => e.stopPropagation())

        confirmDelete.forEach((el) => {
            el.addEventListener('click', (e) => {
                e.preventDefault()

                closeModal(modalContainer)
            })
        })
    })
})

const closeModal = (element) => {
    element.remove()
}
