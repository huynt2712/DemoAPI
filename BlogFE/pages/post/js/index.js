const postUrl = "https://localhost:7213/api/Post";
const categoryUrl = "https://localhost:7213/api/Category";
let listPost = [];
let currentPage = 1;
let pageSize = 3;

function getListPost(searchText = "") {
  fetch(
    `${postUrl}?SearchText=${searchText}&PageNumber=${currentPage}&PageSize=${pageSize}`
  )
    .then((response) => response.json()) //=> arrow function
    .then((data) => {
      displayListPost(data);

      currentPage = data.currentPage;
      if (data.totalPages > 0) setupPagintion(data);
    })
    .catch((error) => console.error("Unable to get post list.", error));
}

function displayListPost(data) {
  const tBody = document.getElementById("listPostIds");
  tBody.innerHTML = "";

  data.items.forEach((post) => {
    let tr = tBody.insertRow();
    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(post.title);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    textNode = document.createTextNode(post.description);
    td2.appendChild(textNode);

    // let td3 = tr.insertCell(2);
    // textNode = document.createTextNode(post.content);
    // td3.appendChild(textNode);

    let td4 = tr.insertCell(2);
    textNode = document.createTextNode(post.slug);
    td4.appendChild(textNode);

    let td5 = tr.insertCell(3);
    const img = document.createElement("img");
    let uploadImage = img.cloneNode(false);
    uploadImage.src = `https://localhost:7213/${post.imagePath}`;
    uploadImage.setAttribute("style", "width:100px;height:100px");
    td5.appendChild(uploadImage);

    let td6 = tr.insertCell(4);
    textNode = document.createTextNode(
      post.createdDate ? new Date(post.createdDate).toDateString() : ""
    );
    td6.appendChild(textNode);

    let td7 = tr.insertCell(5);
    textNode = document.createTextNode(
      post.updateDate ? new Date(post.updateDate).toDateString() : ""
    );
    td7.appendChild(textNode);

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

    let td8 = tr.insertCell(6);
    td8.appendChild(editButton);

    let deletetButton = button.cloneNode(false);
    deletetButton.innerHTML = "Delete";
    deletetButton.classList.add(
      "w3-button",
      "w3-white",
      "w3-border",
      "w3-round-large"
    );
    deletetButton.setAttribute("onclick", `deletePost(${post.id})`);

    let td9 = tr.insertCell(7);
    td9.appendChild(deletetButton);
  });

  listPost = data.items;
}

function addPost() {
  let imgFileUploadElement = document.getElementById("imgFileUpload");
  let imagePath = imgFileUploadElement.dataset.path;

  const addTitleTextBox = document.getElementById("add-title");
  const addDescriptionTextBox = document.getElementById("add-description");
  const addContentTextBox = document.getElementById("add-content");
  const addSlugTextBox = document.getElementById("add-slug");
  const addCategorySelect = document.getElementById("add-category");

  if (!addTitleTextBox) return;
  if (!addDescriptionTextBox) return;
  if (!addContentTextBox) return;
  if (!addSlugTextBox) return;
  if (!addCategorySelect) return;

  let title = addTitleTextBox.value.trim();
  if (title === "") {
    let titleErrorElement = document.getElementById("post_title_error");
    if (!titleErrorElement) return;
    titleErrorElement.innerHTML = "Title can not be empty";
    return;
  }

  let description = addDescriptionTextBox.value.trim();
  if (description === "") {
    let descriptionErrorElement = document.getElementById(
      "post_description_error"
    );
    if (!descriptionErrorElement) return;
    descriptionErrorElement.innerHTML = "description can not be empty";
    return;
  }

  let content = contentEditor.getData();
  if (content === "") {
    let contentErrorElement = document.getElementById("post_content_error");
    if (!contentErrorElement) return;
    contentErrorElement.innerHTML = "Content can not be empty";
    return;
  }

  let slug = addSlugTextBox.value.trim();
  if (slug === "") {
    let slugErrorElement = document.getElementById("post_slug_error");
    if (!slugErrorElement) return;
    slugErrorElement.innerHTML = "Slug can not be empty";
    return;
  }

  const post = {
    title: addTitleTextBox.value.trim(),
    description: addDescriptionTextBox.value.trim(),
    content: content,
    slug: addSlugTextBox.value.trim(),
    imagepath: imagePath,
    postCategoryId: addCategorySelect.value,
  };

  fetch(`${postUrl}`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(post),
  })
    .then((response) => response.json())
    .then((data) => {
      addTitleTextBox.value = "";
      addDescriptionTextBox.value = "";
      addContentTextBox.value = "";
      addSlugTextBox.value = "";
      getListPost();
    })
    .catch((error) => console.error("Unable to add post", error));
}

function deletePost(postId) {
  fetch(`${postUrl}/${postId}`, {
    method: "DELETE",
  })
    .then(() => getListPost())
    .catch((error) => console.error("Unable to delete post", error));
}

function closeInput() {
  document.getElementById("editForm").style.display = "none";
}

function displayEditForm(postId) {
  var post = listPost.find((post) => post.id === postId);

  document.getElementById("edit-title").value = post.title;
  document.getElementById("edit-description").value = post.description;
  CKEDITOR.instances["edit-content"].setData(post.content);
  document.getElementById("edit-id").value = post.id;
  document.getElementById("edit-slug").value = post.slug;
  document.getElementById("edit-category").value = post.postCategoryId;
  let imgFileUploadElement = document.getElementById("imgFileUploadEdit");
  imgFileUploadElement.style.display = "block";
  imgFileUploadElement.src = `https://localhost:7213/${post.imagePath}`;
  document.getElementById("editForm").style.display = "block";
}

function updatePost() {
  const postId = document.getElementById("edit-id").value;
  let imgFileUploadElement = document.getElementById("imgFileUploadEdit");
  const imagePath = imgFileUploadElement.dataset.path;
  const TitleEditTextBox = document.getElementById("edit-title");
  const DescriptionEditTextBox = document.getElementById("edit-description");
  const ContentEditTextBox = document.getElementById("edit-content");
  const SlugEditTextBox = document.getElementById("edit-slug");
  const addCategorySelect = document.getElementById("edit-category");

  let titleEditErrorElement = document.getElementById("postedit_title_error");
  let descriptionEditErrorElement = document.getElementById(
    "postedit_description_error"
  );
  let contentEditErrorElement = document.getElementById(
    "postedit_content_error"
  );
  let slugEditErrorElement = document.getElementById("postedit_slug_error");

  let titleEdit = TitleEditTextBox.value.trim();
  if (titleEdit === "") {
    if (!titleEditErrorElement) return;
    titleEditErrorElement.innerHTML = "Title can not be empty";
    return;
  }

  let descriptionEdit = DescriptionEditTextBox.value.trim();
  if (descriptionEdit === "") {
    if (!descriptionEditErrorElement) return;
    descriptionEditErrorElement.innerHTML = "Description can not be empty";
    return;
  }

  let contentEdit = editContentEditor.getData();
  if (contentEdit === "") {
    if (!contentEditErrorElement) return;
    contentEditErrorElement.innerHTML = "Content can not be empty";
    return;
  }

  let slugEdit = SlugEditTextBox.value.trim();
  if (slugEdit === "") {
    if (!slugEditErrorElement) return;
    slugEditErrorElement.innerHTML = "Slug can not be empty";
    return;
  }

  const post = {
    title: TitleEditTextBox.value.trim(),
    description: DescriptionEditTextBox.value.trim(),
    content: contentEdit,
    slug: SlugEditTextBox.value.trim(),
    imagePath: imagePath,
    postCategoryId: addCategorySelect.value,
  };

  fetch(`${postUrl}/${postId}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
      "Authorization": 'basic afwefsadfsafdsa'
    },
    body: JSON.stringify(post),
  })
    .then(() => getListPost())
    .catch((error) => console.error("Unable to update post", error));

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

function searchPost() {
  let delay = (() => {
    let timer = 0;
    return function (callback, ms) {
      clearTimeout(timer);
      timer = setTimeout(callback, ms);
    };
  })();

  let searchPostElement = document.getElementById("searchPost");
  if (searchPostElement) {
    searchPostElement.addEventListener("keyup", () => {
      delay(function () {
        let searchPostValue = searchPostElement.value.toLowerCase();
        if(searchPostValue) currentPage = 1;
        getListPost(searchPostValue);
      }, 1000);
    });
  }
}

function setupPagintion(data) {
  let postPaginationElement = document.getElementById("post_pagination");
  if (!postPaginationElement) return;

  postPaginationElement.innerHTML = "";
  if (data.hasPrevious) {
    let itemElement = document.createElement("a");
    itemElement.innerHTML = `&laquo;`;
    itemElement.classList.add("w3-button", `pageNumber_${currentPage - 1}`);
    itemElement.setAttribute("onclick", `paginationPost(${currentPage - 1})`);
    postPaginationElement.append(itemElement);
  }

  for (let pageNumber = 0; pageNumber < data.totalPages; pageNumber++) {
    itemElement = document.createElement("a");
    itemElement.classList.add("w3-button", `pageNumber_${pageNumber + 1}`);
    itemElement.innerHTML = pageNumber + 1;
    itemElement.setAttribute("onclick", `paginationPost(${pageNumber + 1})`);
    postPaginationElement.append(itemElement);
  }

  if (data.hasNext) {
    let itemElement = document.createElement("a");
    itemElement.innerHTML = `&raquo;`;
    itemElement.classList.add("w3-button", `pageNumber_${currentPage + 1}`);
    itemElement.setAttribute("onclick", `paginationPost(${currentPage + 1})`);
    postPaginationElement.append(itemElement);
  }

  let currentPageElement = document.querySelector(`.pageNumber_${currentPage}`);
  currentPageElement.classList.add("w3-green");
}

function paginationPost(pageNumber) {
  currentPage = pageNumber;
  let searchPostElement = document.getElementById("searchPost");
  if(searchPostElement) {
    let searchPostValue = searchPostElement.value.toLowerCase();
    getListPost(searchPostValue);
  }
}

function uploadFile() {
  let fileUploadElement = document.getElementById("add-image");
  if (!fileUploadElement) return;

  let formData = new FormData();
  formData.append("file", fileUploadElement.files[0]);
  const postUrl = "https://localhost:7213/api/File";

  fetch(postUrl, {
    method: "POST",
    body: formData,
  })
    .then((response) => response.json())
    .then((data) => {
      let imgFileUploadElement = document.getElementById("imgFileUpload");
      if (!imgFileUploadElement) return;

      imgFileUploadElement.style.display = "block";
      imgFileUploadElement.src = `https://localhost:7213/${data.path}`;
      imgFileUploadElement.dataset.path = data.path;
    });
}

function uploadFileEdit() {
  let fileUploadElement = document.getElementById("edit-image");
  if (!fileUploadElement) return;

  let formData = new FormData();
  formData.append("file", fileUploadElement.files[0]);
  const postUrl = "https://localhost:7213/api/File";

  fetch(postUrl, {
    method: "POST",
    body: formData,
  })
    .then((response) => response.json())
    .then((data) => {
      let imgFileUploadElement = document.getElementById("imgFileUploadEdit");
      if (!imgFileUploadElement) return;

      imgFileUploadElement.style.display = "block";
      imgFileUploadElement.src = `https://localhost:7213/${data.path}`;
      imgFileUploadElement.dataset.path = data.path;
    });
}

function getListCategory(searchText = "") {
  fetch(
    `${categoryUrl}?SearchText=${searchText}&PageNumber=${1}&PageSize=${10}`
  )
    .then((response) => response.json())
    .then((data) => {
      let categorySelectElement = document.getElementById("add-category");
      if (!categorySelectElement) return;

      data.items.forEach((category) => {
        let option = document.createElement("option");
        option.text = category.name;
        option.value = category.id;
        categorySelectElement.add(option);
      });
    })
    .catch((error) => console.error("Unable to get category list.", error));
}
function getListEditCategory(searchText = "") {
  fetch(
    `${categoryUrl}?SearchText=${searchText}&PageNumber=${1}&PageSize=${10}`
  )
    .then((response) => response.json())
    .then((data) => {
      let categoryEditSelectElement = document.getElementById("edit-category");
      if (!categoryEditSelectElement) return;

      data.items.forEach((category) => {
        let option = document.createElement("option");
        option.text = category.name;
        option.value = category.id;
        categoryEditSelectElement.add(option);
      });
    })
    .catch((error) => console.error("Unable to get category list.", error));
}
activePostTab();
getListPost();
searchPost();
closeInput();
getListCategory();
getListEditCategory();

let contentEditor = CKEDITOR.replace("add-content");
let editContentEditor = CKEDITOR.replace("edit-content");
