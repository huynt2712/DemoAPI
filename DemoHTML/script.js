function getProductList() {
  const url = "https://localhost:7144/posts";
  const ulElement = document.getElementById("products");

  if (ulElement) {
    ulElement.textContent = "";
    fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        let products = data;

        for (let product of products) {
          console.log(product);
          let liElement = document.createElement("li");
          liElement.innerHTML = product.name + " - " + product.description;

          ulElement.appendChild(liElement);
        }
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}

function getFilterProductList(searchFilter) {
  let url = `https://localhost:7144/posts/filter/${searchFilter}`;
  console.log(url);
  const ulElement = document.getElementById("products");

  if (ulElement) {
    ulElement.textContent = "";
    fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        let products = data;

        for (let product of products) {
          console.log(product);
          let liElement = document.createElement("li");
          liElement.innerHTML = product.name + " - " + product.description;

          ulElement.appendChild(liElement);
        }
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}

document.getElementById("btnFilter").addEventListener("click", function () {
  let filterProduct = document.form.filter.value;
  getFilterProductList(filterProduct);
});

document.getElementById("btnProducts").addEventListener("click", function () {
  getProductList();
});

document.getElementById("btnClear").addEventListener("click", function () {
  const ulElement = document.getElementById("products");
  if (ulElement) {
    ulElement.textContent = "";
  }
});
