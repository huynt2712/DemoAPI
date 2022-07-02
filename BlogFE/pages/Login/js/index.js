const url = 'https://localhost:7213/api';

function login()
{
    const userNameElement = document.getElementById('userName');
    if(!userNameElement) return;

    const passwordElement = document.getElementById('password');
    if(!passwordElement) return;


    const loginModel = {
            "userName": userNameElement.value.trim(),
            "password": passwordElement.value.trim()
    };

    fetch(`${url}/User/Login`,{
        method: 'POST',
        headers:{
            Accept:'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(loginModel)
    })
    .then((response) => response.json())
    .then((data) => {
        if(data && data.isSuccess)
        {
            localStorage.setItem('devblog_token', data.data);
            window.location.href = '/pages/home/index.html';
        }
        else
        {
            const errorMessageElement = document.getElementById('login_error');
            if(!errorMessageElement) return;
            errorMessageElement.textContent = data.message;
        }
    })


}