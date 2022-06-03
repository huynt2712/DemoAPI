const url = 'https://localhost:7213/api';
let listCategory = [];

function getListCategory()
{
    fetch(`${url}/PostCategory`)
        .then(response => response.json())
        .then(data => displayListCategory(data))
        .catch(error => console.error('Unable to get category list.', error));
}

function displayListCategory(data)
{
    const tBody = document.getElementById('listCategoryId');
    tBody.innerHTML = '';

    const button = document.createElement('button');
    data.forEach(category => {
        let editButton = button.cloneNode(false);
        editButton.classList.add('w3-btn' , 'w3-white' , 'w3-border' , 'w3-round-large');
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${category.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.classList.add('w3-btn' , 'w3-white' , 'w3-border' , 'w3-round-large');
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteCategory(${category.id})`);

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

        
        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    listCategory = data;
}

function addCategory() {
    const addNameTextbox = document.getElementById('add-name');
    const addSlugTextbox = document.getElementById('add-slug');
  
    const category = {
      name: addNameTextbox.value.trim(),
      slug: addSlugTextbox.value.trim()
    };
  
    fetch(`${url}/PostCategory`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(category)
    })
      .then(response => response.json())
      .then(() => {
        getListCategory();
        addNameTextbox.value = '';
        addSlugTextbox.value = ''
      })
      .catch(error => console.error('Unable to add category.', error));
  }

  function closeInput() {
    document.getElementById('editForm').style.display = 'none';
  }

  function displayEditForm(id) {
    const category = listCategory.find(category => category.id === id);
    
    document.getElementById('edit-id').value = category.id;
    document.getElementById('edit-name').value = category.name;
    document.getElementById('edit-slug').value = category.slug;
    document.getElementById('editForm').style.display = 'block';
  }

  function updateCategory() {
    const categoryId = document.getElementById('edit-id').value;
    const category = {
      id: parseInt(categoryId, 10),
      name: document.getElementById('edit-name').value.trim(),
      slug: document.getElementById('edit-slug').value.trim()
    };
  
    fetch(`${url}/PostCategory/${categoryId}`, {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(category)
    })
    .then(() => getListCategory())
    .catch(error => console.error('Unable to update category.', error));
  
    closeInput();
  
    return false;
  }

  function deleteCategory(id) {
    fetch(`${url}/PostCategory/${id}`, {
      method: 'DELETE'
    })
    .then(() => getListCategory())
    .catch(error => console.error('Unable to delete category.', error));
  }

getListCategory();

