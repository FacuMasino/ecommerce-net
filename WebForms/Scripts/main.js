// Verifica y cambia el icono del botón de búsqueda
// Si la clase bi-search no está presente y el usuario escribe
// Significa que intenta realizar una nueva busqueda
// y cambia el icono de "limpiar" a "buscar"
const checkSearchBtn = () => {
    let searchBtnIcon = document.getElementById('SearchBtn').firstElementChild;
    let readyToSearch = searchBtnIcon.classList.contains("bi-search");
    if (!readyToSearch) {
        searchBtnIcon.classList.remove("bi-x-circle");
        searchBtnIcon.classList.add("bi-search");
    }
}
