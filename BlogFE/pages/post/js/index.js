const url = "https://localhost:7213/api/Post";
let listPost = [];
let currentPage = 1;
let pageSize = 5;

function getListCategory(searchText = "") {
  fetch(
    `${url}?SearchText=${searchText}&PageNumber=${currentPage}&PageSize=${pageSize}`
  )
    .then((response) => response.json()) //=> arrow function
    .then((data) => {
      displayListCategory(data);

      if (data.totalPages > 0) setupPagintion(data);
    })
    .catch((error) => console.error("Unable to get category list.", error));
}

function displayListCategory(data) {
  const tBody = document.getElementById("listCategoryIds");
  tBody.innerHTML = "";

  data.items.forEach((post) => {
    let tr = tBody.insertRow();
    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(post.title);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    textNode = document.createTextNode(post.discription);
    td2.appendChild(textNode);

    // let td3 = tr.insertCell(2);
    // textNode = document.createTextNode(
    //   new Date(category.createAt).toDateString()
    // );
    // td3.appendChild(textNode);

    // let td4 = tr.insertCell(3);
    // textNode = document.createTextNode(
    //   category.updateAt ? "" : new Date(category.updateAt).toDateString()
    // );
    // td4.appendChild(textNode);

    const button = document.createElement("button");
    let editButton = button.cloneNode(false);
    editButton.innerHTML = "Edit";
    editButton.classList.add(
      "w3-button",
      "w3-white",
      "w3-border",
      "w3-round-large"
    );
    editButton.setAttribute("onclick", `displayEditForm(${post.id})`);

    let td3 = tr.insertCell(2);
    td3.appendChild(editButton);

    let deletetButton = button.cloneNode(false);
    deletetButton.innerHTML = "Delete";
    deletetButton.classList.add(
      "w3-button",
      "w3-white",
      "w3-border",
      "w3-round-large"
    );
    deletetButton.setAttribute("onclick", `deleteCategory(${post.id})`);

    let td4 = tr.insertCell(3);
    td4.appendChild(deletetButton);
  });

  listPost = data.items;
}

function addCategory() {
  const addNameTextBox = document.getElementById("add-name");
  const addSlugTextBox = document.getElementById("add-slug");

  if (!addNameTextBox) return;
  if (!addSlugTextBox) return;

  let name = addNameTextBox.value.trim();
  if (name === "") {
    let nameErrorElement = document.getElementById("category_name_error");
    if (!nameErrorElement) return;
    nameErrorElement.innerHTML = "Name can not be empty";
    return;
  }

  const category = {
    name: addNameTextBox.value.trim(),
    slug: addSlugTextBox.value.trim(),
  };

  fetch(`${url}`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(category),
  })
    .then((response) => response.json())
    .then((data) => {
      addNameTextBox.value = "";
      addSlugTextBox.value = "";
      getListCategory();
    })
    .catch((error) => console.error("Unable to add category", error));
}

function deleteCategory(categoryId) {
  fetch(`${url}/${categoryId}`, {
    method: "DELETE",
  })
    .then(() => getListCategory())
    .catch((error) => console.error("Unable to delete category", error));
}

function closeInput() {
  document.getElementById("editForm").style.display = "none";
}

function displayEditForm(categoryId) {
  var category = listCategory.find((category) => category.id === categoryId);

  document.getElementById("edit-name").value = category.name;
  document.getElementById("edit-slug").value = category.slug;
  document.getElementById("edit-id").value = category.id;
  document.getElementById("editForm").style.display = "block";
}

function updateCategory() {
  const categoryId = document.getElementById("edit-id").value;
  const category = {
    name: document.getElementById("edit-name").value.trim(),
    slug: document.getElementById("edit-slug").value.trim(),
  };

  fetch(`${url}/${categoryId}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(category),
  })
    .then(() => getListCategory())
    .catch((error) => console.error("Unable to update category", error));

  closeInput();
}

function activePostTab() {
  let currentActiveElement = document.querySelector(".w3-bar-block .w3-blue");
  if (currentActiveElement) {
    currentActiveElement.classList.remove("w3-blue");
    let postpageElement = document.getElementById("postpage");
    if (postpageElement) postpageElement.classList.add("w3-blue");
  }
}

function searchCategory() {
  let delay = (() => {
    let timer = 0;
    return function (callback, ms) {
      clearTimeout(timer);
      timer = setTimeout(callback, ms);
    };
  })();

  let searchCategoryElement = document.getElementById("searchCategory");
  if (searchCategoryElement) {
    searchCategoryElement.addEventListener("keyup", () => {
      delay(function () {
        let searchCategoryValue = searchCategoryElement.value.toLowerCase();
        getListCategory(searchCategoryValue);
      }, 1000);
    });
  }
}

function setupPagintion(data) {
  let categoryPaginationElement = document.getElementById(
    "category_pagination"
  );
  if (!categoryPaginationElement) return;

  categoryPaginationElement.innerHTML = "";
  if (data.hasPrevious) {
    let itemElement = document.createElement("a");
    itemElement.innerHTML = `&laquo;`;
    itemElement.classList.add("w3-button", `pageNumber_${currentPage - 1}`);
    itemElement.setAttribute(
      "onclick",
      `paginationCategory(${currentPage - 1})`
    );
    categoryPaginationElement.append(itemElement);
  }

  for (let pageNumber = 0; pageNumber < data.totalPages; pageNumber++) {
    itemElement = document.createElement("a");
    itemElement.classList.add("w3-button", `pageNumber_${pageNumber + 1}`);
    itemElement.innerHTML = pageNumber + 1;
    itemElement.setAttribute(
      "onclick",
      `paginationCategory(${pageNumber + 1})`
    );
    categoryPaginationElement.append(itemElement);
  }

  if (data.hasNext) {
    let itemElement = document.createElement("a");
    itemElement.innerHTML = `&raquo;`;
    itemElement.classList.add("w3-button", `pageNumber_${currentPage + 1}`);
    itemElement.setAttribute(
      "onclick",
      `paginationCategory(${currentPage + 1})`
    );
    categoryPaginationElement.append(itemElement);
  }

  let currentPageElement = document.querySelector(`.pageNumber_${currentPage}`);
  currentPageElement.classList.add("w3-green");
}

function paginationCategory(pageNumber) {
  currentPage = pageNumber;
  getListCategory();
}

activePostTab();
getListCategory();
searchCategory();
closeInput();
