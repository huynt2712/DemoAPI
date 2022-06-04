function loadCategoryPage(event)
{
    let currentActiveElement = document.querySelector('.w3-bar-block .w3-blue');
    currentActiveElement.classList.remove('w3-blue');
    event.target.classList.add('w3-blue');
    let pageContentElement = document.getElementById('pageContent');
    pageContentElement.innerHTML = '';
    // $(pageContentElement).load('components/category/index.html');
    window.location.href = `${window.location.origin}/components/category/index.html`;
}


function loadHomePage(event)
{
    let currentActiveElement = document.querySelector('.w3-bar-block .w3-blue');
    currentActiveElement.classList.remove('w3-blue');
    event.target.classList.add('w3-blue');
    let pageContentElement = document.getElementById('pageContent');
    pageContentElement.innerHTML = '';
    $(pageContentElement).load('/components/home/index.html');
}