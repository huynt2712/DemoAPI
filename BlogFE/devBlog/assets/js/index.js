const postUrl = "https://localhost:7213/api/Post";
let listPost = [];
let currentPage = 1;
let pageSize = 2;

function renderTemplateHtml(post) {
  const postTemplate = document.getElementById("postTemplate");
  if (!postTemplate) return;

  const postElement = postTemplate.content.firstElementChild.cloneNode(true);
  postElement.dataset.id = post.id;

  const imageElement = postElement.querySelector("#img-post");
  imageElement.src = `https://localhost:7213/${post.imagePath}`;

  const titleElement = postElement.querySelector("#post-title");
  titleElement.textContent = post.title;

  const descriptionElement = postElement.querySelector("#post-description");
  descriptionElement.textContent = post.description;

  const postListElement = document.getElementById("postList");
  if (!postListElement) return;

  postListElement.appendChild(postElement);
}

function getListPost(searchText = "") {
  fetch(
    `${postUrl}?SearchText=${searchText}&PageNumber=${currentPage}&PageSize=${pageSize}`
  )
    .then((response) => response.json()) //=> arrow function
    .then((data) => {
      data.items.forEach((post) => {
        renderTemplateHtml(post);
      });

      currentPage = data.currentPage;
      if (data.totalPages > 0) setupPagintion(data);
    })
    .catch((error) => console.error("Unable to get post list.", error));
}

//renderTemplateHtml();

// function getPostList() {
//   const url = "https://localhost:7213/api/Post";
//   //const ulElement = document.getElementById("posts");

//   fetch(url)
//     .then((response) => {
//       return response.json();
//     })
//     .then((data) => {
//       data.items.forEach((post) => {
//         renderTemplateHtml(post);
//       });
//     })
//     .catch(function (error) {
//       console.log(error);
//     });
// }

function setupPagintion(data) {
  let postPaginationElement = document.getElementById("post_pagination");
  if (!postPaginationElement) return;

  postPaginationElement.innerHTML = "";
  if (data.hasPrevious) {
    let itemElement = document.createElement("a");
    itemElement.innerHTML = "Prevous";
    itemElement.classList.add(
      "nav-link-prevous",
      "nav-item",
      "nav-link",
      "rounded-left",
      `pageNumber_${currentPage - 1}`
    );
    itemElement.setAttribute("onclick", `paginationPost(${currentPage - 1})`);
    postPaginationElement.append(itemElement);
  }

  // for (let pageNumber = 0; pageNumber < data.totalPages; pageNumber++) {
  //   itemElement = document.createElement("a");
  //   itemElement.classList.add(
  //     "nav-link-prev nav-item nav-link rounded-left",
  //     `pageNumber_${pageNumber + 1}`
  //   );
  //   itemElement.innerHTML = pageNumber + 1;
  //   itemElement.setAttribute("onclick", `paginationPost(${pageNumber + 1})`);
  //   postPaginationElement.append(itemElement);
  // }

  if (data.hasNext) {
    let itemElement = document.createElement("a");
    itemElement.innerHTML = "Next";
    itemElement.classList.add(
      "nav-link-next",
      "nav-item",
      "nav-link",
      "rounded-right",
      `pageNumber_${currentPage + 1}`
    );
    itemElement.setAttribute("onclick", `paginationPost(${currentPage + 1})`);
    postPaginationElement.append(itemElement);
  }

  // let currentPageElement = document.querySelector(`.pageNumber_${currentPage}`);
  // currentPageElement.classList.add("w3-green");
}

function paginationPost(pageNumber) {
  currentPage = pageNumber;
  // let searchPostElement = document.getElementById("searchPost");
  // if (searchPostElement) {
  //   let searchPostValue = searchPostElement.value.toLowerCase();

  // }
  getListPost();
}

//getPostList();
getListPost();
