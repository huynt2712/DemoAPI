const url = 'https://localhost:7213/api';

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

function displayUserInfo()
{
    const userInfoElement = document.getElementById('user_info');
    if(!userInfoElement) return;

    const token = localStorage.getItem('devblog_token');
    if(!token) window.location.href = '/pages/Login/index.html';

    fetch(`${url}/User/UserInfo`,
    {
        method: 'GET',
        headers: {
            Accept:'application/json',
            "Content-Type": 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })
    .then((response) => {
        if(response.status == 401){
            window.location.href = '/pages/Login/index.html';
            return;
        }
        else
            return response.json();
    })
    .then((data) => {
        userInfoElement.textContent = `Xin chào ${data}`;
    })
    .catch((error) => {
        console.log(error);
    })
}

activeHomeTab();
displayUserInfo();
