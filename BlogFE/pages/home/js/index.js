function activeHomeTab()
{
    let currentActiveElement = document.querySelector('.w3-bar-block .w3-blue');
    if(currentActiveElement){
        currentActiveElement.classList.remove('w3-blue');
        let homepageElement = document.getElementById('homepage');
        if(homepageElement)
            homepageElement.classList.add('w3-blue');
    }
}

activeHomeTab();