function getPostCategoryList() {
  const url = "https://localhost:7213/api/Post";
  const ulElement = document.getElementById("posts");

  if (ulElement) {
    ulElement.textContent = "";
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
            `<img src="${post.image}" alt="" style="width:150px;height:140px">` +
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

// function getFilterProductList(searchFilter) {
//   let url = `https://localhost:7144/posts/filter/${searchFilter}`;
//   console.log(url);
//   const ulElement = document.getElementById("products");

//   if (ulElement) {
//     ulElement.textContent = "";
//     fetch(url)
//       .then((response) => {
//         return response.json();
//       })
//       .then((data) => {
//         let products = data;

//         for (let product of products) {
//           console.log(product);
//           let liElement = document.createElement("li");
//           liElement.innerHTML = product.name + " - " + product.description;

//           ulElement.appendChild(liElement);
//         }
//       })
//       .catch(function (error) {
//         console.log(error);
//       });
//   }
// }

// function DeletePost(deletePost) {
//   const url = `https://localhost:7213/api/PostCategory/${deletePost}`;
//   const ulElement = document.getElementById("products");

//   if (ulElement) {
//     ulElement.textContent = "";
//     fetch(url)
//       .then((response) => {
//         return response.json();
//       })
//       .then((data) => {
//         let products = data;

//         for (let product of products) {
//           console.log(product);
//           let liElement = document.createElement("li");
//           liElement.innerHTML = product.name;

//           ulElement.appendChild(liElement);
//         }
//       })
//       .catch(function (error) {
//         console.log(error);
//       });
//   }
// }

// document.getElementById("btnFilter").addEventListener("click", function () {
//   let filterProduct = document.form.filter.value;
//   getFilterProductList(filterProduct);
// });

// document.getElementById("btnProducts").addEventListener("click", function () {
//   getPostCategoryList();
// });

getPostCategoryList();
// document.getElementById("btnDelete").addEventListener("click", function () {
//   let Post = document.form.filter.value.toString();
//   DeletePost(Post);
// });

document.getElementById("btnClear").addEventListener("click", function () {
  const ulElement = document.getElementById("posts");
  if (ulElement) {
    ulElement.textContent = "";
  }
});
