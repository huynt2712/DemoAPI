$(document).ready( $ => {

const url = 'https://localhost:7213/api';
let listCategory = [];
let currentPage = 1;
let pageSize = 5;

function getListCategory(searchValue = '')
{
    fetch(`${url}/PostCategory?SearchText=${searchValue}&PageNumber=${currentPage}&PageSize=${pageSize}`)
        .then(response => response.json()) //=> arrow function
        .then(data => {
            displayListCategory(data);
            if(data.totalPages > 0)
                setUpPagination(data);
        })
        .catch(error => console.error('Unable to get category list.', error));
}

function displayListCategory(data)
{
    const tBody = document.getElementById('listCategoryIds');
    tBody.innerHTML = '';

    data.items.forEach(category => {
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

    listCategory = data.items;
}

function addCategory()
{
    const addNameTextBox = document.getElementById('add-name');
    const addSlugTextBox = document.getElementById('add-slug');

    const category = {
        name: addNameTextBox.value.trim(),
        slug: addSlugTextBox.value.trim()
    }

    fetch(`${url}/PostCategory`,{
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
    fetch(`${url}/PostCategory/${categoryId}`,{
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

    fetch(`${url}/PostCategory/${categoryId}`, {
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

function paginationCategory(pageNumber)
{
    currentPage = pageNumber;
    getListCategory();
}

function setUpPagination(data)
{
    let categoryPaginationElement = document.getElementById('category_pagination');
    categoryPaginationElement.innerHTML = '';
    if(data.hasPrevious)
    {
        let itemElement = document.createElement('a');
        itemElement.classList.add('w3-bar-item', 'w3-button', `pageNumer_${currentPage - 1}`);
        itemElement.innerHTML = `&laquo;`;
        itemElement.setAttribute('onclick', `paginationCategory(${currentPage - 1})`);
        categoryPaginationElement.append(itemElement);
    }

    for(let pageNumber = 0; pageNumber < data.totalPages; pageNumber++){
        itemElement = document.createElement('a');
        itemElement.classList.add('w3-bar-item', 'w3-button', `pageNumer_${pageNumber+1}`);
        itemElement.innerText = pageNumber + 1;
        itemElement.setAttribute('onclick', `paginationCategory(${pageNumber + 1})`);
        categoryPaginationElement.append(itemElement);
    }

    if(data.hasNext)
    {
        itemElement = document.createElement('a');
        itemElement.classList.add('w3-bar-item', 'w3-button', `pageNumer_${currentPage + 1}`);
        itemElement.innerHTML = `&raquo;`;
        itemElement.setAttribute('onclick', `paginationCategory(${currentPage + 1})`);
        categoryPaginationElement.append(itemElement);
    }

    let currentPageElement = document.querySelector(`.pageNumer_${currentPage}`);
    currentPageElement.classList.add('w3-green');
}

function SearchCategory()
{
    let searchElement = document.getElementById("searchCategory");
    let searchValue = searchElement.value.toLowerCase();
    getListCategory(searchValue);

}

getListCategory();
closeInput();
  
  });
