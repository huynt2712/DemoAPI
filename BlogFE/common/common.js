function loadHomePage(event){
    let pageContentElement = document.getElementById('pageContent');
    if(pageContentElement)
    {
        pageContentElement.innerHTML = '';
        window.location.href = '/pages/home/index.html';
    }
}

function loadCategoryPage(event)
{
    let pageContentElement = document.getElementById('pageContent');
    if(pageContentElement)
    {
        pageContentElement.innerHTML = '';
        window.location.href = '/pages/category/index.html';
    }
}