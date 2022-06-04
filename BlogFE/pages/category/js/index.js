const url = 'https://localhost:7213/api/Category';
let listCategory = [];

function getListCategory(searchText = '')
{
    fetch(`${url}?SearchText=${searchText}`)
        .then(response => response.json()) //=> arrow function
        .then(data => displayListCategory(data))
        .catch(error => console.error('Unable to get category list.', error));
}

function displayListCategory(data)
{
    const tBody = document.getElementById('listCategoryIds');
    tBody.innerHTML = '';

    data.forEach(category => {
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(category.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        textNode = document.createTextNode(category.slug);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        textNode = document.createTextNode(category.createAt);
        td3.appendChild(textNode);

        let td4 = tr.insertCell(3);
        textNode = document.createTextNode(category.updateAt);
        td4.appendChild(textNode);

        const button = document.createElement('button');
        let editButton = button.cloneNode(false);
        editButton.innerHTML = 'Edit';
        editButton.classList.add('w3-button', 'w3-white', 'w3-border', 'w3-round-large');
        editButton.setAttribute('onclick', `displayEditForm(${category.id})`);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let deletetButton = button.cloneNode(false);
        deletetButton.innerHTML = 'Delete';
        deletetButton.classList.add('w3-button', 'w3-white', 'w3-border', 'w3-round-large');
        deletetButton.setAttribute('onclick', `deleteCategory(${category.id})`);

        let td6 = tr.insertCell(5);
        td6.appendChild(deletetButton);
    });

    listCategory = data;

}

function addCategory()
{
    const addNameTextBox = document.getElementById('add-name');
    const addSlugTextBox = document.getElementById('add-slug');

    const category = {
        name: addNameTextBox.value.trim(),
        slug: addSlugTextBox.value.trim()
    }

    fetch(`${url}`,{
        method: 'POST',
        headers:{
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
    .then(response => response.json())
    .then(() => {
        addNameTextBox.value = '';
        addSlugTextBox.value = '';
        getListCategory();
    })
    .catch(error => console.error('Unable to add category', error));
}

function deleteCategory(categoryId)
{
    fetch(`${url}/${categoryId}`,{
        method: 'DELETE'
    })
    .then(() => getListCategory())
    .catch(error => console.error('Unable to delete category', error));
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displayEditForm(categoryId)
{
    var category = listCategory.find(category => category.id === categoryId);

    document.getElementById('edit-name').value = category.name;
    document.getElementById('edit-slug').value = category.slug;
    document.getElementById('edit-id').value = category.id;
    document.getElementById('editForm').style.display = 'block';
}

function updateCategory()
{
    const categoryId = document.getElementById('edit-id').value;
    const category = {
        'name': document.getElementById('edit-name').value.trim(),
        'slug':document.getElementById('edit-slug').value.trim()
    };

    fetch(`${url}/${categoryId}`, {
        method: 'PUT',
        headers:{
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
    .then(() => getListCategory())
    .catch(error => console.error('Unable to update category', error));

    closeInput();
}

function activeCategoryTab()
{
    let currentActiveElement = document.querySelector('.w3-bar-block .w3-blue');
    if(currentActiveElement){
        currentActiveElement.classList.remove('w3-blue');
        let categorypageElement = document.getElementById('categorypage');
        if(categorypageElement)
        categorypageElement.classList.add('w3-blue');
    }
}

function searchCategory()
{
    let delay = (()=>{
        let timer = 0;
        return function(callback, ms){
          clearTimeout (timer);
          timer = setTimeout(callback, ms);
        };
      })();
    
    let searchCategoryElement = document.getElementById('searchCategory');
    if(searchCategoryElement)
    {
        searchCategoryElement.addEventListener('keyup', () => {
            delay(function(){
                    let searchCategoryValue = searchCategoryElement.value.toLowerCase();
                    getListCategory(searchCategoryValue);
                }, 1000 );
            });
    }
}

activeCategoryTab();
getListCategory();
searchCategory();
closeInput();