const url = "https://localhost:7034/api";

function displayUserInfo()
{
const userNameElement = document.getElementById('userName');
if(!userNameElement) return;
const token = localStorage.getItem("devblog_token");
if(!token) window.location.href = "/pages/Login/index.html";

fetch(`${url}/User/userInfo`,
    {
        method: "GET",
        headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
        'Authorization': 'Bearer ' + token
        }
    })
.then((response) => {
    debugger;
    if(response.status == 401){
        window.location.href = "/pages/Login/index.html";
        return;
    }
    else
        return  response.json();
})
.then((data) => {
    userNameElement.textContent = data;
});
}

displayUserInfo();