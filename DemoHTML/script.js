function getPostCategoryList() {
  const url = "https://localhost:7213/api/Post";
  const ulElement = document.getElementById("posts");

  if (ulElement) {
    fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        let posts = data;

        for (let post of posts) {
          console.log(post);
          let liElement = document.createElement("li");
          var data =
            '<div class="post">' +
            `<img src="${post.description}" alt="" style="width:150px;height:140px">` +
            `<p>${post.title}</p>` +
            `<p>${post.content}</p>` +
            "</div>";
          liElement.innerHTML = data;

          ulElement.appendChild(liElement);
        }
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}

function createPostElement(post) {
  if (!post) return null;

  const postTemplate = document.getElementById("postTemplate");
  if (!postTemplate) return;

  const postElement = postTemplate.content.firstElementChild.cloneNode(true);
  console.log(postElement);
  postElement.dataset.id = post.id;

  const imageElement = postElement.querySelector(".post-image");
  if (imageElement) imageElement.src = post.image;
  console.log(imageElement);

  const titleElement = postElement.querySelector(".post-title");
  if (titleElement) titleElement.textContent = post.title;

  const contentElement = postElement.querySelector(".post-content");
  if (contentElement) contentElement.textContent = post.content;

  return postElement;
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

getPostCategoryList();

// document.getElementById("btnClear").addEventListener("click", function () {
//   const ulElement = document.getElementById("posts");
//   if (ulElement) {
//     ulElement.textContent = "";
//   }
// });
