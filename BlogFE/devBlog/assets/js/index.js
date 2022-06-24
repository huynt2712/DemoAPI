//const postUrl = "https://localhost:7213/api/Post";
let listPost = [];
let currentPage = 1;
let pageSize = 1;

function renderTemplateHtml()
{
  const postTemplate = document.getElementById('postTemplate');
   if(!postTemplate) return;

   const postElement = postTemplate.content.firstElementChild.cloneNode(true);

   const postListElement = document.getElementById('postList');
   if(!postListElement) return;

   postListElement.appendChild(postElement);
}

renderTemplateHtml();

function getPostList() {
  const url = "https://localhost:7213/api/Post";
  //const ulElement = document.getElementById("posts");

  fetch(url)
    .then((response) => {
      return response.json();
    })
    .then((data) => {
      displayListPost(data);
    })
    .catch(function (error) {
      console.log(error);
    });
}

function displayListPost(data) {
  data.items.forEach((post) => {
    const imageElement = document.getElementById("img-post");
    imageElement.src = `https://localhost:7213/${post.imagePath}`;

    const titleElement = document.getElementById("post-title");
    titleElement.textContent = post.title;

    const descriptionElement = document.getElementById("post-description");
    descriptionElement.textContent = post.description;
  });
}

function createPostElement(post) {
  if (!post) return null;

  const postTemplate = document.getElementById("postTemplate");
  if (!postTemplate) return;

  //const postElement = postTemplate.content.firstElementChild.cloneNode(true);
  //console.log(postElement);
  //postElement.dataset.id = post.id;

  const imageElement = post.querySelector("#post-image");
  if (imageElement) imageElement.src = post.image;
  console.log(imageElement);

  //   const titleElement = postElement.querySelector(".post-title");
  //   if (titleElement) titleElement.textContent = post.title;

  //   const contentElement = postElement.querySelector(".post-content");
  //   if (contentElement) contentElement.textContent = post.content;

  //return postElement;
}

function renderPostList(postList, ulElementId) {
  if (!Array.isArray(postList) || postList.length === 0) return;
  console.log(postList);
  console.log(ulElementId);

  //find ul element
  //loop categoryList
  //each category => create li element => append li into ul
  const ulElement = document.getElementById(ulElementId);
  if (!ulElement) return;

  for (const post of postList) {
    console.log(post);
    const liElement = createPostElement(post);
    ulElement.appendChild(liElement);
  }
}

// getPostList();
